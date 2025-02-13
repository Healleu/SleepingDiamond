using System.Collections.Generic;
using System.Linq;
using Godot;

public partial class MotionComponent2D : Component2D
{
    protected bool _is_moving = false;
    protected float _threshold_squared = 1.0f;
    protected float _velocity_max = 120.0f;
    protected Vector2 _next_waypoint = Vector2.Zero;
    protected List<Vector2> _path = null;

    public MotionComponent2D(Entity2D entity, double update_period, float velocity_max) : base(entity, update_period)
    {
        _velocity_max = velocity_max;
        _process_type = ProcessType.PHYSICS;
    }
    protected override void _Process(double delta)
    {
        if (_is_moving)
        {
            // If at Waypoint then go next
            Vector2 position = _entity.GetPosition();
            if (position == _next_waypoint)
            {
                _path.RemoveAt(0);
                if (_path.Any())
                {
                    _next_waypoint = _path[0];
                }
                else
                {
                    _is_moving = false;
                    _path = null;
                    return;
                }
            }
            Transform2D trans = new Transform2D(position.AngleToPoint(_next_waypoint), position.MoveToward(_next_waypoint, _velocity_max * (float)delta));
            _entity.SetTransform2D(trans);

            //_entity.SetPosition( position.MoveToward(_next_waypoint, _velocity_max * (float)delta));
        }
    }
    /*
        protected override void _Process(double delta){
            if(_is_moving) 
            {
                // If at Waypoint then go next
                Vector2 position = _entity.GetPosition();
                if(_next_waypoint.DistanceSquaredTo(position) <= _threshold_squared)
                {
                    _path.RemoveAt(0);
                    if(_path.Any())
                    {
                        _next_waypoint = _path[0];
                    }
                    else 
                    {
                        _entity.SetPosition(_next_waypoint);
                        _is_moving = false;
                        _path = null;
                        return;
                    }
                }

                // Move to Waypoint
                Vector2 direction = (_next_waypoint - position).Normalized();
                Vector2 movement = direction * _velocity_max * (float)delta;
                if(movement.Length() > _next_waypoint.DistanceSquaredTo(position))
                {
                    _entity.SetPosition(_next_waypoint);
                }
                else 
                {
                    _entity.SetPosition(_entity.GetPosition() + movement);
                }
            }
        }
    */
    public void SetPath(List<Vector2> path)
    {
        if (path != null && path.Any())
        {
            _path = path;
            _next_waypoint = _path[0];
            _is_moving = true;
        }
        else
        {
            GD.PrintErr("Try to set an empty path");
        }
    }

    public bool IsMoving()
    {
        return _is_moving;
    }

}