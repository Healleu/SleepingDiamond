public partial class Component2D : Component
{
    protected new Entity2D _entity = null;

    public Component2D(Entity2D entity, double update_period) : base(entity, update_period)
    {
      _entity = entity;
    }

    public override void Setup()
    {
        
    }

    protected override void _Process(double delta){}

    public override Entity2D GetEntity()
    {
      return _entity;
    }

    public override string GetClass()
    {
      return "Component";
    }
}