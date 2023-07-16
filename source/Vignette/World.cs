// Copyright (c) Cosyne
// Licensed under GPL 3.0 with SDK Exception. See LICENSE for details.

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Numerics;
using Sekai.Mathematics;
using Vignette.Collections;
using Vignette.Graphics;

namespace Vignette;

/// <summary>
/// A <see cref="Node"/> that presents and processes its children.
/// </summary>
public class World : Behavior
{
    protected override Matrix4x4 WorldMatrix => LocalMatrix;

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
        }

        while (behaviorUnloadQueue.TryDequeue(out var node))
        {
            node.Unload();
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

        // Shadow Map Pass

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
                    drawable.Draw(renderQueue.Begin(light, drawable));
                }

                renderer.Draw(renderQueue);
            }
        }

        // Lighting Pass

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
            foreach (var node in args.NewItems!.OfType<Node>())
            {
                load(node);
            }
        }

        if (args.Action == NotifyCollectionChangedAction.Remove)
        {
            foreach (var node in args.OldItems!.OfType<Node>())
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
        foreach (var child in node.GetNodes<Behavior>())
        {
            load(child);
        }

        if (node is Behavior behavior)
        {
            behaviors.Add(behavior);
            behaviorLoadQueue.Enqueue(behavior);
        }

        if (node is Drawable drawable)
        {
            drawables.Add(drawable);
        }

        if (node is Light light)
        {
            lights.Add(light);
        }

        if (node is Camera camera)
        {
            cameras.Add(camera);
        }

        if (node is World world)
        {
            worlds.Add(world);
        }
        else
        {
            node.CollectionChanged += handleCollectionChanged;
        }
    }

    private void unload(Node node)
    {
        foreach (var child in node.GetNodes<Behavior>())
        {
            unload(child);
        }

        if (node is Behavior behavior)
        {
            behaviors.Remove(behavior);
            behaviorUnloadQueue.Enqueue(behavior);
        }

        if (node is Drawable drawable)
        {
            drawables.Remove(drawable);
        }

        if (node is Light light)
        {
            lights.Remove(light);
        }

        if (node is Camera camera)
        {
            cameras.Remove(camera);
        }

        if (node is World world)
        {
            worlds.Add(world);
        }
        else
        {
            node.CollectionChanged -= handleCollectionChanged;
        }
    }
}
