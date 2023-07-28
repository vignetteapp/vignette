// Copyright (c) Cosyne
// Licensed under GPL 3.0 with SDK Exception. See LICENSE for details.

using Sekai;
using Sekai.GLFW;
using Sekai.OpenAL;
using Sekai.OpenGL;
using Vignette;

var options = new VignetteGameOptions();
options.UseOpenAL();
options.UseOpenGL();
options.UseScripts();

if (RuntimeInfo.IsDesktop)
{
    options.UseGLFW();
    options.Window.Title = "Vignette";
}

var game = new VignetteGame(options);
game.Run();
