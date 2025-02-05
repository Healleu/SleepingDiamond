using System.Collections.Generic;

public partial class Sheep : Animal
{
    protected bool _can_milk = false;
    protected bool _can_shear = false;

    protected List<Sheep> _children = new List<Sheep>();
}
