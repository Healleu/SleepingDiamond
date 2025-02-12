using System.Collections.Generic;
using Godot;

public partial class VisionComponent2D : Component2D
{
    private List<Area2D> _areas = new List<Area2D>();
    private List<Entity2D> _entities = new List<Entity2D>();
    private List<Node2D> _bodies = new List<Node2D>();
    protected Area2D _area = null;
    public VisionComponent2D(Entity2D entity, double update_period, Area2D area, bool detect_area = true, bool detect_body = true) : base(entity, update_period)
    {
        _area = area;
    }

    protected override void _Process(double delta)
    {

    }

    public List<Node2D> GetBodies()
    {
        return _bodies;
    }

    public List<Area2D> GetAreas()
    {
        return _areas;
    }

    public List<Entity2D> GetEntities()
    {
        return _entities;
    }

}