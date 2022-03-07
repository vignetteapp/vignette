// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using System;
using System.Collections.Generic;
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
        public ExtensionSystem ExtensionSystem { get; private set; }
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

        public virtual async Task<object> DispatchAsync(IExtension actor, string channel, params object[] args)
        {
            if (!channels.TryGetValue(channel, out var item))
                throw new ChannelNotFoundException();

            return await Invoke(item, args);
        }

        public object Dispatch(IExtension actor, string channel, params object[] args)
            => DispatchAsync(actor, channel, args).GetAwaiter().GetResult();

        public async Task<T> DispatchAsync<T>(IExtension actor, string channel, params object[] args)
            => (T)await DispatchAsync(actor, channel, args);

        public T Dispatch<T>(IExtension actor, string channel, params object[] args)
            => (T)Dispatch(actor, channel, args);

        public bool Register(string channel, object action) => channels.TryAdd(channel, action);
        public void Unregister(string channel) => channels.Remove(channel);

        protected virtual void Initialize()
        {
        }

        protected virtual void Destroy()
        {
        }

        protected abstract Task<object> Invoke(object method, params object[] args);

        public bool Equals(IExtension other)
            => Name.Equals(other.Name) && Author.Equals(other.Author)
                && Description.Equals(other.Description) && Identifier.Equals(other.Identifier)
                && Version.Equals(other.Version);

        public override bool Equals(object obj)
            => Equals(obj as IExtension);

        public override int GetHashCode()
            => HashCode.Combine(Name, Author, Description, Identifier, Version);

        public override string ToString() => $@"{Identifier} ({Version.ToString(3)})";
    }

    public class ChannelNotFoundException : Exception
    {
    }
}
