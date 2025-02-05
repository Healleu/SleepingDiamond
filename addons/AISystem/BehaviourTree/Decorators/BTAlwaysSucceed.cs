public partial class BTAlwaysSuceed : BTDecorator
{
    public override BTStatus Tick(double delta, object actor, Blackboard blackboard)
    {
        BTStatus response = _leaf.Tick(delta, actor, blackboard);

        if (response == BTStatus.RUNNING)
            return BTStatus.RUNNING;

        return BTStatus.SUCCESS;
    }
}