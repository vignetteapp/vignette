// Copyright (c) The Vignette Authors
// Licensed under GPL-3.0 (With SDK Exception). See LICENSE for details.

using System;
using NUnit.Framework;

[AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
public class RequiresOpenCVRuntimeAttribute : CategoryAttribute
{
    public RequiresOpenCVRuntimeAttribute() : base("RequiresOpenCVRuntimeTest") { }
}
