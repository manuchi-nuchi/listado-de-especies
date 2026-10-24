class_name ListButton
extends Control

@export var list: SpeciesList = null
@export var underline: Control
@export var target_scale_value: float = 0.005
@export var max_width: float = 62000.0
@export var animation_duration: float = 1.0
@export var back_duration_scale: float = 2.0
@export var ease_curve: Curve

var target_scale: Vector2
var min_width: float = 512.0
var start_width: float
var target_width: float
var actual_duration: float
var t: float = INF
var v: float
var size_cache: Vector2

func _ready() -> void:
	target_scale = Vector2(target_scale_value, target_scale_value)
	actual_duration = animation_duration

	size_cache = underline.size

func _process(delta: float) -> void:
	if t > actual_duration:
		return

	t += delta
	v = ease_curve.sample(t / actual_duration)
	size_cache.x = lerpf(start_width, target_width, v)
	underline.size = size_cache
	underline.position = -size_cache / 2.0

	underline.scale = Vector2.ZERO if underline.size.x <= 513.0 else target_scale

func select() -> void:
	t = (actual_duration - t) / back_duration_scale if t < actual_duration else 0.0
	actual_duration = animation_duration
	start_width = min_width
	target_width = max_width

func deselect(insta: bool = false) -> void:
	if insta:
		size_cache.x = min_width
		underline.size = size_cache
		underline.position = -size_cache / 2.0
		underline.scale = Vector2.ZERO
	else:
		t = (actual_duration - t) * back_duration_scale if t < actual_duration else 0.0
		actual_duration = animation_duration * back_duration_scale
		start_width = max_width
		target_width = min_width