extends Control

@export var main_menu_scene : PackedScene = null

func _ready() -> void :
	MenuSystem.set_pause_menu(self)
	return

func _on_button_resume_pressed() -> void :
	get_tree().set_pause(false)
	return

func _on_button_settings_pressed() -> void :
	MenuSystem.show_settings_menu()
	return

func _on_button_main_menu_pressed() -> void :
	if main_menu_scene :
		MenuSystem.replace_actual_scene(main_menu_scene)
	else :
		push_error("No main menu scene defined for pause menu")
	return
