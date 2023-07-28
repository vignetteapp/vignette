// Copyright (c) Cosyne
// Licensed under GPL 3.0 with SDK Exception. See LICENSE for details.

using System;

namespace Vignette.Scripting;

/// <summary>
/// Denotes that a given <see langword="class"/> or <see langword="struct"/> member is visible in scripting.
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Event | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
internal sealed class ScriptVisibleAttribute : Attribute
{
}
