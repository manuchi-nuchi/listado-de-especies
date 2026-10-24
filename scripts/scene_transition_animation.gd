class_name SceneTransitionAnimation
extends Node

@export var fade_duration: float = 1.0
@export var pause_duration: float = 0.5
@export var ease_curve: Curve
@export var target: CanvasItem

var material: ShaderMaterial

func _ready() -> void:
	material = target.material as ShaderMaterial

	Signals.change_scene.connect(play_scene_transition)

func _exit_tree() -> void:
	if Signals.change_scene.is_connected(play_scene_transition):
		Signals.change_scene.disconnect(play_scene_transition)

func fade_in() -> void:
	place()
	await animation(1.0, 0.0)

func fade_out() -> void:
	place()
	await animation(0.0, 1.0)

func pause() -> void:
	await get_tree().create_timer(pause_duration).timeout

func play_scene_transition() -> void:
	Globals.changing_scene = true

	await fade_in()

	change_scene()

	await pause()

	await fade_out()

	Globals.changing_scene = false

func place() -> void:
	(target.get_parent() as Control).position = Globals.transition_position

func change_scene() -> void:
	get_tree().change_scene_to_file(Globals.new_scene_path)

func animation(from: float, to: float) -> void:
	var t: float = 0.0
	var start_time: int = Time.get_ticks_msec()

	while t < fade_duration:
		t = (Time.get_ticks_msec() - start_time) / 1000.0
		var v: float = lerpf(from, to, ease_curve.sample(t / fade_duration))
		material.set_shader_parameter("openness", v)

		await get_tree().process_frame

	material.set_shader_parameter("openness", to)
