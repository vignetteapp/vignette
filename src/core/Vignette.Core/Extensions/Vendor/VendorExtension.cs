// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.ClearScript;
using Microsoft.ClearScript.V8;
using Newtonsoft.Json;
using Stride.Core.IO;
using Vignette.Core.IO.Serialization;

namespace Vignette.Core.Extensions.Vendor
{
    public abstract class VendorExtension : Extension, IDisposable
    {
        public override string Name => name;
        public override string Author => author;
        public override string Description => description;
        public override string Identifier => id;
        public override Version Version => version;
        public ExtensionIntents Intents => intents;
        public IReadOnlyList<VendorExtensionDependency> Dependencies => dependencies;

        [JsonProperty]
        private string name { get; set; }

        [JsonProperty]
        private string author { get; set; }

        [JsonProperty]
        private string id { get; set; }

        [JsonProperty]
        private string description { get; set; }

        [JsonProperty(ItemConverterType = typeof(VersionConverter))]
        private Version version { get; set; }

        [JsonProperty(ItemConverterType = typeof(FlagConverter<ExtensionIntents>))]
        private ExtensionIntents intents { get; set; }

        [JsonProperty]
        private VendorExtensionDependency[] dependencies { get; set; }

        protected readonly IVirtualFileProvider Files;
        private readonly V8Runtime runtime;
        private V8ScriptEngine engine;
        private V8Script script;
        private bool isDisposed;

        public VendorExtension(V8Runtime runtime, IVirtualFileProvider files)
        {
            Files = files;

            if (Files.FileExists("meta.json"))
            {
                using var stream = Files.OpenStream("meta.json", VirtualFileMode.Open, VirtualFileAccess.Read);
                using var reader = new StreamReader(stream);
                JsonConvert.PopulateObject(reader.ReadToEnd(), this);
            }

            this.runtime = runtime;
        }

        public sealed override void Activate(ExtensionSystem extensionSystem)
        {
            base.Activate(extensionSystem);

            if (Activated || !Files.FileExists("extension.js"))
                return;

            using var stream = Files.OpenStream("extension.js", VirtualFileMode.Open, VirtualFileAccess.Read);
            using var reader = new StreamReader(stream);

            string code = reader.ReadToEnd();

            engine = runtime.CreateScriptEngine(Identifier);
            script = engine.Compile(CreateDocumentInfo(code), code);

            Prepare(engine);

            engine.Execute(script);

            if ((bool)engine.Evaluate("typeof activate === 'function'"))
                engine.Script.activate();
        }

        public sealed override void Deactivate()
        {
            base.Deactivate();
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
        }

        protected abstract DocumentInfo CreateDocumentInfo(string code);

        protected virtual void Dispose(bool disposing)
        {
            if (isDisposed)
                return;

            Files.Dispose();
            cleanup();

            isDisposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void cleanup()
        {
            if ((bool)engine?.Evaluate("typeof deactivate === 'function'"))
                engine?.Script.deactivate();

            script?.Dispose();
            script = null;
            engine?.Dispose();
            engine = null;
        }
    }
}
