// Copyright (c) Cosyne
// Licensed under GPL 3.0 with SDK Exception. See LICENSE for details.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;

namespace Vignette;

public abstract class Node : INotifyCollectionChanged, ICollection<Node>, IEquatable<Node>
{
    /// <summary>
    /// The <see cref="Node"/>'s unique identifier.
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// The <see cref="Node"/>'s name.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// The depth of this <see cref="Node"/> relative to the root.
    /// </summary>
    public int Depth { get; private set; }

    /// <summary>
    /// The number of children this <see cref="Node"/> contains.
    /// </summary>
    public int Count => nodes.Count;

    /// <summary>
    /// The parent <see cref="Node"/>.
    /// </summary>
    public Node? Parent { get; private set; }

    /// <summary>
    /// Called when the <see cref="Node"/>'s children has been changed.
    /// </summary>
    public event NotifyCollectionChangedEventHandler? CollectionChanged;

    private readonly Dictionary<string, Node> nodes = new();

    /// <summary>
    /// Creates a new <see cref="Node"/>.
    /// </summary>
    /// <param name="name">The optional name for this <see cref="Node"/>.</param>
    protected Node(string? name = null)
        : this(Guid.NewGuid(), name)
    {
    }

    private Node(Guid id, string? name = null)
    {
        Id = id;
        Name = name ?? id.ToString();
    }

    /// <summary>
    /// Called when the <see cref="Node"/> has entered the node graph.
    /// </summary>
    protected virtual void Enter()
    {
    }

    /// <summary>
    /// Called when the <see cref="Node"/> is leaving the node graph.
    /// </summary>
    protected virtual void Leave()
    {
    }

