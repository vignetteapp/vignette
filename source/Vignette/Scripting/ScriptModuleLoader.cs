// Copyright (c) Cosyne
// Licensed under GPL 3.0 with SDK Exception. See LICENSE for details.

using System;
using System.IO;
using Esprima;
using Esprima.Ast;
using Jint;
using Jint.Runtime;
using Jint.Runtime.Modules;
using Sekai.Storages;

namespace Vignette.Scripting;

public sealed class ScriptModuleLoader : IModuleLoader
{
    private readonly Storage storage;
    private static readonly Uri baseUri = new("files:///extensions/");

    public ScriptModuleLoader(Storage storage)
    {
        this.storage = storage;
    }

    public Module LoadModule(Engine engine, ResolvedSpecifier resolved)
    {
        if (resolved.Type != SpecifierType.RelativeOrAbsolute)
        {
            throw new NotSupportedException("Cannot load from bare specifiers.");
        }

        if (resolved.Uri is null)
        {
            throw new InvalidOperationException("Module has no resolved URI.");
        }

        string path = Uri.UnescapeDataString(resolved.Uri.AbsolutePath);

        if (!storage.Exists(path))
        {
            throw new ArgumentException("Module Not Found: ", resolved.Specifier);
        }

        using var stream = storage.Open(path, FileMode.Open, FileAccess.Read);
        using var reader = new StreamReader(stream);

        string code = reader.ReadToEnd();

        try
        {
            return new JavaScriptParser().ParseModule(code, resolved.Uri.LocalPath);
        }
        catch (ParserException ex)
        {
            throw new JavaScriptException(engine.Construct("SyntaxError", $"Error while loading module: error in module '{resolved.Uri.LocalPath}': {ex.Error}"));
        }
        catch (Exception)
        {
            throw new JavaScriptException($"Could not load module {resolved.Uri.LocalPath}");
        }
    }

    public ResolvedSpecifier Resolve(string? referencingModuleLocation, string specifier)
    {
        if (string.IsNullOrEmpty(specifier))
        {
            throw new ModuleResolutionException("Invalid Module Specifier.", specifier, referencingModuleLocation);
        }

        Uri resolved;

        if (Uri.TryCreate(specifier, UriKind.Absolute, out _))
        {
            throw new ModuleResolutionException("Absolute Paths are not permitted.", specifier, referencingModuleLocation);
        }
        else if (isRelative(specifier))
        {
            resolved = new Uri(baseUri, specifier);
        }
        else
        {
            return new ResolvedSpecifier(specifier, specifier, null, SpecifierType.Bare);
        }

        if (resolved.IsFile)
        {
            if (resolved.UserEscaped)
            {
                throw new ModuleResolutionException("Invalid Module Specifier.", specifier, referencingModuleLocation);
            }

            if (!Path.HasExtension(resolved.LocalPath))
            {
                throw new ModuleResolutionException("Unsupported Directory Import.", specifier, referencingModuleLocation);
            }
        }

        return new ResolvedSpecifier(specifier, resolved.AbsoluteUri, resolved, SpecifierType.RelativeOrAbsolute);
    }

    private static bool isRelative(string specifier)
    {
        return specifier[0] is '.' or '/';
    }
}
