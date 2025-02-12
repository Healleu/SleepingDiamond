using System;
using Godot;

public partial class PhysicsComponent2D : Component2D
{
    private Rid _area;
    private Rid _shape;
    private Node2D mynode = null;

    public PhysicsComponent2D(Entity2D entity, double update_period, Node2D node) : base(entity, update_period)
    {
        /*
        // Create the body.
        _area = PhysicsServer2D.BodyCreate();
        PhysicsServer2D.BodySetMode(_area, PhysicsServer2D.BodyMode.Rigid);
        // Add a shape.
        _shape = PhysicsServer2D.RectangleShapeCreate();
        // Set rectangle extents.
        PhysicsServer2D.ShapeSetData(_shape, new Vector2(100, 100));
        // Make sure to keep the shape reference!
        PhysicsServer2D.BodyAddShape(_area, _shape);
        // Set space, so it collides in the same space as current scene.
        PhysicsServer2D.BodySetSpace(_area, node.GetWorld2D().Space);
        // Move initial position.
        PhysicsServer2D.BodySetState(_area, PhysicsServer2D.BodyState.Transform, entity.GetTransform2D());
        PhysicsServer2D.BodySetCollisionMask(_area, 1);
        PhysicsServer2D.BodySetCollisionLayer(_area, 1);
        
        */
        mynode = node;
        _shape = PhysicsServer2D.CircleShapeCreate();
        _area = PhysicsServer2D.AreaCreate();
        PhysicsServer2D.ShapeSetData(_shape, 32);
        PhysicsServer2D.AreaAddShape(_area, _shape);
        PhysicsServer2D.AreaSetSpace(_area, node.GetWorld2D().Space);
        PhysicsServer2D.AreaSetMonitorable(_area, true);
        PhysicsServer2D.AreaAttachObjectInstanceId(_area, entity.GetInstanceId());
        //PhysicsServer2D.AreaInt
        //PhysicsServer2D.AreaAttachCanvasInstanceId
    }

    public void SetTransform(Transform2D transform)
    {
        PhysicsServer2D.AreaSetTransform(_area, transform);
        mynode.QueueRedraw();
    }

    public Transform2D GetTransform()
    {
        return PhysicsServer2D.AreaGetTransform(_area);
    }

    public Variant GetShapeData()
    {
        return PhysicsServer2D.ShapeGetData(_shape);
    }

    public override void CleanUp()
    {
        base.CleanUp();
        PhysicsServer2D.FreeRid(_area);
        PhysicsServer2D.FreeRid(_shape);
    }

    public void Draw()
    {
        mynode.DrawCircle(GetTransform().Origin, (float)GetShapeData(), (Color)ProjectSettings.GetSetting("debug/shapes/collision/shape_color"));
    }

    public override Type GetClass()
    {
        return typeof(PhysicsComponent2D);
    }
}