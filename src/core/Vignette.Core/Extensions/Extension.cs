// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Vignette.Core.Extensions
{
    public abstract class Extension : IExtension
    {
        public abstract string Name { get; }
        public abstract string Author { get; }
        public abstract string Description { get; }
        public abstract string Identifier { get; }
        public abstract Version Version { get; }
        public bool Activated { get; private set; }
        protected ExtensionSystem ExtensionSystem { get; private set; }
        private readonly Dictionary<string, object> channels = new Dictionary<string, object>();

        public virtual void Activate(ExtensionSystem extensionSystem)
        {
            if (Activated)
                return;

            ExtensionSystem = extensionSystem;
            Activated = true;
        }

        public virtual void Deactivate()
        {
            Activated = false;
        }

        public virtual Task<bool> TryDispatchAsync(IExtension actor, string channel, out object value, CancellationToken token = default, params object[] args)
        {
            if (!channels.TryGetValue(channel, out var item))
            {
                value = new ChannelNotFoundException();
                return Task.FromResult(false);
            }

            try
            {
                value = Invoke(item, args);
                return Task.FromResult(true);
            }
            catch (Exception e)
            {
                value = e;
                return Task.FromResult(false);
            }
        }

        public bool TryDispatch(IExtension actor, string channel, out object value, params object[] args)
            => TryDispatchAsync(actor, channel, out value, default, args).GetAwaiter().GetResult();

        public async Task<object> DispatchAsync(IExtension actor, string channel, params object[] args)
        {
            await TryDispatchAsync(actor, channel, out var value, default, args);

            if (value is Exception e)
                throw e;

            return value;
        }

        public object Dispatch(IExtension actor, string channel, params object[] args)
            => DispatchAsync(actor, channel, args).GetAwaiter().GetResult();

        public async Task<T> DispatchAsync<T>(IExtension actor, string channel, params object[] args)
            => (T)await DispatchAsync(actor, channel, args);

        public T Dispatch<T>(IExtension actor, string channel, params object[] args)
            => (T)Dispatch(actor, channel, args);

        protected bool Register(string channel, object action) => channels.TryAdd(channel, action);
        protected void Unregister(string channel) => channels.Remove(channel);
        protected abstract object Invoke(object method, params object[] args);
    }

    public class ChannelNotFoundException : Exception
    {
    }
}
