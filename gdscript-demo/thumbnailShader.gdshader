shader_type canvas_item;

uniform sampler2D base_tex;
uniform bool selected;

void fragment() {
    vec4 base = texture(base_tex, UV);
	if(selected) {
    if (UV.x < 0.01 || UV.x>0.99 || UV.y<0.01 || UV.y>0.99) { // if base transparent - result transparent
        COLOR = vec4(1.0, 0.0, 0.0, 1.0);
    } else { // if overlay is not transparent - draw overlay color
        COLOR = base;
    }}
	else {
		COLOR=base;
	}
}