    /// <summary>
    /// Adds a child <see cref="Node"/>.
    /// </summary>
    /// <param name="node">The <see cref="Node"/> to add.</param>
    public void Add(Node node)
    {
        add(node);
        raiseCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, node));
    }

    /// <summary>
    /// Adds a range of children.
    /// </summary>
    /// <param name="nodes">The children to add.</param>
    public void AddRange(IEnumerable<Node> nodes)
    {
        foreach (var node in nodes)
        {
            add(node);
        }

        raiseCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, nodes.ToArray()));
    }

    /// <summary>
    /// Removes a child <see cref="Node"/>.
    /// </summary>
    /// <param name="node">The <see cref="Node"/> to remove.</param>
    /// <returns><see langword="true"/> if the <see cref="Node"/> has been removed. Otherwise, returns <see langword="false"/>.</returns>
    public bool Remove(Node node)
    {
        if (!remove(node))
        {
            return false;
        }

        raiseCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, node));

        return true;
    }

    /// <summary>
    /// Removes a range of children based on a given <paramref name="predicate"/>.
    /// </summary>
    /// <param name="predicate">The predicate used to select the children.</param>
    /// <returns>The number of removed children.</returns>
    public int RemoveRange(Predicate<Node> predicate)
    {
        var selected = nodes.Values.Where(n => predicate(n)).ToArray();

        foreach (var node in selected)
        {
            remove(node);
        }

        raiseCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, selected));

        return selected.Length;
    }

    /// <summary>
    /// Removes a range of children.
    /// </summary>
    /// <param name="nodes">The children to remove.</param>
    /// <returns>The number of removed children.</returns>
    public int RemoveRange(IEnumerable<Node> nodes)
    {
        var removed = new List<Node>();

        foreach (var node in nodes)
        {
            if (remove(node))
            {
                removed.Add(node);
            }
        }

        raiseCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, removed));

        return removed.Count;
    }

    /// <summary>
    /// Removes all children from this <see cref="Node"/>.
    /// </summary>
    public void Clear()
    {
        var copy = nodes.Values.ToArray();

        foreach (var node in copy)
        {
            remove(node);
        }

        raiseCollectionChanged(reset_args);
    }

    /// <summary>
    /// Determines whether a given <see cref="Node"/> is a child of this node.
    /// </summary>
    /// <param name="node">The node to test.</param>
    /// <returns><see langword="true"/> if the <paramref name="node"/> is a child of this node or <see langword="false"/> if not.</returns>
    public bool Contains(Node node)
    {
        return nodes.ContainsValue(node);
    }

    /// <summary>
    /// Gets the root node.
    /// </summary>
    /// <returns>The root node.</returns>
    public Node GetRoot()
    {
        var current = this;

        while (current.Parent is not null)
        {
            current = current.Parent;
        }

        return current;
    }

    /// <summary>
    /// Gets the nearest <typeparamref name="T"/> node.
    /// </summary>
    /// <typeparam name="T">The node type to search for.</typeparam>
    /// <returns>The nearest node.</returns>
    public T? GetNearest<T>()
        where T : Node
    {
        var current = this;

        while (current.Parent is not null)
        {
            current = current.Parent;

            if (current is T)
            {
                break;
            }
        }

        return current as T;
    }

    /// <summary>
    /// Gets the node from the given path.
    /// </summary>
    /// <param name="path">The relative or absolute path.</param>
    /// <returns>The node on the given path.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="path"/> is invalid.</exception>
    /// <exception cref="KeyNotFoundException">Thrown when a part of the path is not found.</exception>
    public Node GetNode(string path)
    {
        if (!Uri.TryCreate(path, UriKind.RelativeOrAbsolute, out var uri))
        {
            throw new ArgumentException("Provided path is not a URI.", nameof(path));
        }

        if (uri.IsAbsoluteUri && uri.Scheme != node_scheme)
        {
            throw new ArgumentException("Absolute paths must start with the node scheme.", nameof(path));
        }

        var current = uri.IsAbsoluteUri ? GetRoot() : this;
        string[] cm = uri.GetComponents(UriComponents.Path, UriFormat.SafeUnescaped).Split(Path.AltDirectorySeparatorChar, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

        foreach (string part in cm)
        {
            if (!current.nodes.ContainsKey(part))
            {
                throw new KeyNotFoundException($"The node \"{part}\" was not found.");
            }

            current = current.nodes[part];
        }

        return current;
    }

    /// <summary>
    /// Gets the node from the given path.
    /// </summary>
    /// <typeparam name="T">The type to cast the node as.</typeparam>
    /// <param name="path">The relative or absolute path.</param>
    /// <returns>The node on the given path.</returns>
    /// <exception cref="InvalidCastException">Thrown when the returned node cannot be casted to <typeparamref name="T"/>.</exception>
    public T GetNode<T>(string path)
        where T : Node
    {
        var node = GetNode(path);

        if (node is not T typed)
        {
            throw new InvalidCastException($"Cannot cast {typeof(T)} to the found node.");
        }

        return (T)node;
    }

    /// <summary>
    /// Gets an enumeration of the nodes of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type to filter the enumeration.</typeparam>
    /// <returns>An enumerable of nodes of type <typeparamref name="T"/>.</returns>
    public IEnumerable<T> GetNodes<T>()
        where T : Node
    {
        return this.OfType<T>();
    }

    public IEnumerator<Node> GetEnumerator()
    {
        return nodes.Values.GetEnumerator();
    }

    public bool Equals(Node? node)
    {
        if (node is null)
        {
            return false;
        }

        if (node.Id.Equals(Id))
        {
            return true;
        }

        if (ReferenceEquals(this, node))
        {
            return true;
        }

        return false;
    }

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        return obj is Node node && Equals(node);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id);
    }

    private void raiseCollectionChanged(NotifyCollectionChangedEventArgs args)
    {
        CollectionChanged?.Invoke(this, args);
    }

    private void add(Node node)
    {
        if (Equals(node))
        {
            throw new ArgumentException("Cannot add self as a child.", nameof(node));
        }

        if (node.Parent is not null)
        {
            throw new ArgumentException("Cannot add a node that already has a parent.", nameof(node));
        }

        if (nodes.ContainsKey(node.Name))
        {
            throw new ArgumentException($"There is already a child with the name \"{node.Name}\".", nameof(node));
        }

        node.Depth = Depth + 1;
        node.Parent = this;

        nodes.Add(node.Name, node);

        node.Enter();
    }

    private bool remove(Node node)
    {
        if (!nodes.ContainsKey(node.Name))
        {
            return false;
        }

        node.Leave();

        node.Depth = 0;
        node.Parent = null;

        nodes.Remove(node.Name);

        return true;
    }

    void ICollection<Node>.CopyTo(Node[] array, int arrayIndex)
    {
        nodes.Values.CopyTo(array, arrayIndex);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    bool ICollection<Node>.IsReadOnly => false;

    private const string node_scheme = "node";
    private static readonly NotifyCollectionChangedEventArgs reset_args = new(NotifyCollectionChangedAction.Reset);
}
