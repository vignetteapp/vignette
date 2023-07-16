// Copyright (c) Cosyne
// Licensed under GPL 3.0 with SDK Exception. See LICENSE for details.

using System;
using System.Numerics;
using Sekai.Mathematics;
using Vignette.Graphics;

namespace Vignette;

public class Camera : Node, IProjector
{
    /// <summary>
    /// The near plane distance.
    /// </summary>
    public float NearPlane = 0.1f;

    /// <summary>
    /// The far plane distance.
    /// </summary>
    public float FarPlane = 1000f;

    /// <summary>
    /// The camera's aspect ratio.
    /// </summary>
    /// <remarks>Used when <see cref="ProjectionMode"/> is <see cref="CameraProjectionMode.Perspective"/>.</remarks>
    public float AspectRatio = 16.0f / 9.0f;

    /// <summary>
    /// The camera's field of view.
    /// </summary>
    public float FieldOfView = 60.0f;

    /// <summary>
    /// The camera's view size.
    /// </summary>
    public SizeF ViewSize = SizeF.Zero;

    /// <summary>
    /// The camera's view scale.
    /// </summary>
    public Vector2 ViewScale = Vector2.One;

    /// <summary>
    /// The camera's top left position.
    /// </summary>
    public Vector2 ViewTopLeft = Vector2.Zero;

    /// <summary>
    /// The camera projection mode.
    /// </summary>
    public CameraProjectionMode ProjectionMode = CameraProjectionMode.OrthographicOffCenter;

    /// <summary>
    /// The camera's rendering groups.
    /// </summary>
    public RenderGroup Groups { get; set; } = RenderGroup.Default;

    /// <summary>
    /// The camera's view frustum.
    /// </summary>
    public BoundingFrustum Frustum => BoundingFrustum.FromMatrix(((IProjector)this).ProjMatrix);

    Matrix4x4 IProjector.ViewMatrix => Matrix4x4.CreateLookAt(Position, Position + Vector3.Transform(-Vector3.UnitZ, Matrix4x4.CreateFromYawPitchRoll(Rotation.Y, Rotation.X, Rotation.Z)), Vector3.UnitY);

    Matrix4x4 IProjector.ProjMatrix => ProjectionMode switch
    {
        CameraProjectionMode.Perspective => Matrix4x4.CreatePerspective(ViewSize.Width * ViewScale.X, ViewSize.Height * ViewScale.Y, NearPlane, FarPlane),
        CameraProjectionMode.PerspectiveOffCenter => Matrix4x4.CreatePerspectiveOffCenter(ViewTopLeft.X, ViewSize.Width * ViewScale.X, ViewSize.Height * ViewScale.Y, ViewTopLeft.Y, NearPlane, FarPlane),
        CameraProjectionMode.PerspectiveFieldOfView => Matrix4x4.CreatePerspectiveFieldOfView(FieldOfView, AspectRatio, NearPlane, FarPlane),
        CameraProjectionMode.Orthographic => Matrix4x4.CreateOrthographic(ViewSize.Width * ViewScale.X, ViewSize.Height * ViewScale.Y, NearPlane, FarPlane),
        CameraProjectionMode.OrthographicOffCenter => Matrix4x4.CreateOrthographicOffCenter(ViewTopLeft.X, ViewSize.Width * ViewScale.X, ViewSize.Height * ViewScale.Y, ViewTopLeft.Y, NearPlane, FarPlane),
        _ => throw new InvalidOperationException($"Unknown {nameof(ProjectionMode)} {ProjectionMode}."),
    };
}

/// <summary>
/// An enumeration of camera projection modes.
/// </summary>
public enum CameraProjectionMode
{
    /// <summary>
    /// Orthographic projection.
    /// </summary>
    Orthographic,

    /// <summary>
    /// Custom orthographic projection.
    /// </summary>
    OrthographicOffCenter,

    /// <summary>
    /// Perspective projection.
    /// </summary>
    Perspective,

    /// <summary>
    /// Custom perspective projection.
    /// </summary>
    PerspectiveOffCenter,

    /// <summary>
    /// Perspective field of view projection.
    /// </summary>
    PerspectiveFieldOfView,
}
