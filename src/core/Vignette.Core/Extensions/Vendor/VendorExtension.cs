// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.ClearScript;
using Microsoft.ClearScript.JavaScript;
using Microsoft.ClearScript.V8;
using Vignette.Core.Extensions.Vendor.Modules;
using Vignette.Core.Util;

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
        public virtual Uri DocumentUri { get; }
        public virtual ExtensionMode Mode => ExtensionMode.Production;

        protected bool IsDisposed { get; private set; }

        private readonly VendorExtensionMetadata metadata;
        private V8ScriptEngine engine;

        public VendorExtension(VendorExtensionMetadata metadata)
        {
            this.metadata = metadata;
        }

        public void AddDocument(string name, VendorExtensionModule module)
        {
            engine?.DocumentSettings.AddSystemDocument(name, ModuleCategory.Standard, $"export const {{ {name} }} = import.meta;", module.ToDocumentContext);
        }

        public void AddModule(VendorExtensionModule module)
        {
            engine?.AddHostObject(module.Name, module);
        }

        protected sealed override void Initialize()
        {
            var flags = V8ScriptEngineFlags.EnableDynamicModuleImports
                | V8ScriptEngineFlags.EnableTaskPromiseConversion
                | V8ScriptEngineFlags.EnableValueTaskPromiseConversion
                | V8ScriptEngineFlags.DisableGlobalMembers;

            engine = ExtensionSystem.Runtime.CreateScriptEngine(Identifier, flags);
            engine.DocumentSettings.AccessFlags = DocumentAccessFlags.EnforceRelativePrefix | DocumentAccessFlags.EnableFileLoading;

            AddModule(new ConsoleModule(this));
            AddDocument("vignette", new HostModule(this));

            var documentInfo = DocumentUri != null ? new DocumentInfo(DocumentUri) : new DocumentInfo("extension");
            documentInfo.Category = ModuleCategory.Standard;

            engine.DocumentSettings.AddSystemDocument("extension", new StringDocument(documentInfo, GetDocumentContent(documentInfo)));

            Prepare(engine);

            engine.Execute(new DocumentInfo { Category = ModuleCategory.Standard }, "import { activate } from 'extension'; activate();");
        }

        protected sealed override void Destroy()
        {
            Dispose();
        }

        protected sealed override async Task<object> Invoke(object method, params object[] args)
        {
            if (method is ScriptObject item)
            {
                var result = item.Invoke(false, args);

                if (result is not Task task)
                    return result;

                await task.ConfigureAwait(false);
                return task.GetResult();
            }

            return null;
        }

        protected abstract string GetDocumentContent(DocumentInfo documentInfo);

        protected virtual void Prepare(V8ScriptEngine engine)
        {
        }

        protected virtual void Dispose(bool disposing)
        {
            if (IsDisposed)
                return;

            engine?.Execute(new DocumentInfo { Category = ModuleCategory.Standard }, "import { deactivate } from 'extension'; deactivate();");
            engine?.Dispose();
            engine = null;

            IsDisposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
