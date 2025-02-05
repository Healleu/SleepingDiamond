using System.Collections.Generic;
public abstract partial class BTComposite : BTBehaviour
{
    protected List<BTBehaviour> _leaves = new List<BTBehaviour>();
    protected byte _active_index = 0;

    public void AddLeaf(BTBehaviour leaf)
    {
        _leaves.Add(leaf);
    }

    public byte GetActiveIndex()
    {
        return _active_index;
    }
    //public abstract BTStatus Tick(double delta, Entity actor, Blackboard blackboard);
}
