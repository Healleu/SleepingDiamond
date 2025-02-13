using Godot;

public partial class AreaPhysicsComponent2D : PhysicsComponent2D
{

    public AreaPhysicsComponent2D(Entity2D entity, double update_period, Node2D parent, bool visible_collision_shapes = false) : base(entity, update_period, parent, visible_collision_shapes)
    {
        _collision_object = PhysicsServer2D.AreaCreate();
        PhysicsServer2D.AreaSetSpace(_collision_object, _parent.GetWorld2D().Space);
        PhysicsServer2D.AreaSetMonitorable(_collision_object, true);
        PhysicsServer2D.AreaAttachObjectInstanceId(_collision_object, entity.GetInstanceId());
    }

    public override void SetTransform(Transform2D transform)
    {
        base.SetTransform(transform);
        //GD.Print(transform);
        PhysicsServer2D.AreaSetTransform(_collision_object, transform);
    }

    public override Transform2D GetTransform()
    {
        return PhysicsServer2D.AreaGetTransform(_collision_object);
    }

    public override Variant GetShapeData()
    {
        return PhysicsServer2D.ShapeGetData(_shape);
    }

    public override void CleanUp()
    {
        base.CleanUp();
        PhysicsServer2D.FreeRid(_collision_object);
        PhysicsServer2D.FreeRid(_shape);
    }

}