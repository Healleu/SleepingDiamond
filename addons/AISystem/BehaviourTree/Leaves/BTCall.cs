using Godot;

public partial class BTCall : BTLeaf
{
    public override BTStatus Tick(double delta, object actor, Blackboard blackboard)
    {
        if (actor != null)
        {
        }
        return BTStatus.SUCCESS;
    }
}
