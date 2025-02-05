using Godot;

public partial class Entity2D : Entity
{
    protected Transform2D transform = Transform2D.Identity;

    public Transform2D GetTransform2D()
    {
        return transform;
    }

    public void SetTransform2D(Transform2D transform)
    {
        this.transform = transform;
    }

    public Vector2 GetPosition()
    {
        return transform.Origin;
    }

    public void SetPosition(Vector2 position)
    {
        transform.Origin = position;
    }
}