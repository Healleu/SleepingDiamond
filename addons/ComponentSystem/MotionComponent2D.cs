using System.Collections.Generic;
using Godot;

public partial class MotionComponent2D : Component2D
{
    protected bool _moving = false;
    protected float _threshold = 1.0f;
    protected float _velocity_max = 120.0f;
    protected List<Vector2> _path = new List<Vector2>();

    protected Vector2 _destination = Vector2.Zero;

    public MotionComponent2D(Entity2D entity, double update_period, float velocity_max) : base(entity, update_period)
    {
        _velocity_max = velocity_max;
        _process_type = ProcessType.PHYSICS;
    }

    protected override void _Process(double delta){
        if(_moving) 
        {
            if(_entity != null){
                Vector2 position = _entity.GetPosition();
                float distance = _destination.DistanceSquaredTo(position);
                if(distance < _threshold * _threshold){
                    _path.RemoveAt(0);
                    _entity.SetPosition(_destination); // should be comment ?
                    if(_path.Count > 0)
                    {
                        _destination = _path[0];
                    }
                    else {
                        _moving = false;
                        return;
                    }
                }
                Vector2 diff = _destination - position;
                Vector2 direction = diff.Normalized();
                Vector2 movement = direction * _velocity_max * (float)delta;
                if(movement.Length() > _destination.DistanceSquaredTo(position))
                {
                    _entity.SetPosition(_destination);
                }
                else 
                {
                    _entity.SetPosition(_entity.GetPosition() + movement);
                }
        
            }
        }
        
    }

    public void SetPath(List<Vector2> path)
    {
        _path = path;
        _destination = _path[0];
        _moving = true;
    }

    public bool IsMoving()
    {
        return _moving;
    }

    public override string GetClass()
    {
        return "MotionComponent";
    }

}