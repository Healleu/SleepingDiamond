using Godot;

public partial class IsAt : BTLeaf
{
    protected Vector2 _position;

    public IsAt(Vector2 position)
    {
        _position = position;
    }
    public override BTStatus Tick(double delta, object actor, Blackboard blackboard)
    {
        if (((Entity2D)actor).GetPosition() == _position)
        {
            return BTStatus.SUCCESS;
        }
        return BTStatus.FAILURE;
    }
}