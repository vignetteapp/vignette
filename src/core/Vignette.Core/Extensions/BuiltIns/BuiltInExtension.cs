// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Vignette.Core.Extensions.Vendor;

namespace Vignette.Core.Extensions.BuiltIns
{
    public abstract class BuiltInExtension : Extension
    {
        public override string Author { get; } = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyCompanyAttribute>().Company;
        public override Version Version { get; } = Assembly.GetExecutingAssembly().GetName().Version;
        public virtual ExtensionIntents Intent => ExtensionIntents.None;

        public override void Activate(ExtensionSystem extensionSystem)
        {
            base.Activate(extensionSystem);

            foreach (var method in GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public))
            {
                var chan = method.GetCustomAttribute<ListenAttribute>();

                if (chan == null)
                    continue;

                Register(chan.Name, method);
            }
        }

        public sealed override void Deactivate()
        {
            throw new InvalidOperationException(@"Built-in extensions cannot be disabled.");
        }

        public sealed override Task<bool> TryDispatchAsync(IExtension actor, string channel, out object value, CancellationToken token = default, params object[] args)
        {
            if (Intent != ExtensionIntents.None)
            {
                if (actor is VendorExtension vendor && !vendor.Intents.HasFlag(Intent))
                {
                    value = new InsufficientIntentsException(@"Failed to dispatch as actor lacks proper intents to perform this action.");
                    return Task.FromResult(false);
                }
            }

            return base.TryDispatchAsync(actor, channel, out value, token, args);
        }

        protected sealed override object Invoke(object method, params object[] args)
            => (method as MethodInfo)?.Invoke(this, args);

        [AttributeUsage(AttributeTargets.Method)]
        protected class ListenAttribute : Attribute
        {
            public string Name { get; set; }

            public ListenAttribute(string name)
            {
                Name = name;
            }
        }
    }

    public class InsufficientIntentsException : Exception
    {
        public InsufficientIntentsException(string message)
            : base(message)
        {
        }
    }
}
