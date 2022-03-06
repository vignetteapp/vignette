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
        protected IReadOnlyDictionary<string, object> Channels => channels;
        private readonly Dictionary<string, object> channels = new Dictionary<string, object>();

        public void Activate(ExtensionSystem extensionSystem)
        {
            if (Activated)
                return;

            ExtensionSystem = extensionSystem ?? throw new ArgumentNullException(nameof(extensionSystem));

            Initialize();

            Activated = true;
        }

        public void Deactivate()
        {
            if (!Activated)
                return;

            Destroy();

            ExtensionSystem = null;
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

        protected virtual void Initialize()
        {
        }

        protected virtual void Destroy()
        {
        }

        protected bool Register(string channel, object action) => channels.TryAdd(channel, action);
        protected void Unregister(string channel) => channels.Remove(channel);
        protected abstract object Invoke(object method, params object[] args);

        public bool Equals(IExtension other)
            => Name.Equals(other.Name) && Author.Equals(other.Author)
                && Description.Equals(other.Description) && Identifier.Equals(other.Identifier)
                && Version.Equals(other.Version);

        public override bool Equals(object obj)
            => Equals(obj as IExtension);

        public override int GetHashCode()
            => HashCode.Combine(Name, Author, Description, Identifier, Version);
    }

    public class ChannelNotFoundException : Exception
    {
    }
}
