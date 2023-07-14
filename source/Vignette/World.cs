// Copyright (c) Cosyne
// Licensed under GPL 3.0 with SDK Exception. See LICENSE for details.

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Sekai.Mathematics;
using Vignette.Collections;
using Vignette.Graphics;

namespace Vignette;

/// <summary>
/// A node that represents the world.
/// </summary>
public class World : Behavior
{
    private readonly SortedFilteredCollection<Behavior> behaviors = new
    (
        Comparer<Behavior>.Default,
        (node) => node.Enabled,
        (node, handler) => node.OrderChanged += handler,
        (node, handler) => node.OrderChanged -= handler,
        (node, handler) => node.EnabledChanged += handler,
        (node, handler) => node.EnabledChanged -= handler
    );

    private readonly SortedFilteredCollection<Drawable> drawables = new
    (
        Comparer<Behavior>.Default,
        (node) => node.Visible,
        (node, handler) => node.OrderChanged += handler,
        (node, handler) => node.OrderChanged -= handler,
        (node, handler) => node.VisibleChanged += handler,
        (node, handler) => node.VisibleChanged -= handler
    );

    private readonly SortedFilteredCollection<World> worlds = new
    (
        Comparer<Behavior>.Default,
        (node) => node.Enabled,
        (node, handler) => node.OrderChanged += handler,
        (node, handler) => node.OrderChanged -= handler,
        (node, handler) => node.EnabledChanged += handler,
        (node, handler) => node.EnabledChanged -= handler
    );

    private readonly List<Light> lights = new();
    private readonly List<Camera> cameras = new();

    private readonly RenderQueue renderQueue = new();
    private readonly Queue<Behavior> behaviorLoadQueue = new();
    private readonly Queue<Behavior> behaviorUnloadQueue = new();

    public World()
    {
        CollectionChanged += handleCollectionChanged;
    }

    public override void Update(TimeSpan elapsed)
    {
        while (behaviorLoadQueue.TryDequeue(out var node))
        {
            node.Load();
            add(node, worlds);
            add(node, cameras);
            add(node, behaviors);
            add(node, drawables);
        }

        while (behaviorUnloadQueue.TryDequeue(out var node))
        {
            node.Load();
            rem(node, worlds);
            rem(node, cameras);
            rem(node, behaviors);
            rem(node, drawables);
        }

        foreach (var behavior in behaviors)
        {
            behavior.Update(elapsed);
        }

        static void add<T>(Node node, ICollection<T> target)
            where T : Node
        {
            if (node is not T typed)
            {
                return;
            }

            target.Add(typed);
        }

        static void rem<T>(Node node, ICollection<T> target)
            where T : Node
        {
            if (node is not T typed)
            {
                return;
            }

            target.Remove(typed);
        }
    }

    public void Draw(Renderer renderer)
    {
        foreach (var world in worlds)
        {
            world.Draw(renderer);
        }

        foreach (var camera in cameras)
        {
            foreach (var light in lights)
            {
                renderQueue.Clear();

                if (BoundingFrustum.Contains(camera.Frustum, light.Frustum) == Containment.Disjoint)
                {
                    continue;
                }

                foreach (var drawable in drawables)
                {
                    drawable.Draw(renderQueue.Begin(light, drawable));
                }

                renderer.Draw(renderQueue);
            }
        }

        foreach (var camera in cameras)
        {
            renderQueue.Clear();

            foreach (var drawable in drawables)
            {
                drawable.Draw(renderQueue.Begin(camera, drawable));
            }

            renderer.Draw(renderQueue);
        }
    }

    private void handleCollectionChanged(object? sender, NotifyCollectionChangedEventArgs args)
    {
        if (args.Action == NotifyCollectionChangedAction.Add)
        {
            foreach (var node in args.NewItems!.Cast<Behavior>())
            {
                load(node);
            }
        }

        if (args.Action == NotifyCollectionChangedAction.Remove)
        {
            foreach (var node in args.OldItems!.Cast<Behavior>())
            {
                unload(node);
            }
        }

        if (args.Action == NotifyCollectionChangedAction.Reset)
        {
            foreach (var node in GetNodes<Behavior>())
            {
                unload(node);
            }
        }
    }

    private void load(Behavior node)
    {
        foreach (var child in node.GetNodes<Behavior>())
        {
            load(child);
        }

        if (node is not World)
        {
            node.CollectionChanged += handleCollectionChanged;
        }

        behaviorLoadQueue.Enqueue(node);
    }

    private void unload(Behavior node)
    {
        foreach (var child in node.GetNodes<Behavior>())
        {
            unload(child);
        }

        if (node is not World)
        {
            node.CollectionChanged -= handleCollectionChanged;
        }

        behaviorUnloadQueue.Enqueue(node);
    }
}
