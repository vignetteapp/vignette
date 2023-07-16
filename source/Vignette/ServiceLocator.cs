// Copyright (c) Cosyne
// Licensed under GPL 3.0 with SDK Exception. See LICENSE for details.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Vignette;

/// <summary>
/// A collection of services.
/// </summary>
public sealed class ServiceLocator : IServiceLocator, IServiceProvider
{
    private readonly Dictionary<Type, object> services = new();

    /// <summary>
    /// Adds a service to this locator.
    /// </summary>
    /// <param name="type">The type of service.</param>
    /// <param name="instance">The service instance.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="type"/> is already added to this locator.</exception>
    /// <exception cref="InvalidCastException">Thrown when <paramref name="instance"/> cannot be assigned to <paramref name="type"/>.</exception>
    public void Add(Type type, object instance)
    {
        if (services.ContainsKey(type))
        {
            throw new ArgumentException($"{type} already exists in this locator.", nameof(type));
        }

        if (!instance.GetType().IsAssignableTo(type))
        {
            throw new InvalidCastException($"The {nameof(instance)} cannot be casted to {type}.");
        }

        services.Add(type, instance);
    }

    /// <summary>
    /// Adds a service to this locator.
    /// </summary>
    /// <param name="instance">The service instance.</param>
    /// <typeparam name="T">The type of service.</typeparam>
    /// <exception cref="ArgumentException">Thrown when <typeparamref name="T"/> is already added to this locator.</exception>
    /// <exception cref="InvalidCastException">Thrown when <paramref name="instance"/> cannot be assigned to <typeparamref name="T"/>.</exception>
    public void Add<T>(T instance)
        where T : class
    {
        Add(typeof(T), instance);
    }

    /// <summary>
    /// Removes a service from this locator.
    /// </summary>
    /// <param name="type">The service type to remove.</param>
    /// <returns><c>true</c> when the service is removed. <c>false</c> otherwise.</returns>
    public bool Remove(Type type)
    {
        return services.Remove(type);
    }

    /// <summary>
    /// Removes a service from this locator.
    /// </summary>
    /// <typeparam name="T">The service type to remove.</typeparam>
    /// <returns><c>true</c> when the service is removed. <c>false</c> otherwise.</returns>
    public bool Remove<T>()
        where T : class
    {
        return Remove(typeof(T));
    }

    public object? Get(Type type, [DoesNotReturnIf(true)] bool required = true)
    {
        if (!services.TryGetValue(type, out object? instance) && required)
        {
            throw new ServiceNotFoundException(type);
        }

        return instance;
    }

    public T? Get<T>([DoesNotReturnIf(true)] bool required = true)
        where T : class
    {
        return Unsafe.As<T>(Get(typeof(T), required));
    }

    object? IServiceProvider.GetService(Type type) => Get(type, false);
}

/// <summary>
/// An interface for objects capable of locating services.
/// </summary>
public interface IServiceLocator
{
    /// <summary>
    /// Gets the service of a given type.
    /// </summary>
    /// <param name="type">The object type to resolve.</param>
    /// <param name="required">Whether the service is required or not.</param>
    /// <returns>The service object of the given type or <see langword="null"/> when <paramref name="required"/> is <c>false</c> and the service is not found.</returns>
    /// <exception cref="ServiceNotFoundException">Thrown when <paramref name="required"/> is <c>true</c> and the service is not found.</exception>
    object? Get(Type type, [DoesNotReturnIf(true)] bool required = true);

    /// <summary>
    /// Gets the service of a given type.
    /// </summary>
    /// <typeparam name="T">The object type to resolve.</typeparam>
    /// <param name="required">Whether the service is required or not.</param>
    /// <returns>The service object of type <typeparamref name="T"/> or <see langword="null"/> when <paramref name="required"/> is <c>false</c> and the service is not found.</returns>
    /// <exception cref="ServiceNotFoundException">Thrown when <paramref name="required"/> is <c>true</c> and the service is not found.</exception>
    T? Get<T>([DoesNotReturnIf(true)] bool required = true) where T : class;
}

/// <summary>
/// Exception thrown when <see cref="IServiceLocator"/> fails to locate a required service of a given type.
/// </summary>
public sealed class ServiceNotFoundException : Exception
{
    internal ServiceNotFoundException(Type type)
        : base($"Failed to locate service of type {type}.")
    {
    }
}
