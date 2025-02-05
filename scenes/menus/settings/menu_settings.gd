extends Control

func _ready() -> void :
	MenuSystem.set_settings_menu(self)
	return

func _on_h_slider_volume_master_value_changed(value: float) -> void:
	SettingsSystem.volume_master = value
	return

func _on_h_slider_volume_musics_value_changed(value: float) -> void:
	SettingsSystem.volume_music = value
	return

func _on_h_slider_volume_sound_value_changed(value: float) -> void:
	SettingsSystem.volume_sound = value
	return

func _on_button_back_pressed() -> void:
	MenuSystem.hide_settings_menu()
	MenuSystem.show_actual_scene()
	return
