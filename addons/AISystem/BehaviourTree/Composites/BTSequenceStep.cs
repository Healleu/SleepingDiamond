
public partial class BTSequenceStep : BTComposite
{

    public override BTStatus Tick(double delta, object actor, Blackboard blackboard)
    {
        BTStatus response = _leaves[_active_index].Tick(delta, actor, blackboard);

        if (response == BTStatus.RUNNING)
            return response;

        if (response == BTStatus.FAILURE)
        {
            _active_index = 0;
            return response;
        }

        _active_index += 1;
        if (_active_index > _leaves.Count - 1)
        {
            _active_index = 0;
            return BTStatus.SUCCESS;
        }

        return BTStatus.RUNNING;
    }
}
