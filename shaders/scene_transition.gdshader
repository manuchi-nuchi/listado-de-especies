shader_type canvas_item;

uniform float radius : hint_range(0., 2.) = 1.;
uniform float openness : hint_range(0., 1.) = 1.;
uniform float thickness : hint_range(0., .4) = .1;
uniform vec3 color : source_color;
uniform sampler2D noise;

const vec2 center = vec2(.5);
void fragment() {
	COLOR.rgb = color;
	
	float actual_radius = radius * openness;
	float distance_to_center = distance(UV, center);
	float outside = step(actual_radius, distance_to_center);
	float inside = smoothstep(actual_radius - thickness, actual_radius, distance_to_center) * (1. - outside);
	float noise_value = step(texture(noise, UV).r, inside);
	
	COLOR.a = min(outside + noise_value, 1);
}
