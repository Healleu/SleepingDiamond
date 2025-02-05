extends Node2D

var mouse_pressed : bool = false
var mouse_position : Vector2 = Vector2.ZERO
var mouse_clic_position : Vector2 = Vector2.ZERO
var mouse_release_position : Vector2 = Vector2.ZERO
var mouse_threshold_position : Vector2 = Vector2(16, 16)

func _input(event : InputEvent) -> void :
	if event is InputEventMouseButton :
		# mouse pressed
		if event.pressed and event.button_index == MOUSE_BUTTON_LEFT :
			mouse_pressed = true
			mouse_clic_position = get_viewport().get_mouse_position()
			
		# mouse release
		if not event.pressed and event.button_index == MOUSE_BUTTON_LEFT :
			mouse_pressed = false
			mouse_release_position = get_viewport().get_mouse_position()
			var diff_mouse_position : Vector2 = abs(mouse_release_position - mouse_clic_position)
			if max(diff_mouse_position.x, diff_mouse_position.y) > float(64 / 2) :
				print("span")
			else :
				print("simple clic")
			queue_redraw()
			
	if event is InputEventMouseMotion :
		if mouse_pressed :
			mouse_position = get_viewport().get_mouse_position()
			queue_redraw()

func _draw() -> void:
	if mouse_pressed :
		draw_rect(Rect2(mouse_clic_position - get_viewport_transform().get_origin(), mouse_position - mouse_clic_position), Color.GRAY, false)
