extends Control

@export var play_scene : PackedScene = null


func _ready() -> void :
	MenuSystem.set_actual_scene(self)
	return

func _on_button_play_pressed() -> void :
	if play_scene :
		MenuSystem.replace_actual_scene(play_scene)
	else :
		push_error("No scene defined for button PLAY")
	return

func _on_button_settings_pressed() -> void :
	MenuSystem.hide_actual_scene()
	MenuSystem.show_settings_menu()
	return

func _on_button_quit_pressed() -> void :
	get_tree().quit()
	return
