extends Node

const BUS_INDEX_MASTER : int = 0
const NO_EXIST_BUS_INDEX : int = -1

var _bus_index_music : int = -1
var _bus_index_sound : int = -1

func _ready() -> void :
	_bus_index_music = AudioServer.get_bus_index("Music")
	_bus_index_sound = AudioServer.get_bus_index("Sound")
	return

var volume_master : float = 1.0 : 
	set(value) :
		volume_master = value
		AudioServer.set_bus_volume_db(BUS_INDEX_MASTER, linear_to_db(volume_master))

var volume_music : float = 1.0 :
	set(value) :
		volume_music = value
		if _bus_index_music > NO_EXIST_BUS_INDEX :
			AudioServer.set_bus_volume_db(_bus_index_music, linear_to_db(volume_music))

var volume_sound : float = 1.0 :
	set(value) :
		volume_sound = value
		if _bus_index_sound > NO_EXIST_BUS_INDEX :
			AudioServer.set_bus_volume_db(_bus_index_sound, linear_to_db(volume_sound))
