public partial class BTSelector : BTComposite
{

    public override BTStatus Tick(double delta, object actor, Blackboard blackboard)
    {
        byte index = 0;
        foreach (var leave in _leaves)
        {
            if (index < _active_index)
            {
                index += 1;
                continue;
            }


            BTStatus response = leave.Tick(delta, actor, blackboard);
            if (response == BTStatus.SUCCESS)
            {
                _active_index = 0;
                return response;
            }

            if (response == BTStatus.RUNNING)
                return BTStatus.RUNNING;
            if (response == BTStatus.FAILURE)
                _active_index += 1;

            index += 1;
        }

        _active_index = 0;
        return BTStatus.FAILURE;
    }

}