extends Node

# required scene
var _actual_scene : CanvasItem = null
var _load_save_scene : Control = null
var _pause_menu : Control = null
var _settings_menu : Control = null

#
var _scene : Dictionary = {}

@onready var _main : Node = get_node_or_null("/root/Main")

func set_scene(scene_name : String, scene : PackedScene) -> void :
	_scene[scene_name] = scene
	return

func set_actual_scene(scene : CanvasItem) -> void :
	_actual_scene = scene
	return

func get_actual_scene() -> CanvasItem :
	return _actual_scene

func show_actual_scene() -> void :
	if _actual_scene :
		_actual_scene.show()
	return

func hide_actual_scene() -> void :
	if _actual_scene :
		_actual_scene.hide()
	return

func replace_actual_scene(scene : PackedScene) -> void :
	if _actual_scene :
		_actual_scene.call_deferred("queue_free")
	if _main :
		if scene :
			var _new_scene = scene.instantiate()
			_main.call_deferred("add_child", _new_scene)
			_actual_scene = _new_scene
	return

func set_pause_menu(scene : Control) -> void :
	_pause_menu = scene
	return

func get_pause_menu() -> Control :
	return _pause_menu

func show_pause_menu() -> void :
	if _pause_menu :
		_pause_menu.show()
	return

func hide_pause_menu() -> void : 
	if _pause_menu :
		_pause_menu.hide()
	return
	
func set_settings_menu(scene : Control) -> void :
	_settings_menu = scene
	return
	
func get_settings_menu() -> Control :
	return _settings_menu
	
func show_settings_menu() -> void :
	if _settings_menu :
		_settings_menu.show()
	return

func hide_settings_menu() -> void : 
	if _settings_menu :
		_settings_menu.hide()
	return

func set_load_save_scene(scene : Control) -> void :
	_load_save_scene = scene
	return

func get_load_save_scene() -> Control :
	return _load_save_scene

func show_load_save_scene() -> void :
	if _load_save_scene :
		_load_save_scene.show()
	return

func hide_load_save_scene() -> void :
	if _load_save_scene :
		_load_save_scene.hide()
	return
