public partial class BTLeafSuccess : BTLeaf
{
    public override BTStatus Tick(double delta, object actor, Blackboard blackboard)
    {
        return BTStatus.SUCCESS;
    }
}