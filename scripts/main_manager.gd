class_name MainManager
extends Node

@export var list: SpeciesList
@export var labels: Array[SpeciesLabel]
@export var scientific_name_label: RichTextLabel
@export var image: TextureRect

@export var bg: CanvasItem
@export var watercolor: CanvasItem
@export var frame: CanvasItem
@export var animation_duration: float = 0.5
@export var ease_curve: Curve

@export_file("*.tscn") var list_selection_scene_path: String
@export var transition_origin: Control

var current: int = 0
var offset: int = -2

var t: float = INF
var v: float
var previous1: Color
var previous2: Color
var previous3: Color

var current_plant: Species:
	get:
		return list.at(current)

func _ready() -> void:
	if Globals.list != null:
		list = Globals.list

	Globals.transition_position = transition_origin.global_position

	set_species()

func _process(delta: float) -> void:
	if t > animation_duration:
		return

	t += delta
	v = ease_curve.sample(t / animation_duration)
	bg.modulate = previous1.lerp(current_plant.color1, v)
	watercolor.modulate = previous2.lerp(current_plant.color2, v)
	frame.self_modulate = previous3.lerp(current_plant.color3, v)

func set_species() -> void:
	for i in labels.size():
		labels[i].new_text(list.at(bounded_index(i)).common_name)

	scientific_name_label.text = current_plant.scientific_name
	image.texture = current_plant.image

	set_colors()

func set_colors() -> void:
	t = 0.0
	previous1 = bg.modulate
	previous2 = watercolor.modulate
	previous3 = frame.self_modulate

func next_image() -> void:
	image.texture = current_plant.next_image

func previous_image() -> void:
	image.texture = current_plant.previous_image

func _unhandled_input(event: InputEvent) -> void:
	if Globals.changing_scene:
		return

	var key := event as InputEventKey
	if key == null or not key.pressed or key.echo:
		return

	match key.keycode:
		KEY_DOWN:
			current = (current + 1) % list.size()
			set_species()
		KEY_UP:
			current = (current - 1 + list.size()) % list.size()
			set_species()
		KEY_RIGHT:
			next_image()
		KEY_LEFT:
			previous_image()
		KEY_ESCAPE:
			Globals.new_scene_path = list_selection_scene_path
			Signals.change_scene.emit()

func bounded_index(i: int) -> int:
	return (current + offset + i + list.size()) % list.size()
