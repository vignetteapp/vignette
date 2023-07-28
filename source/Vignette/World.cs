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
/// A <see cref="Node"/> that presents and processes its children.
/// </summary>
public class World : Node
{
    private readonly SortedFilteredCollection<Behavior> behaviors = new
    (
        Comparer<Node>.Default,
        static (node) => node.Enabled,
        static (node, handler) => node.OrderChanged += handler,
        static (node, handler) => node.OrderChanged -= handler,
        static (node, handler) => node.EnabledChanged += handler,
        static (node, handler) => node.EnabledChanged -= handler
    );

    private readonly SortedFilteredCollection<Drawable> drawables = new
    (
        Comparer<Node>.Default,
        static (node) => node.Visible,
        static (node, handler) => node.OrderChanged += handler,
        static (node, handler) => node.OrderChanged -= handler,
        static (node, handler) => node.VisibleChanged += handler,
        static (node, handler) => node.VisibleChanged -= handler
    );

    private readonly List<World> worlds = new();
    private readonly List<Light> lights = new();
    private readonly List<Camera> cameras = new();
    private readonly RenderQueue renderQueue = new();
    private readonly Queue<Behavior> enterQueue = new();
    private readonly Queue<Behavior> leaveQueue = new();

    public World()
    {
        CollectionChanged += handleCollectionChanged;
    }

    public void Update(TimeSpan elapsed)
    {
        while (enterQueue.TryDequeue(out var behavior))
        {
            behavior.Load();
            behaviors.Add(behavior);

            if (behavior is Drawable drawable)
            {
                drawables.Add(drawable);
            }
        }

        while (leaveQueue.TryDequeue(out var behavior))
        {
            behavior.Unload();
            behaviors.Remove(behavior);

            if (behavior is Drawable drawable)
            {
                drawables.Remove(drawable);
            }
        }

        foreach (var behavior in behaviors)
        {
            behavior.Update(elapsed);
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
                if (BoundingFrustum.Contains(camera.Frustum, light.Frustum) == Containment.Disjoint)
                {
                    continue;
                }

                renderQueue.Clear();

                foreach (var drawable in drawables)
                {
                    drawable.Draw(renderQueue.Begin(camera, drawable));
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
            foreach (var node in args.NewItems!.Cast<Node>())
            {
                load(node);
            }
        }

        if (args.Action == NotifyCollectionChangedAction.Remove)
        {
            foreach (var node in args.OldItems!.Cast<Node>())
            {
                unload(node);
            }
        }

        if (args.Action == NotifyCollectionChangedAction.Reset)
        {
            foreach (var node in this)
            {
                unload(node);
            }
        }
    }

    private void load(Node node)
    {
        foreach (var child in node)
        {
            load(child);
        }

        if (node is Behavior behavior)
        {
            enterQueue.Enqueue(behavior);
        }

        if (node is World world)
        {
            worlds.Add(world);
        }
        else
        {
            node.CollectionChanged += handleCollectionChanged;
        }

        if (node is Light light)
        {
            lights.Add(light);
        }

        if (node is Camera camera)
        {
            cameras.Add(camera);
        }
    }

    private void unload(Node node)
    {
        foreach (var child in node)
        {
            unload(child);
        }

        if (node is Behavior behavior)
        {
            leaveQueue.Enqueue(behavior);
        }

        if (node is World world)
        {
            worlds.Add(world);
        }
        else
        {
            node.CollectionChanged -= handleCollectionChanged;
        }

        if (node is Light light)
        {
            lights.Remove(light);
        }

        if (node is Camera camera)
        {
            cameras.Remove(camera);
        }
    }
}
