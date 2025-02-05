public partial class BTLeafFailure : BTLeaf
{
    public override BTStatus Tick(double delta, object actor, Blackboard blackboard)
    {
        return BTStatus.FAILURE;
    }
}