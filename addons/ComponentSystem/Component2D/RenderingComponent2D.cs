using System;
using Godot;

public partial class RenderingComponent2D : Component2D
{
    private Texture2D _texture;
    private Rid _canvas_item;

    public RenderingComponent2D(Entity2D entity, double update_period, Node2D node) : base(entity, update_period)
    {
        // Create a canvas item, child of this node.
        _canvas_item = RenderingServer.CanvasItemCreate();
        // Make this node the parent.
        RenderingServer.CanvasItemSetParent(_canvas_item, node.GetCanvasItem());
        // Draw a texture on it.
        // Remember, keep this reference.
        _texture = ResourceLoader.Load<Texture2D>("res://icon.svg");
        // Add it, centered.
        RenderingServer.CanvasItemAddTextureRect(_canvas_item, new Rect2(-_texture.GetSize() / 2, _texture.GetSize()), _texture.GetRid());
        // Add the item, rotated 45 degrees and translated.
        RenderingServer.CanvasItemSetTransform(_canvas_item, entity.GetTransform2D());
        //RenderingServer.CanvasItemSetInterpolated(_canvas_item, true);
        //RenderingServer.InstanceSetInterpolated(entity.GetRid(), true);
        //RenderingServer.InstanceAttachObjectInstanceId(_canvas_item, entity.GetInstanceId());
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

    public override Type GetClass()
    {
        return typeof(RenderingComponent2D);
    }
}