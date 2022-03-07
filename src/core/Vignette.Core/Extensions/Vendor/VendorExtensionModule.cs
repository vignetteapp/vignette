// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.ClearScript;

namespace Vignette.Core.Extensions.Vendor
{
    public partial class VendorExtension
    {
        private IDictionary<string, object> getVendorMeta(DocumentInfo info) => new Dictionary<string, object>
        {
            {
                "vignette",
                new
                {
                    version = Assembly.GetExecutingAssembly().GetName().Version.ToString(3),
                    commands = new
                    {
                        register = new Action<string, ScriptObject>(register),
                        dispatch = new Func<string, IList, object>(dispatch),
                    },
                    extension = new
                    {
                        id = Identifier,
                        mode = Mode,
                        name = Name,
                        author = Author,
                        version = Version.ToString(3),
                        description = Description,
                    }
                }
            }
        };

        private void register(string channel, ScriptObject value)
        {
            if (((dynamic)value).constructor.name != "Function")
                throw new ArgumentException(@"Value must be a function.");

            Register(channel, value);
        }

        private object dispatch(string command, IList args)
        {
            string[] cmd = command.Split(':', 2, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            var entry = ExtensionSystem.Loaded.FirstOrDefault(e => e.Identifier == cmd[0]);

            if (entry is not Extension ext)
                throw new Exception($@"Extension {cmd[0]} is not found.");

            return ext.Dispatch(this, cmd[1], args.Cast<object>().ToArray());
        }
    }
}
