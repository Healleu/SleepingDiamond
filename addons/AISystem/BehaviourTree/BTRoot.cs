public partial class BTRoot : BTBehaviour
{
    protected Blackboard blackboard = new Blackboard();

    protected bool active = true;

    protected BTStatus current_status = BTStatus.RUNNING;

    protected BTBehaviour entry_point = null;

    public void Update(double delta, Entity actor)
    {
        if (entry_point != null && active)
        {
            current_status = entry_point.Tick(delta, actor, blackboard);
        }

    }

    public void AddEntryPoint(BTBehaviour bTBehaviour)
    {
        entry_point = bTBehaviour;
    }
}
