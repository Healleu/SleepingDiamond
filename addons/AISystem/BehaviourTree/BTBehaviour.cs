using Godot;

public abstract partial class BTBehaviour : RefCounted
{
    public enum BTStatus : byte
    {
        SUCCESS,
        FAILURE,
        RUNNING,
    }

    public virtual BTStatus Tick(double delta, object actor, Blackboard blackboard)
    {
        return BTStatus.SUCCESS;
    }

}
