class_name Species
extends Resource

@export var common_name: String
@export var scientific_name: String
@export var images: Array[Texture2D]
var current_image: int = 0

@export var color1: Color = Color.from_hsv(0.2, 0.3, 0.8)
@export var color2: Color = Color.from_hsv(0.1, 0.55, 0.65)
@export var color3: Color = Color.from_hsv(0.0, 0.8, 0.4)

var next_image: Texture2D:
	get:
		current_image = (current_image + 1) % images.size()
		return images[current_image]

var previous_image: Texture2D:
	get:
		current_image = (current_image - 1 + images.size()) % images.size()
		return images[current_image]

var image: Texture2D:
	get:
		return images[current_image]