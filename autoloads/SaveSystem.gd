extends Node

const ALLOWED_CHARACTERS = "abcdefghijklmnopqrstuvwxyz
						ABCDEFGHIJKLMNOPQRSTUVWXYZ
						0123456789
						=+/*-()[]{}-_#"
const SAVE_TEMPLATE_PATH : String = "res://saves/SaveTemplate.txt"
const SAVE_PATH : String = "res://saves/SavesFolder/"
const MINIMUM_SAVE_NAME_LENGTH : int = 10
const MAXIMUM_SAVE_NAME_LENGTH : int = 15

var _saves_data : Dictionary = {}
var _current_save_data : Dictionary = {}
var _current_save_name : String = ""

func _ready() -> void :
	#_load_saves()
	return

func _load_saves() -> void :
	var _saves_name : PackedStringArray = DirAccess.get_files_at(SAVE_PATH)
	for _save_name in _saves_name :
		if _save_name.get_extension() == "json" and not _save_name.contains("backup") :
			var _file : FileAccess = FileAccess.open(SAVE_PATH + _save_name, FileAccess.READ)
			var _dict_data : Variant = JSON.parse_string(_file.get_as_text())
			if _dict_data == null :
				push_error("Fail to read save from : " + _save_name.get_basename())
			else :
				_saves_data[_save_name.get_basename()] = _dict_data
	return

func _generate_save_name() -> String :
	var _save_name_lenght = randi_range(MINIMUM_SAVE_NAME_LENGTH, MAXIMUM_SAVE_NAME_LENGTH)
	var _save_name : String = ""
	for _char in range(_save_name_lenght):
		_save_name += ALLOWED_CHARACTERS[randi() % ALLOWED_CHARACTERS.length()]
	return _save_name

func save_game() -> bool :
	var _error : bool = false
	# remove older save
	_error = DirAccess.remove_absolute(SAVE_PATH + _current_save_name + ".json")
	if _error :
		push_error("Fail to remove older save")
		return true
	
	# create new save
	var _file = FileAccess.open(SAVE_PATH + _current_save_name + ".json", FileAccess.WRITE)
	if _file == null :
		push_error("Fail to create new save")
		return true
	_file.store_string(JSON.stringify(_current_save_data))
	_file.close()
	return false

func set_current_save_data_from_name(_save_name : String) -> void :
	if _saves_data.has(_save_name) :
		_current_save_name = _save_name
		_current_save_data = _saves_data.get(_save_name)
	else :
		push_error("Fail to select save in saves selection")
	return

func create_new_save() -> void :
	_current_save_name = _generate_save_name()
	return

func get_saves_data() -> Dictionary :
	return _saves_data

func get_save_data() -> Dictionary :
	return _current_save_data

func _load_data(_save_name : String) -> bool :
	var _data : Dictionary = _saves_data.get(_saves_data, {})
	if _data.is_empty() :
		return false
	
	_current_save_data = _data
	return true
