shader_type canvas_item;

instance uniform vec3 color : source_color;
instance uniform float width : hint_range(0., 0.5) = 0.4;
instance uniform float height : hint_range(0., 0.5) = 0.4;
instance uniform float radius : hint_range(0., 1.) = 0.1;

const vec2 center = vec2(.5);
void fragment() {
	float r = min(width, height) * radius;
	vec2 uv = abs(UV - center);
	
	// inner part
	float x = step(uv.x, width);
	float y = step(uv.y, height);
	
	//float border = min(step(uv.x, width - radius) * step(uv.y, height - radius) + .5, 1.);
	
	// rounded corner
	float corner_x = min(step(uv.x, width - r) + .5, 1.);
	float corner_y = min(step(uv.y, height - r) + .5, 1.);
	float corner = step(corner_x * corner_y, .3) * x * y;
	vec2 radius_center = vec2(width - r, height - r);
	float rounded_corner = min(step(distance(uv, radius_center), r), corner);
	float outside_corner = 1. - max(corner - rounded_corner, 0.);
	
	COLOR.rgb = color;
	COLOR.a = x * y * outside_corner;
}
