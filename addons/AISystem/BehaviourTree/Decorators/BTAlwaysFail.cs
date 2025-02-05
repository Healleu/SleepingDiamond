public partial class BTAlwaysFail : BTDecorator
{
    public override BTStatus Tick(double delta, object actor, Blackboard blackboard)
    {
        BTStatus response = _leaf.Tick(delta, actor, blackboard);

        if (response == BTStatus.RUNNING)
            return BTStatus.RUNNING;

        return BTStatus.FAILURE;
    }
}