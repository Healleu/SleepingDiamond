extends Node2D

var _shared_Area: RID
var _circle_shape: RID

var _shapes: Array = []
var _areas: Array = []
var _forms: Array = []

func _ready() -> void:
	#Create_collision(Vector2.ZERO)
	#Create_collision(Vector2(100,100))
	#Create_collision(Vector2(200,200))
	return

var xform: Transform2D
func Create_collision(pos: Vector2):
	xform = Transform2D(1.0, pos)
	_circle_shape = PhysicsServer2D.circle_shape_create()
	_shared_Area = PhysicsServer2D.area_create()
	_areas.append(_shared_Area)
	_shapes.append(_circle_shape)
	_forms.append(xform)
	PhysicsServer2D.shape_set_data(_circle_shape, 32)
	PhysicsServer2D.area_add_shape(_shared_Area, _circle_shape, xform)
	PhysicsServer2D.area_set_space(_shared_Area, get_world_2d().space)
	PhysicsServer2D.area_set_monitorable(_shared_Area, true)
	
	
func _input(event: InputEvent) -> void:
	if event is InputEventMouseButton:
		if event.pressed:
			mouse_detect()

func mouse_detect():
	var space = get_world_2d().get_direct_space_state()
	var mousePos = get_global_mouse_position()
	var a: PhysicsShapeQueryParameters2D = PhysicsShapeQueryParameters2D.new()
	a.collide_with_areas = true
	a.collide_with_bodies = true
	a.collision_mask = 1
	a.transform = Transform2D(1.0, mousePos)
	var shape = CircleShape2D.new()
	shape.radius = 1
	a.shape = shape
	print(space.intersect_shape(a))

func _draw() -> void:
	for i in range(_areas.size()):
		print(i)
		print(_forms[i].origin)
		draw_circle(_forms[i].origin, 32, 0x0099b36b)
