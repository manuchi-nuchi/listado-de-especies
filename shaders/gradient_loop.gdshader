shader_type canvas_item;

uniform sampler2D gradient : repeat_enable;
uniform float speed : hint_range(0., 1.) = 1.;
uniform float scale : hint_range(0., 1.) = 1.;

const float speedScale = 0.1f;
void fragment() {
	vec3 color = texture(gradient, vec2(UV.x * scale + TIME * speed * speedScale, UV.y)).rgb;
	COLOR.rgb = color;
}
