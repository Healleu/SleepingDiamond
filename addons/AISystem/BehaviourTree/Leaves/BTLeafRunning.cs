public partial class BTLeafRunning : BTLeaf
{
    public override BTStatus Tick(double delta, object actor, Blackboard blackboard)
    {
        return BTStatus.RUNNING;
    }
}