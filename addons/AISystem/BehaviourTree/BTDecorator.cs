public abstract partial class BTDecorator : BTBehaviour
{
    protected BTBehaviour _leaf = null;

    public void SetLeaf(BTBehaviour leaf)
    {
        _leaf = leaf;
    }

    public BTBehaviour GetLeaf()
    {
        return _leaf;
    }
}
