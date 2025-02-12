using Castle.Components.DictionaryAdapter.Xml;

public partial class HasPartner : BTLeaf
{
    public override BTStatus Tick(double delta, object actor, Blackboard blackboard)
    {
        VisionComponent2D vision = (VisionComponent2D)((Entity)actor).GetComponentByName("VisionComponent2D");
        foreach(var target in vision.GetEntities())
        {
            Animal targ = target as Animal;
            Animal act = actor as Animal;
            if (targ.GetClass() == act.GetClass())
            {
                if (targ.GetSexe() != act.GetSexe())
                {
                    if (targ.CanReproduce() != act.CanReproduce())
                    {
                        MotionComponent2D motion = (MotionComponent2D)((Entity)actor).GetComponentByName("MotionComponent2D");
                        //motion.SetPath(PathFinding)
                        return BTStatus.SUCCESS;
                    }
                }
                
            }
        }
        return BTStatus.FAILURE;
    }
}