public partial class BTWaitTime : BTLeaf
{
    protected double _wait_time = 1.0;
    protected BTStatus _wait_status = BTStatus.FAILURE;

    protected double _wait_time_elapsed = 0.0;

    public BTWaitTime(double wait_time, BTStatus wait_status = BTStatus.FAILURE)
    {
        _wait_time = wait_time;
        _wait_status = wait_status;
    }
    public override BTStatus Tick(double delta, object actor, Blackboard blackboard)
    {
        _wait_time_elapsed += delta;
        if(_wait_time_elapsed >= _wait_time)
        {
            _wait_time_elapsed = 0.0;
            return BTStatus.SUCCESS;
        }

        return _wait_status;
    }
}