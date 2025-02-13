using Godot;

public partial class RenderingComponent2D : Component2D
{
    protected Texture2D _texture;
    protected Rid _canvas_item;
    protected string _texture_path;
    protected Node2D _parent = null;

    public RenderingComponent2D(Entity2D entity, double update_period, Node2D parent, string texture_path) : base(entity, update_period)
    {
        _texture_path = texture_path;
        _parent = parent;
        _texture = ResourceLoader.Load<Texture2D>(_texture_path);
        _canvas_item = RenderingServer.CanvasItemCreate();
        RenderingServer.CanvasItemSetParent(_canvas_item, parent.GetCanvasItem());
        RenderingServer.CanvasItemAddTextureRect(_canvas_item, new Rect2(-_texture.GetSize() / 2, _texture.GetSize()), _texture.GetRid());
        RenderingServer.CanvasItemSetTransform(_canvas_item, _entity.GetTransform2D());
    }

    public void SetTransform(Transform2D transform)
    {
        //RenderingServer.CanvasItemTransformPhysicsInterpolation(_canvas_item, transform);
        RenderingServer.CanvasItemSetTransform(_canvas_item, transform);
    }

    public override void CleanUp()
    {
        base.CleanUp();
        RenderingServer.FreeRid(_canvas_item);
    }

}