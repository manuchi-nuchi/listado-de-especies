class_name ListSelection
extends Node

@export var buttons: Array[ListButton]
@export var bg: Array[CanvasItem]
@export var bg_fade_duration: float = 0.2
@export_file("*.tscn") var main_scene_path: String

var tweens: Array[Tween]
var current: Vector2i = Globals.current
var columns: int = 4
var rows: int = 3
var shown: Color = Color(1, 1, 1, 1)
var hidden: Color = Color(1, 1, 1, 0.01)

var current_index: int:
	get:
		return current.x + current.y * columns

var current_button: ListButton:
	get:
		return buttons[current_index]

func _ready() -> void:
	# need to wait 1 frame for control nodes to have a valid position
	await get_tree().process_frame
	Globals.transition_position = current_button.get_global_rect().get_center()

	for i in buttons.size():
		if i == current_index:
			buttons[i].select()
		else:
			buttons[i].deselect(true)

	tweens.resize(bg.size())

	for i in bg.size():
		hide_bg(i, true)

		if i == current.x:
			show_bg(i)

	Input.mouse_mode = Input.MOUSE_MODE_HIDDEN
	DisplayServer.window_set_mode(DisplayServer.WINDOW_MODE_FULLSCREEN)

func _unhandled_input(event: InputEvent) -> void:
	if Globals.changing_scene:
		return

	var key := event as InputEventKey
	if key == null or not key.pressed or key.echo:
		return

	match key.keycode:
		KEY_LEFT:
			current_button.deselect()
			hide_bg(current.x)
			current.x = (current.x - 1 + columns) % columns
			current_button.select()
			show_bg(current.x)
		KEY_RIGHT:
			current_button.deselect()
			hide_bg(current.x)
			current.x = (current.x + 1) % columns
			current_button.select()
			show_bg(current.x)
		KEY_UP:
			current_button.deselect()
			current.y = (current.y - 1 + rows) % rows
			current_button.select()
		KEY_DOWN:
			current_button.deselect()
			current.y = (current.y + 1) % rows
			current_button.select()
		KEY_ENTER:
			if current_button.list != null:
				Globals.list = current_button.list
				Globals.current = current
				Globals.new_scene_path = main_scene_path
				Globals.transition_position = current_button.get_global_rect().get_center()
				Signals.change_scene.emit()
		KEY_ESCAPE:
			get_tree().quit()

func show_bg(index: int) -> void:
	if tweens[index]:
		tweens[index].kill()
	tweens[index] = create_tween()
	tweens[index].tween_interval(bg_fade_duration * 0.5)	# small wait before fading in, to give the previous bg time to fade out
	tweens[index].tween_property(bg[index], "modulate", shown, bg_fade_duration) \
		.set_trans(Tween.TRANS_SINE) \
		.set_ease(Tween.EASE_IN_OUT)

func hide_bg(index: int, insta: bool = false) -> void:
	if insta:
		bg[index].modulate = hidden
	else:
		if tweens[index]:
			tweens[index].kill()
		tweens[index] = create_tween()
		tweens[index].tween_property(bg[index], "modulate", hidden, bg_fade_duration) \
			.set_trans(Tween.TRANS_SINE) \
			.set_ease(Tween.EASE_IN_OUT)
