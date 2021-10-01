attribute vec4 a_position;
attribute vec2 a_texCoord;
varying vec2 v_texCoord;
uniform mat4 u_matrix;

void main()
{
    gl_Position = u_matrix * a_position;
    v_texCoord = a_texCoord;
    v_texCoord.y = 1.0 - v_texCoord.y;
}