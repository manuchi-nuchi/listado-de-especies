shader_type canvas_item;

uniform sampler2D screen_texture : hint_screen_texture;
uniform sampler2D noise : repeat_enable;
uniform float strength = 1.;
uniform float speed = 1.;
uniform float alpha = 1.;

void fragment() {
	vec2 offset = vec2(texture(noise, SCREEN_UV + vec2(TIME * speed, 0)).r) * strength;
	COLOR = texture(screen_texture, SCREEN_UV + offset);
	
	COLOR.a = texture(noise, SCREEN_UV + vec2(TIME * speed, 0) * strength).r * alpha;
}
