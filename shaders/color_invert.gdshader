shader_type canvas_item;

uniform sampler2D screen_texture : hint_screen_texture;
uniform float attenuation : hint_range(0.05, 2.) = 1.;

void fragment() {
	COLOR.rgb = vec3(1.) - texture(screen_texture, SCREEN_UV).rgb / attenuation;
}
