public partial class HasPartner : BTLeaf
{
    public override BTStatus Tick(double delta, object actor, Blackboard blackboard)
    {
        VisionComponent2D vision = (Entity2D)actor).GetComponentByName("VisionComponent2D"))
        foreach(var target in vision.targets)
        {
            if (target.GetClass() == actor.GetClass())
            {
                if (target.GetSexe() != actor.GetSexe())
                {
                    if (target.CanReproduct() != target.CanReproduct())
                    {
                        
                    }
                }
                
            }
        }
    }
}