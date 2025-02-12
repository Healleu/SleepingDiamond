using System;
using Godot;

public partial class Entity2D : Entity
{
    protected Transform2D transform = Transform2D.Identity;

    protected PhysicsComponent2D physicsComponent2D = null;
    protected RenderingComponent2D renderingComponent2D = null;

    public override void Setup()
    {
        _components_idle.Clear();
        _components_physics.Clear();
        foreach (var component in _components)
        {
            component.Setup();
            if (component.GetClass() == typeof(PhysicsComponent2D))
            {
                physicsComponent2D = component as PhysicsComponent2D;
            }
            if (component.GetClass() == typeof(RenderingComponent2D))
            {
                renderingComponent2D = component as RenderingComponent2D;
            }
            if (component.GetProcessType() == ProcessType.IDLE)
            {
                _components_idle.Add(component);
            }
            if (component.GetProcessType() == ProcessType.PHYSICS)
            {
                _components_physics.Add(component);
            }
        }
    }

    public Transform2D GetTransform2D()
    {
        return transform;
    }

    public void SetTransform2D(Transform2D transform)
    {
        this.transform = transform;
        if (physicsComponent2D != null)
        {
            physicsComponent2D.SetTransform(transform);
        }
        if (renderingComponent2D != null)
        {
            renderingComponent2D.SetTransform(transform);
        }
    }

    public Vector2 GetPosition()
    {
        return transform.Origin;
    }

    public void SetPosition(Vector2 position)
    {
        transform.Origin = position;
        if (physicsComponent2D != null)
        {
            physicsComponent2D.SetTransform(transform);
        }
        if (renderingComponent2D != null)
        {
            renderingComponent2D.SetTransform(transform);
        }
    }

    public override Type GetClass()
    {
        return typeof(Entity2D);
    }
}