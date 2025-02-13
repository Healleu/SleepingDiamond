using Godot;

public abstract partial class PhysicsComponent2D : Component2D
{
    protected bool _visible_collision_shapes = false;
    protected Rid _collision_object;
    protected Rid _shape;
    protected Node2D _parent = null;
    protected Rid _canvas_item;
    protected Vector2 _offset = Vector2.Zero;

    protected string _shape_type = new string(' ', 9);

    public PhysicsComponent2D(Entity2D entity, double update_period, Node2D parent, bool visible_collision_shapes = true) : base(entity, update_period)
    {
        _parent = parent;
        _visible_collision_shapes = visible_collision_shapes;
    }

    public virtual void SetTransform(Transform2D transform)
    {
        if (_canvas_item.IsValid)
        {
            RenderingServer.CanvasItemSetTransform(_canvas_item, transform);
        }

    }

    public abstract Transform2D GetTransform();

    public abstract Variant GetShapeData();

    public void SetCircleShape(float radius)
    {
        if (_shape.IsValid)
        {
            PhysicsServer2D.FreeRid(_shape);
        }
        if (_collision_object.IsValid)
        {
            _shape = PhysicsServer2D.CircleShapeCreate();
            PhysicsServer2D.ShapeSetData(_shape, radius);
            PhysicsServer2D.AreaAddShape(_collision_object, _shape);
            _shape_type = "circle";
            if (_visible_collision_shapes)
            {
                _canvas_item = RenderingServer.CanvasItemCreate();
                RenderingServer.CanvasItemSetParent(_canvas_item, _parent.GetCanvasItem());
                RenderingServer.CanvasItemAddCircle(_canvas_item, Vector2.Zero, radius, (Color)ProjectSettings.GetSetting("debug/shapes/collision/shape_color"));
                RenderingServer.CanvasItemSetTransform(_canvas_item, _entity.GetTransform2D());
                RenderingServer.CanvasItemSetDrawIndex(_canvas_item, int.MaxValue);
            }
        }
    }

    public void SetRectangleShape(Vector2 rect_size)
    {
        if (_shape.IsValid)
        {
            PhysicsServer2D.FreeRid(_shape);
        }
        if (_collision_object.IsValid)
        {
            //_offset = -rect_size / 2;
            _shape = PhysicsServer2D.RectangleShapeCreate();
            PhysicsServer2D.ShapeSetData(_shape, rect_size / 2);
            PhysicsServer2D.AreaAddShape(_collision_object, _shape);
            //PhysicsServer2D.AreaSetTransform(_collision_object, Transform2D.Identity);
            _shape_type = "rectangle";
            if (_visible_collision_shapes)
            {
                _canvas_item = RenderingServer.CanvasItemCreate();
                RenderingServer.CanvasItemSetParent(_canvas_item, _parent.GetCanvasItem());
                RenderingServer.CanvasItemAddRect(_canvas_item, new Rect2(-rect_size / 2, rect_size), (Color)ProjectSettings.GetSetting("debug/shapes/collision/shape_color"));
                RenderingServer.CanvasItemSetTransform(_canvas_item, _entity.GetTransform2D());
                RenderingServer.CanvasItemSetDrawIndex(_canvas_item, int.MaxValue);
            }


        }
        //_parent.QueueRedraw();
    }

    public override void CleanUp()
    {
        base.CleanUp();
        if (_canvas_item.IsValid)
        {
            RenderingServer.FreeRid(_canvas_item);
        }
        //if(_collision_object.IsValid)
        //{
        //    PhysicsServer2D.FreeRid(_collision_object);
        //}
        //if(_shape.IsValid)
        //{
        //    PhysicsServer2D.FreeRid(_shape);
        //}
    }

}