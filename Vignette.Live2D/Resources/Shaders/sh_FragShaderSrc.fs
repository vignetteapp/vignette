#include "sh_Utils.h"

varying vec2 v_texCoord;
uniform sampler2D s_texture0;
uniform vec4 u_baseColor;

void main()
{
    vec4 color = toSRGB(texture2D(s_texture0 , v_texCoord)) * u_baseColor;
    gl_FragColor = vec4(color.rgb * color.a,  color.a);
}