#addin nuget:?package=Cake.Http&version=0.7.0
#addin nuget:?package=Cake.FileHelpers&version=3.3.0

#addin nuget:?package=Cake.Issues&version=0.9.1
#addin nuget:?package=Cake.Issues.MsBuild&version=0.9.0
#addin nuget:?package=Cake.Issues.PullRequests&version=0.9.1
#addin nuget:?package=Cake.Issues.PullRequests.GitHubActions&version=0.9.0

using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

var target = Argument("target", "Test");
var nugetApiKey = Argument("nugetApiKey", string.Empty);
var configuration = Argument("configuration", "Debug");
var runtime = Argument("runtime", "win10-x64");

var rootDirectory = new DirectoryPath("..");
var artifactsDirectory = rootDirectory.Combine("artifacts");
var application = rootDirectory.CombineWithFilePath("vignette.Desktop/vignette.Desktop.csproj");
var tests = rootDirectory.CombineWithFilePath("vignette.Tests/vignette.Tests.csproj");
var logs = rootDirectory.CombineWithFilePath("build/logs/msbuild.binlog");

var today = DateTime.Today;
var version = "0.0.0";

[DataContract]
public class GitHubRelease
{
    [DataMember(Name = "tag_name")]
    public string Tag { get; set; }
}

Task("DetermineVersion")
    .Does(() =>
    {
        using (var stream = new MemoryStream())
        {
            var writer = new StreamWriter(stream);
            writer.Write(HttpGet("https://api.github.com/repos/vignette-project/vignette/releases"));
            writer.Flush();
            stream.Position = 0;

            var serializer = new DataContractJsonSerializer(typeof(List<GitHubRelease>));
            var response = (List<GitHubRelease>)serializer.ReadObject(stream);

            var year = today.Year.ToString();
            var monthDay = $"{today.Month.ToString().PadLeft(2, '0')}{today.Day.ToString().PadLeft(2, '0')}";
            var published = response.Find(r => r.Tag == $"{year}.{monthDay}.0");
            if (published != null)
            {
                var latest = published.Tag.Split('.');
                if ((year == latest[0]) && (monthDay == latest[1]))
                {
                    if (Int32.TryParse(latest[2], out int revision))
                    {
                        revision += 1;
                        version = $"{year}.{monthDay}.{revision.ToString()}";
                    }
                }
            }
            else
            {
                version = $"{year}.{monthDay}.0";
            }

            Information(version);
            EnsureDirectoryExists(artifactsDirectory);
            FileWriteText(artifactsDirectory.CombineWithFilePath("version"), version);
        }
    });

Task("Clean")
    .Does(() =>
    {
        EnsureDirectoryExists(artifactsDirectory);
        CleanDirectory(artifactsDirectory);
    });

Task("Compile")
    .Does(() =>
    {
        var msBuildSettings = new DotNetCoreMSBuildSettings
        {
            BinaryLogger = new MSBuildBinaryLoggerSettings
            {
                Enabled = true,
                FileName = logs.FullPath,
            }
        };

        DotNetCoreBuild(tests.FullPath, new DotNetCoreBuildSettings
        {
            Verbosity = DotNetCoreVerbosity.Minimal,
            Configuration = configuration,
            MSBuildSettings = msBuildSettings
        });
    });

// NOTE: Cake has MSBuild Log File Format 8 and the project has 9
Task("CheckIssues")
    .WithCriteria(GitHubActions.IsRunningOnGitHubActions)
    .IsDependentOn("Compile")
    .Does(() =>
    {
        ReportIssuesToPullRequest(MsBuildIssuesFromFilePath(logs.FullPath, MsBuildBinaryLogFileFormat), GitHubActionsBuilds(), rootDirectory);
    });

Task("Test")
    .Does(() =>
    {
        var settings = new DotNetCoreTestSettings
        {
            Logger = "trx",
            Settings = new FilePath("vstestconfig.runsettings"),
            ToolTimeout = TimeSpan.FromHours(10),
            Configuration = configuration,
            ResultsDirectory = new DirectoryPath("logs"),
        };

        DotNetCoreTest(tests.FullPath, settings);
    });

Task("Pack")
    .IsDependentOn("DetermineVersion")
    .Does(() =>
    {
        var settings = new DotNetCorePublishSettings
        {
            OutputDirectory = artifactsDirectory.Combine(runtime),
            Configuration = configuration,
            Verbosity = DotNetCoreVerbosity.Quiet,
            Runtime = runtime,
            ArgumentCustomization = args =>
            {
                args.Append($"/p:Version={version}");
                args.Append($"/p:GenerateDocumentationFile=true");

                return args;
            }
        };

        DotNetCorePublish(application.FullPath, settings);
    });

Task("Build")
    .IsDependentOn("Clean")
    .IsDependentOn("Compile")
    .IsDependentOn("Test")
    .IsDependentOn("Pack");

RunTarget(target);
