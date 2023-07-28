// Copyright (c) Cosyne
// Licensed under GPL 3.0 with SDK Exception. See LICENSE for details.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Vignette;

/// <summary>
/// Locates and resolves registered services.
/// </summary>
public sealed class ServiceLocator : IServiceProvider
{
    /// <summary>
    /// An empty <see cref="ServiceLocator"/>.
    /// </summary>
    public static readonly ServiceLocator Empty = new(ImmutableDictionary<Type, object>.Empty);

    private readonly IReadOnlyDictionary<Type, object> services;

    private ServiceLocator(IReadOnlyDictionary<Type, object> services)
    {
        this.services = services;
    }

    /// <summary>
    /// Resolves the service of a given type.
    /// </summary>
    /// <param name="type">The object type to resolve.</param>
    /// <param name="required">Whether the service is required or not.</param>
    /// <returns>The service object of the given type or <see langword="null"/> when <paramref name="required"/> is <c>false</c> and the service is not found.</returns>
    /// <exception cref="ServiceNotFoundException">Thrown when <paramref name="required"/> is <c>true</c> and the service is not found.</exception>
    public object? Resolve(Type type, [DoesNotReturnIf(true)] bool required = true)
    {
        if (!services.TryGetValue(type, out object? instance) && required)
        {
            throw new ServiceNotFoundException(type);
        }

        return instance;
    }

    /// <summary>
    /// Resolves the service of a given type.
    /// </summary>
    /// <typeparam name="T">The object type to resolve.</typeparam>
    /// <param name="required">Whether the service is required or not.</param>
    /// <returns>The service object of type <typeparamref name="T"/> or <see langword="null"/> when <paramref name="required"/> is <c>false</c> and the service is not found.</returns>
    /// <exception cref="ServiceNotFoundException">Thrown when <paramref name="required"/> is <c>true</c> and the service is not found.</exception>
    public T? Resolve<T>([DoesNotReturnIf(true)] bool required = true)
        where T : class
    {
        return Unsafe.As<T>(Resolve(typeof(T), required));
    }

    /// <summary>
    /// Attempts to resolve a service.
    /// </summary>
    /// <param name="type">The type of service to resolve.</param>
    /// <param name="service">The resolved service or <see langword="null"/> if not found.</param>
    /// <returns><see langword="true"/> if the service has been resolved. Otherwise, <see langword="false"/>.</returns>
    public bool TryResolve(Type type, [NotNullWhen(true)] out object? service)
    {
        service = Resolve(type, false);
        return service is not null;
    }

    /// <summary>
    /// Attempts to resolve the <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of service to resolve.</typeparam>
    /// <param name="service">The resolved service or <see langword="null"/> if not found.</param>
    /// <returns><see langword="true"/> if the service has been resolved. Otherwise, <see langword="false"/>.</returns>
    public bool TryResolve<T>([NotNullWhen(true)] out T? service)
        where T : class
    {
        service = Resolve<T>(false);
        return service is not null;
    }

    /// <summary>
    /// Creates a registry to allow registering services.
    /// </summary>
    public static IServiceRegistry CreateRegistry()
    {
        return new ServiceRegistry();
    }

    object? IServiceProvider.GetService(Type type) => Resolve(type, false);

    private sealed class ServiceRegistry : IServiceRegistry
    {
        private readonly ImmutableDictionary<Type, object>.Builder instances = ImmutableDictionary.CreateBuilder<Type, object>();

        public IServiceRegistry Register(Type type, object instance)
        {
            if (type.IsValueType)
            {
                throw new ArgumentException($"{type} is a value type.", nameof(type));
            }

            if (instances.ContainsKey(type))
            {
                throw new ArgumentException($"{type} already exists in this locator.", nameof(type));
            }

            if (!instance.GetType().IsAssignableTo(type))
            {
                throw new InvalidCastException($"The {nameof(instance)} cannot be casted to {type}.");
            }

            instances.Add(type, instance);

            return this;
        }

        public IServiceRegistry Register<T>(T instance)
            where T : notnull
        {
            return Register(typeof(T), instance);
        }
        public IServiceRegistry Register<T>()
            where T : notnull, new()
        {
            return Register(new T());
        }

        public IServiceRegistry Register<T>(Func<T> creator)
            where T : notnull
        {
            var type = typeof(T);

            if (instances.ContainsKey(type))
            {
                throw new ArgumentException($"{type} already exists in this locator.", nameof(T));
            }

            instances.Add(type, (Func<object>)(() => creator()));

            return this;
        }

        public ServiceLocator Build()
        {
            foreach (var pair in instances.ToImmutableDictionary())
            {
                if (pair.Value is Func<object> creator)
                {
                    instances[pair.Key] = creator();
                }
            }

            var services = new ServiceLocator(instances.ToImmutableDictionary());

            foreach (object obj in instances.Values)
            {
                if (obj is not IServiceObject service)
                {
                    continue;
                }

                service.Initialize(services);
            }

            return services;
        }
    }
}

/// <summary>
/// Provides a mechanism for registering services which can then later be compiled.
/// </summary>
public interface IServiceRegistry
{
    /// <summary>
    /// Registers a service.
    /// </summary>
    /// <param name="type">The type of service.</param>
    /// <param name="instance">The service instance.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="type"/> is already added.</exception>
    /// <exception cref="InvalidCastException">Thrown when <paramref name="instance"/> cannot be assigned to <paramref name="type"/>.</exception>
    IServiceRegistry Register(Type type, object instance);

    /// <summary>
    /// Registers a <typeparamref name="T"/>.
    /// </summary>
    /// <param name="instance">The service instance.</param>
    /// <typeparam name="T">The type of service.</typeparam>
    /// <exception cref="ArgumentException">Thrown when <typeparamref name="T"/> is already added.</exception>
    /// <exception cref="InvalidCastException">Thrown when <paramref name="instance"/> cannot be assigned to <typeparamref name="T"/>.</exception>
    IServiceRegistry Register<T>(T instance) where T : notnull;

    /// <summary>
    /// Registers a <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of service.</typeparam>
    IServiceRegistry Register<T>() where T : notnull, new();

    /// <summary>
    /// Lazily registers a <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type to register.</typeparam>
    /// <param name="creator">The creation function.</param>
    /// <exception cref="ArgumentException">Thrown when <typeparamref name="T"/> is already added</exception>
    IServiceRegistry Register<T>(Func<T> creator) where T : notnull;

    /// <summary>
    /// Compiles registered services to a <see cref="ServiceLocator"/>.
    /// </summary>
    /// <returns>The built service locator.</returns>
    ServiceLocator Build();
}

/// <summary>
/// An object that resolves services.
/// </summary>
internal interface IServiceObject
{
    /// <summary>
    /// Initializes the <see cref="IServiceObject"/>.
    /// </summary>
    /// <param name="services">The service locator.</param>
    void Initialize(ServiceLocator services);
}

/// <summary>
/// Exception thrown when <see cref="ServiceLocator"/> fails to locate a required service of a given type.
/// </summary>
public sealed class ServiceNotFoundException : Exception
{
    internal ServiceNotFoundException(Type type)
        : base($"Failed to locate service of type {type}.")
    {
    }
}
