public partial class MoveTo : BTLeaf
{
    public override BTStatus Tick(double delta, object actor, Blackboard blackboard)
    {
        if (((MotionComponent2D)((Entity2D)actor).GetComponentByName("MotionComponent2D")).IsMoving())
        {
            return BTStatus.RUNNING;
        }
        return BTStatus.SUCCESS;
    }
}