public partial class AIComponent : Component
{
    protected BTRoot _bt_root = null;
    public AIComponent(Entity entity, double update_period, BTRoot btroot) : base(entity, update_period)
    {
        _bt_root = btroot;
        _process_type = ProcessType.IDLE;
    }

    protected override void _Process(double delta)
    {
        if (_bt_root != null)
        {
            _bt_root.Update(delta, _entity);
        }
    }
}