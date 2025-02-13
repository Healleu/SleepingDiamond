using System;
using System.Linq;
using Godot;

public partial class Entity2D : Entity
{
    string[] PHYSICS_COMPONENT = { "PhysicsComponent2D", "AreaPhysicsComponent2D" };
    protected Transform2D _transform = Transform2D.Identity;

    protected PhysicsComponent2D _physicsComponent2D = null;
    protected RenderingComponent2D _renderingComponent2D = null;

    public override void Setup()
    {
        _components_idle.Clear();
        _components_physics.Clear();
        foreach (var component in _components)
        {
            component.Setup();
            if (PHYSICS_COMPONENT.Contains(component.GetClass()))
            {
                _physicsComponent2D = component as PhysicsComponent2D;
            }
            if (component.GetClass() == "RenderingComponent2D")
            {
                _renderingComponent2D = component as RenderingComponent2D;
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
        return _transform;
    }

    public void SetTransform2D(Transform2D transform)
    {
        //GD.Print(transform);
        _transform = transform;
        if (_physicsComponent2D != null)
        {
            _physicsComponent2D.SetTransform(transform);
        }
        if (_renderingComponent2D != null)
        {
            _renderingComponent2D.SetTransform(transform);
        }
    }

    public Vector2 GetPosition()
    {
        return _transform.Origin;
    }

    public void SetPosition(Vector2 position)
    {
        _transform.Origin = position;
        if (_physicsComponent2D != null)
        {
            _physicsComponent2D.SetTransform(_transform);
        }
        if (_renderingComponent2D != null)
        {
            _renderingComponent2D.SetTransform(_transform);
        }
    }

    public float GetRotation()
    {
        return _transform.Rotation;
    }

    public void SetRotation(float rotation)
    {
        _transform = new Transform2D(rotation, _transform.Origin);
        if (_physicsComponent2D != null)
        {
            _physicsComponent2D.SetTransform(_transform);
        }
        if (_renderingComponent2D != null)
        {
            _renderingComponent2D.SetTransform(_transform);
        }
    }
}