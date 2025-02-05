using Godot;

public partial class AStarGrid2DDebug : Node2D
{


    [Export]
    public float radius = 5.0f;
    [Export]
    public Color color = new Color(Colors.Red);
    [Export]
    public bool filled = true;



}
/*
@onready var pathfinding = null

func _ready() -> void :
	if pathfinding :
		pathfinding.draw_path = self
		pathfinding.new_active_path.connect(_on_new_active_path)
	return

func _draw() -> void:
	if pathfinding :
		for id in pathfinding.active_path :
			var path : PackedVector2Array = pathfinding.active_path[id]
			for cell in path :
				draw_circle(cell, radius, color, filled)
	return

func _on_new_active_path() -> void :
	queue_redraw()
	return
*/