class_name TilemapLayerManager extends Node2D

#signal updated()

var _layers : Array[TileMapLayer] = []
var _reversed_layers : Array[TileMapLayer] = []

func _ready() -> void :
	# get all layers
	for child_index in get_child_count() :
		_layers.append(get_child(child_index))
		_reversed_layers.append(get_child(child_index))
	_reversed_layers.reverse()
	return

func set_cell(layer : int, coords : Vector2i, source_id : int = -1, atlas_coords : Vector2i = Vector2i(-1, -1), alternative_tile : int = 0) -> void :
	_layers[layer].set_cell(coords, source_id, atlas_coords, alternative_tile)
	return
	
func get_cell_atlas_coord(layer : int, coords : Vector2i) -> Vector2i :
	return _layers[layer].get_cell_atlas_coords(coords)
	
func get_first_cell_atlas_coord(coords : Vector2i) -> Vector2i :
	var coord : Vector2i = Vector2i(-1, -1)
	for layer : TileMapLayer in _reversed_layers :
		coord = layer.get_cell_atlas_coords(coords)
		if coord != Vector2i(-1, -1) :
			break
	return coord

func get_cell_weight(layer : int, coords : Vector2i, atlas_coords : Array = []) -> int :
	var weight : int = 0
	var index : int = 0
	var atlas_coord : Vector2i = Vector2i(-1, -1)
	var neighbours : Array[Vector2i] = _layers[layer].get_surrounding_cells(coords)
	for neighbour : Vector2i in neighbours :
		atlas_coord  = get_cell_atlas_coord(layer, neighbour)
		if atlas_coords.has(atlas_coord) :
			weight += int(pow(2, index))
		index += 1
	return weight
