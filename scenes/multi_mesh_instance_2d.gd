extends MultiMeshInstance2D


func _ready():
	# Create the multimesh.
	multimesh = MultiMesh.new()
	# Set the format first.
	multimesh.transform_format = MultiMesh.TRANSFORM_2D
	# Then resize (otherwise, changing the format is not allowed).
	multimesh.instance_count = 160000
	# Maybe not all of them should be visible at first.
	multimesh.visible_instance_count = -1
	var quad : QuadMesh = QuadMesh.new()
	quad.set_size(Vector2(128,128))
	quad.get_rid()
	multimesh.mesh = quad
	

	# Set the transform of the instances.
	var x  = 0
	var y = 0
	for i in multimesh.instance_count :
		if x== 400 :
			y+=1
			x= 0
		var angle = PI
		var pos = Vector2(x * 128, y * 128)
		multimesh.set_instance_transform_2d(i, Transform2D(angle, Vector2(0.25,0.25),0,pos))
		x+= 1
