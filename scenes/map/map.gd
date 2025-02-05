extends Node2D

enum BLOC{
	DIRT = 0,
	GRASS = 1,
	ROCK = 2,
}

var ATLAS_COORDS_DIRT : Array[Vector2i] = []
var ATLAS_COORDS_STONE : Array[Vector2i] = []
const CELL_SIZE : int = 256
const MAP_WIDTH : int = 320
const MAP_HEIGHT : int = 180

const FLOWER_POOL : Array[Vector2i] = [Vector2i(2, 3)]
const TREE_POOL : Array[Vector2i] = [Vector2i(0, 3)]

@export_category("Temperature")
@export var temp_treshold_low : float = 0.2
@export var temp_treshold_high : float = 0.4

@export_category("Height")
@export var height_treshold : float = 0.3

var temperature_noise : FastNoiseLite = FastNoiseLite.new()
var height_noise : FastNoiseLite = FastNoiseLite.new()


@export var bush_ratio : float = 0.1
@onready var tilemap_layer_manager : TilemapLayerManager = get_node_or_null("TilemapLayerManager")
@onready var plant_manager : PlantManager = get_node_or_null("PlantManager")

var random : RandomNumberGenerator = RandomNumberGenerator.new()
var path = []

func _ready() -> void :
	for i in 16 :
		ATLAS_COORDS_DIRT.append(Vector2i(i, 0))
		ATLAS_COORDS_STONE.append(Vector2i(i, 1))
	
	random.randomize()
	temperature_noise.set_seed(500)
	height_noise.set_seed(100)
	PathfindingAStar.setup(Rect2i(0, 0, MAP_WIDTH, MAP_HEIGHT), Vector2(CELL_SIZE, CELL_SIZE), AStarGrid2D.DiagonalMode.DIAGONAL_MODE_NEVER)
	_generate_map()
	return

func _generate_map() -> void :
	for x in MAP_WIDTH :
		for y in MAP_HEIGHT :
			var temperature_noise_value = abs(temperature_noise.get_noise_2d(x, y))
			var heigh_noise_value = abs(height_noise.get_noise_2d(x, y))
			if temperature_noise_value < temp_treshold_low :
				tilemap_layer_manager.set_cell(0, Vector2(x, y), 0, Vector2(BLOC.DIRT, 0))
				if heigh_noise_value > height_treshold :
					tilemap_layer_manager.set_cell(1, Vector2(x, y), 0, Vector2(0, 0))
			elif temperature_noise_value > temp_treshold_high :
				tilemap_layer_manager.set_cell(0, Vector2(x, y), 0, Vector2(BLOC.ROCK, 0))
				if heigh_noise_value > height_treshold :
					tilemap_layer_manager.set_cell(1, Vector2(x, y), 0, Vector2(0, 1))
			else :
				tilemap_layer_manager.set_cell(0, Vector2(x, y), 0, Vector2(BLOC.GRASS, 0))
	var tile_weight : int = 0
	for x in MAP_WIDTH :
		for y in MAP_HEIGHT :
			var tile_value : Vector2i = tilemap_layer_manager.get_cell_atlas_coord(1, Vector2i(x, y))
			if tile_value.y == 0 :
				tile_weight = tilemap_layer_manager.get_cell_weight(1, Vector2i(x, y), ATLAS_COORDS_DIRT)
				tilemap_layer_manager.set_cell(1, Vector2i(x, y), 0, Vector2(tile_weight, 0))
			elif tile_value.y == 1 :
				tile_weight = tilemap_layer_manager.get_cell_weight(1, Vector2i(x, y), ATLAS_COORDS_STONE)
				tilemap_layer_manager.set_cell(1, Vector2i(x, y), 0, Vector2(tile_weight, 1))
			else :
				if random.randf() > 0.95 :
					# flower
					tilemap_layer_manager.set_cell(1, Vector2i(x, y), 0, Vector2(2, 3))
					#plant_manager.add_plant(Vector2i(x, y), Vector2(2, 3))
				elif random.randf() < 0.05 :
					# bush
					tilemap_layer_manager.set_cell(1, Vector2i(x, y), 0, Vector2(0, 3))
					#plant_manager.add_plant(Vector2i(x, y), Vector2(0, 3))
	return

func _convert_position_to_cell(pos : Vector2i) -> Vector2i :
	return pos / Vector2i(CELL_SIZE, CELL_SIZE)

func set_bush() -> void :
	var rand : RandomNumberGenerator = RandomNumberGenerator.new()
	rand.randomize()
	var rand_value = rand.randf()
	if rand_value < bush_ratio :
		var x = 5
		var y = 5
		tilemap_layer_manager.set_cell(1, Vector2(x, y), 0, Vector2(0, 0))
		#plant_manager.add_plant(Vector2(x, y), Plant.PlantType.APPLE_TREE, Plant.PlantState.GROWING)
		PathfindingAStar.astar_grid.set_point_solid(Vector2(x, y))
	return
