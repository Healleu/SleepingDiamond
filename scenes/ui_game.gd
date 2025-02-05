extends CanvasLayer

@onready var label_fps : Label = $LabelFPS

var zoom_v = Vector2(1.0,1.0)

func _physics_process(_delta : float) -> void :
	label_fps.set_text(str(Performance.get_monitor(Performance.TIME_FPS)))
	return

func _on_camera_zoom_changed(zoom_value: Vector2) -> void:
	zoom_v = zoom_value
	pass # Replace with function body.
