extends Node2D

var unit_texture: Texture2D = preload("icon.svg")
var unit_size: Vector2 = Vector2(128, 128) *4
var unit_mid: Vector2 = Vector2(8.0, 8.0) * 4
var unit_count: int = 160000/4
var newpos : Array[Vector2] = []
var quad_mesh: QuadMesh = QuadMesh.new()
var unit_rid: RID = RenderingServer.canvas_item_create()
var mesh_rid: RID = RenderingServer.multimesh_create()

var pscale = Vector2(4,4)

var area 
var shape
var texture
var ci_rid
var body

func _ready() -> void:
	#quad_mesh.size = unit_size
	#RenderingServer.multimesh_set_mesh(mesh_rid, quad_mesh.get_rid())
	#RenderingServer.multimesh_allocate_data(mesh_rid, unit_count, RenderingServer.MULTIMESH_TRANSFORM_2D)
	#RenderingServer.canvas_item_set_parent(unit_rid, get_canvas_item())
	#var x = 0
	#var y = 0
	#for unit_index in unit_count:
		#if x==400:
			#y+=1
			#x=0
		#RenderingServer.multimesh_instance_set_transform_2d(mesh_rid, unit_index, Transform2D(0, unit_size * Vector2(x,y)))
		#x += 1
	##RenderingServer.canvas_item_add_multimesh(unit_rid, mesh_rid, unit_texture)
# Create a canvas item, child of this node.
	ci_rid = RenderingServer.canvas_item_create()
	# Make this node the parent.
	RenderingServer.canvas_item_set_parent(ci_rid, get_canvas_item())
	# Draw a texture on it.
	# Remember, keep this reference.
	texture = load("res://icon.svg")
	# Add it, centered.
	RenderingServer.canvas_item_add_texture_rect(ci_rid, Rect2(-texture.get_size() / 2, texture.get_size()), texture)
	# Add the item, rotated 45 degrees and translated.
	var xform = Transform2D().rotated(deg_to_rad(45)).translated(Vector2(20, 30))
	RenderingServer.canvas_item_set_transform(ci_rid, xform)
	body = PhysicsServer2D.body_create()
	PhysicsServer2D.body_set_mode(body, PhysicsServer2D.BODY_MODE_RIGID)
	# Add a shape.
	shape = PhysicsServer2D.rectangle_shape_create()
	# Set rectangle extents.
	PhysicsServer2D.shape_set_data(shape, Vector2(10, 10))
	# Make sure to keep the shape reference!
	PhysicsServer2D.body_add_shape(body, shape)
	# Set space, so it collides in the same space as current scene.
	PhysicsServer2D.body_set_space(body, get_world_2d().space)
	# Move initial position.
	PhysicsServer2D.body_set_state(body, PhysicsServer2D.BODY_STATE_TRANSFORM, Transform2D(0, Vector2(10, 20)))
	# Add the transform callback, when body moves
	# The last parameter is optional, can be used as index
	# if you have many bodies and a single callback.
	PhysicsServer2D.body_set_force_integration_callback(body, _body_moved, 0)
	#PhysicsServer2D.area_set_area_monitor_callback(area, self, 'debug_overlap')
	#var ran = RandomNumberGenerator.new()
	#ran.randomize()
	#newpos.resize(2000)
	#for i in 2000 :
		#newpos[i] = Vector2(ran.randi_range(0,800), ran.randi_range(0,800))
func _body_moved(state, index):
	# Created your own canvas item, use it here.
	RenderingServer.canvas_item_set_transform(ci_rid, state.transform)
