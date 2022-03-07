// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.ClearScript;

namespace Vignette.Core.Extensions.Vendor.Modules
{
    public class HostModule : VendorExtensionModule
    {
        public override string Name => @"vignette";

        [ScriptMember("version")]
        public readonly string Version = Assembly.GetExecutingAssembly().GetName().Version.ToString(3);

        [ScriptMember("commands")]
        public readonly CommandsModule Commands;

        [ScriptMember("extension")]
        public readonly ExtensionContextModule Context;

        public HostModule(VendorExtension extension)
            : base(extension)
        {
            Context = new ExtensionContextModule(extension);
            Commands = new CommandsModule(extension);
        }

        public class ExtensionContextModule : VendorExtensionModule
        {
            public override string Name => @"extension";

            [ScriptMember("name")]
            public string ExtensionName => Extension.Name;

            [ScriptMember("mode")]
            public ExtensionMode ExtensionMode => Extension.Mode;

            [ScriptMember("author")]
            public string ExtensionAuthor => Extension.Author;

            [ScriptMember("version")]
            public string ExtensionVersion => Extension.Version.ToString(3);

            [ScriptMember("description")]
            public string ExtensionDescription => Extension.Description;

            public ExtensionContextModule(VendorExtension extension)
                : base(extension)
            {
            }

            [ScriptMember("has")]
            public bool HasExtension(string identifier)
                => Extension.ExtensionSystem.Loaded.Any(ext => ext.Identifier == identifier);
        }

        public class CommandsModule : VendorExtensionModule
        {
            public override string Name => @"commands";

            public CommandsModule(VendorExtension extension)
                : base(extension)
            {
            }

            [ScriptMember("register")]
            public void Register(string channel, object value)
            {
                if (value is ScriptObject scriptObject)
                {
                    string name = ((dynamic)scriptObject).constructor.name;
                    if (name == "Function" || name == "AsyncFunction")
                    {
                        Extension.Register(channel, value);
                        return;
                    }
                }

                if (value is Task)
                {
                    Extension.Register(channel, value);
                    return;
                }

                throw new ArgumentException(@"Value must be a function or promise.");
            }

            [ScriptMember("unregister")]
            public void Unregister(string channel)
            {
                Extension.Unregister(channel);
            }

            [ScriptMember("dispatch")]
            public object Dispatch(string command, IList args)
            {
                var ext = getExtension(command, out string channel);
                return ext.Dispatch(Extension, channel, args.Cast<object>().ToArray());
            }

            [ScriptMember("dispatchAsync")]
            public Task<object> DispatchAsync(string command, IList args)
            {
                var ext = getExtension(command, out string channel);
                return ext.DispatchAsync(Extension, channel, args.Cast<object>().ToArray());
            }

            private Extension getExtension(string input, out string command)
            {
                string[] cmd = input.Split(':', 2, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                command = cmd[1];

                var e = Extension.ExtensionSystem.Loaded.FirstOrDefault(e => e.Identifier == cmd[0]);

                if (e is not Extension ext)
                    throw new Exception($@"Extension {cmd[0]} is not found.");

                return ext;
            }
        }
    }
}
