// Copyright (c) Cosyne
// Licensed under GPL 3.0 with SDK Exception. See LICENSE for details.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using Sekai.Mathematics;

namespace Vignette.Graphics;

/// <summary>
/// A priority queue that is sorted by the distance between a projector and a model.
/// </summary>
public sealed class RenderQueue : IReadOnlyCollection<RenderData>
{
    /// <summary>
    /// Gets the number of <see cref="RenderObject"/>s queued.
    /// </summary>
    public int Count => renderables.Count;

    private readonly List<RenderData> renderables = new();
    private readonly List<RenderOrder> renderOrders = new();

    /// <summary>
    /// Creates a new render queue.
    /// </summary>
    public RenderQueue()
    {
    }

    /// <summary>
    /// Enqueues a <see cref="RenderObject"/> to this queue.
    /// </summary>
    /// <param name="projector">The projector used.</param>
    /// <param name="world">The model used.</param>
    /// <param name="renderObject">The render object to be enqueued.</param>
    public void Enqueue(IProjector projector, IWorld world, RenderObject renderObject)
    {
        if ((projector.Groups & renderObject.Groups) != 0)
        {
            return;
        }

        if (!renderObject.Bounds.Equals(BoundingBox.Empty))
        {
            if (BoundingFrustum.Contains(projector.Frustum, renderObject.Bounds) == Containment.Disjoint)
            {
                return;
            }
        }

        int renderable = renderables.Count;
        int materialID = renderObject.Material.GetMaterialID();
        float distance = Vector3.Distance((renderObject.Bounds.Center * world.Scale) + world.Position, projector.Position);

        renderables.Add(new RenderData(projector, world, renderObject));
        renderOrders.Add(new(renderable, materialID, distance));
    }

    /// <summary>
    /// Clears the queue.
    /// </summary>
    public void Clear()
    {
        renderables.Clear();
        renderOrders.Clear();
    }

    /// <summary>
    /// Returns an enumerator that iterates through this queue in order.
    /// </summary>
    /// <returns>An <see cref="IEnumerator{RenderObject}"/> that iterates through this queue in order.</returns>
    public IEnumerator<RenderData> GetEnumerator()
    {
        renderOrders.Sort();
        return new Enumerator(renderOrders, renderables);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    private struct Enumerator : IEnumerator<RenderData>
    {
        public RenderData Current { get; private set; }

        private int index;
        private readonly IReadOnlyList<RenderData> renderables;
        private readonly IReadOnlyList<RenderOrder> renderOrders;

        public Enumerator(IReadOnlyList<RenderOrder> renderOrders, IReadOnlyList<RenderData> renderables)
        {
            this.renderables = renderables;
            this.renderOrders = renderOrders;
        }

        public bool MoveNext()
        {
            if (index >= renderOrders.Count)
            {
                Current = default;
                return false;
            }
            else
            {
                Current = renderables[renderOrders[index].Renderable];
                index += 1;
                return true;
            }
        }

        public void Reset()
        {
            index = 0;
            Current = default;
        }

        public readonly void Dispose()
        {
        }

        readonly object IEnumerator.Current => Current;
    }

    private readonly struct RenderOrder : IEquatable<RenderOrder>, IComparable<RenderOrder>
    {
        public int Renderable { get; }
        public int MaterialID { get; }
        public float Distance { get; }

        public RenderOrder(int renderable, int materialID, float distance)
        {
            Distance = distance;
            Renderable = renderable;
            MaterialID = materialID;
        }

        public readonly int CompareTo(RenderOrder other)
        {
            if (Equals(other))
            {
                return 0;
            }

            int value = Distance.CompareTo(other.Distance);

            if (value != 0)
            {
                return value;
            }

            return MaterialID.CompareTo(other.MaterialID);
        }

        public readonly bool Equals(RenderOrder other)
        {
            return Renderable.Equals(other.Renderable) && MaterialID.Equals(other.MaterialID) && Distance.Equals(other.Distance);
        }

        public override readonly bool Equals([NotNullWhen(true)] object? obj)
        {
            return obj is RenderOrder order && Equals(order);
        }

        public override readonly int GetHashCode()
        {
            return HashCode.Combine(Renderable, MaterialID, Distance);
        }

        public static bool operator ==(RenderOrder left, RenderOrder right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(RenderOrder left, RenderOrder right)
        {
            return !(left == right);
        }

        public static bool operator <(RenderOrder left, RenderOrder right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator <=(RenderOrder left, RenderOrder right)
        {
            return left.CompareTo(right) <= 0;
        }

        public static bool operator >(RenderOrder left, RenderOrder right)
        {
            return left.CompareTo(right) > 0;
        }

        public static bool operator >=(RenderOrder left, RenderOrder right)
        {
            return left.CompareTo(right) >= 0;
        }
    }
}

internal static class RenderQueueExtensions
{
    /// <summary>
    /// Begins a rendering context.
    /// </summary>
    /// <param name="queue">The render queue.</param>
    /// <param name="projector">The projector used.</param>
    /// <param name="world">The model used.</param>
    /// <returns>A new render context.</returns>
    internal static RenderContext Begin(this RenderQueue queue, IProjector projector, IWorld world)
    {
        return new RenderContext(queue, projector, world);
    }
}
