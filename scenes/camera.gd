extends Camera2D

signal zoom_changed(zoom_value : Vector2)

@export var zoomSpeed : float = 10;

var zoomTarget :Vector2

var dragStartMousePos = Vector2.ZERO
var dragStartCameraPos = Vector2.ZERO
var isDragging : bool = false

func _ready():
	zoomTarget = zoom
	return

func _process(delta):
	Zoom(delta)
	SimplePan(delta)
	#ClickAndDrag()
	
func Zoom(delta):
	if Input.is_action_just_pressed("camera_zoom_in"):
		zoomTarget *= 1.1
		
	if Input.is_action_just_pressed("camera_zoom_out"):
		zoomTarget *= 0.9
		
	zoom = zoom.slerp(zoomTarget, zoomSpeed * delta)
	zoom_changed.emit(zoom)
	
	
func SimplePan(delta):
	var moveAmount = Vector2.ZERO
	if Input.is_action_pressed("ui_right"):
		moveAmount.x += 1
		
	if Input.is_action_pressed("ui_left"):
		moveAmount.x -= 1
	if Input.is_action_pressed("ui_up"):
		moveAmount.y -= 1
		
	if Input.is_action_pressed("ui_down"):
		moveAmount.y += 1
		
	moveAmount = moveAmount.normalized()
	position += moveAmount * delta * 1000 * (1/zoom.x)
	
#func ClickAndDrag():
	#if !isDragging and Input.is_action_just_pressed("camera_pan"):
		#dragStartMousePos = get_viewport().get_mouse_position()
		#dragStartCameraPos = position
		#isDragging = true
		#
	#if isDragging and Input.is_action_just_released("camera_pan"):
		#isDragging = false
		#
	#if isDragging:
		#var moveVector = get_viewport().get_mouse_position() - dragStartMousePos
		#position = dragStartCameraPos - moveVector * 1/zoom.x	
		#
	#
	#
