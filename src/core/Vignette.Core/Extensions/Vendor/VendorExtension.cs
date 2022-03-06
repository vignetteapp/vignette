// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.ClearScript;
using Microsoft.ClearScript.JavaScript;
using Microsoft.ClearScript.V8;

namespace Vignette.Core.Extensions.Vendor
{
    public abstract class VendorExtension : Extension, IDisposable
    {
        public override string Name => metadata.Name;
        public override string Author => metadata.Author;
        public override string Description => metadata.Description;
        public override string Identifier => metadata.Identifier;
        public override Version Version => metadata.Version;
        public ExtensionIntents Intents => metadata.Intents;
        public IReadOnlyList<VendorExtensionDependency> Dependencies => metadata.Dependencies;

        protected bool IsDisposed { get; private set; }

        private readonly V8Runtime runtime;
        private readonly VendorExtensionMetadata metadata;
        private V8ScriptEngine engine;
        private V8Script script;
        private ScriptObject onActivate;
        private ScriptObject onDeactivate;

        public VendorExtension(V8Runtime runtime, VendorExtensionMetadata metadata)
        {
            this.runtime = runtime;
            this.metadata = metadata;
        }

        protected sealed override void Initialize()
        {
            var flags = V8ScriptEngineFlags.EnableDynamicModuleImports
                | V8ScriptEngineFlags.EnableTaskPromiseConversion
                | V8ScriptEngineFlags.EnableValueTaskPromiseConversion
                | V8ScriptEngineFlags.DisableGlobalMembers;

            engine = runtime.CreateScriptEngine(Identifier, flags);

            Prepare(engine);

            script = engine.Compile(GetDocumentInfo(out string code), code);
            engine.Execute(script);

            onActivate?.Invoke(false);
        }

        protected sealed override void Destroy()
        {
            cleanup();
        }

        protected sealed override object Invoke(object method, params object[] args)
        {
            if (method is ScriptObject item)
                return item.Invoke(false, args);

            return null;
        }

        protected virtual void Prepare(V8ScriptEngine engine)
        {
            engine.DocumentSettings.AccessFlags = DocumentAccessFlags.EnforceRelativePrefix | DocumentAccessFlags.EnableFileLoading;
            engine.DocumentSettings.AddSystemDocument("vignette", ModuleCategory.Standard, "export const { vignette } = import.meta;", getVendorMeta);
        }

        protected abstract DocumentInfo GetDocumentInfo(out string code);

        protected virtual void Dispose(bool disposing)
        {
            if (IsDisposed)
                return;

            cleanup();

            IsDisposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void cleanup()
        {
            onDeactivate?.Invoke(false);
            script?.Dispose();
            script = null;
            engine?.Dispose();
            engine = null;
        }

        private IDictionary<string, object> getVendorMeta(DocumentInfo info) => new Dictionary<string, object>
        {
            {
                "vignette",
                new
                {
                    version = Assembly.GetExecutingAssembly().GetName().Version.ToString(3),
                    platform = getOSPlatform(),
                    commands = new
                    {
                        dispatch = new Func<string, IList, object>(dispatch),
                        register = new Action<string, ScriptObject>(register)
                    },
                    extension = new
                    {
                        id = Identifier,
                        name = Name,
                        author = Author,
                        version = Version.ToString(3),
                        description = Description,
                        onActivate = new Action<ScriptObject>(s => register("__activate__", s)),
                        onDeactivate = new Action<ScriptObject>(s => register("__deactivate__", s)),
                    }
                }
            }
        };

        private void register(string channel, ScriptObject value)
        {
            if (((dynamic)value).constructor.name != "Function")
                throw new ArgumentException(@"Value must be a function.");

            if (channel == "__activate__")
                onActivate = value;
            else if (channel == "__deactivate__")
                onDeactivate = value;
            else
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

        private static string getOSPlatform()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return "Windows";

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                return "Linux";

            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                return "OSX";

            return string.Empty;
        }
    }
}
