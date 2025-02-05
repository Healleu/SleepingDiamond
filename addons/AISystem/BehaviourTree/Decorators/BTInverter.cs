public partial class BTInverter : BTDecorator
{
    public override BTStatus Tick(double delta, object actor, Blackboard blackboard)
    {
        BTStatus response = _leaf.Tick(delta, actor, blackboard);

        if (response == BTStatus.FAILURE)
            return BTStatus.SUCCESS;

        if (response == BTStatus.SUCCESS)
            return BTStatus.FAILURE;

        return BTStatus.RUNNING;
    }
}