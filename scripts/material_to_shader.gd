class_name MaterialToShader
extends TextureRect

@export var gradient: Texture2D

func _ready() -> void:
	var mat := material.duplicate() as ShaderMaterial
	mat.set_shader_parameter("gradient", gradient)
	material = mat