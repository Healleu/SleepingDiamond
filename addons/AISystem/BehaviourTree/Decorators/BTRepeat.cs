public partial class BTRepeat : BTDecorator
{
    protected byte _repetition = 1;
    protected BTStatus _on_limit = BTStatus.FAILURE;

    protected string _cache_key = "0"; // TODO need to be done with instance ID to have constant value
    public override BTStatus Tick(double delta, object actor, Blackboard blackboard)
    {
        byte _current_count = (byte)blackboard.GetValue(_cache_key);

        if (_current_count <= _repetition)
        {
            BTStatus response = _leaf.Tick(delta, actor, blackboard);

            if (response == BTStatus.RUNNING)
                return response;

            if (response == BTStatus.SUCCESS)
            {
                _current_count += 1;
                blackboard.SetValue(_cache_key, _current_count);
                return BTStatus.RUNNING;
            }
            else
            {
                return response;
            }
        }
        else
        {
            blackboard.SetValue(_cache_key, 0);
            return _on_limit;
        }

    }
}



