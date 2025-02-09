public partial class HasPath : BTLeaf
{
    public override BTStatus Tick(double delta, object actor, Blackboard blackboard)
    {
        Vector2I start_cell = (Entity2D)actor.GetPosition() / Vector2(32, 32) // TODO A faire
        Vector2I end_cell = blackboard.GetValue("destination");
        List<Vector2> path = PathFinding.GetCellPath(start_cell, end_cell)
        if (path != null && path.Any())
        {
            return BTStatus.SUCCESS;
        }
        return BTStatus.FAILURE;
    }
}