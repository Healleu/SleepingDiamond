using System.Collections.Generic;
using Godot;

public partial class Sheep : Animal
{
    protected bool _can_milk = false;
    protected bool _can_shear = false;

    protected List<Sheep> _children = new List<Sheep>();

    private void OnInputEvent(Node viewport, InputEvent @event, int shape_id)
    {
    	if (@event is InputEventMouseButton)
        {
            GD.Print("Ici");
        }
	}

    private void OnMouseEntered()
    {
        GD.Print("Ici");
    }

}
