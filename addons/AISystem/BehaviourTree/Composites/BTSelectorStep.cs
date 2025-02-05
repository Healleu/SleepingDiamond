public abstract partial class BTSelectorStep : BTComposite
{
    public override BTStatus Tick(double delta, object actor, Blackboard blackboard)
    {
        if (_active_index > _leaves.Count - 1)
        {
            _active_index = 0;
            return BTStatus.FAILURE;
        }

        BTStatus response = _leaves[_active_index].Tick(delta, actor, blackboard);

        if (response == BTStatus.SUCCESS)
        {
            _active_index = 0;
            return response;
        }

        if (response == BTStatus.RUNNING)
            return response;

        _active_index += 1;
        return BTStatus.RUNNING;
    }

}