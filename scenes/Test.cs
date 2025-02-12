using System.Collections.Generic;
using System.Diagnostics;
using System;
using System.Linq;
using Godot;

public partial class Test : Node2D
{
	public Sprite2D sprite2D;
	private List<Entity> _entities = new List<Entity>();
	public override void _Ready()
	{
		//sprite2D = (Sprite2D)GetChild(0);
		Entity ent = SpecificEntity();
		_entities.Add(ent);
		//AddChild(ent);
	}

	public override void _Process(double delta)
	{
		//ulong start = Time.GetTicksUsec();
		//base._Process(delta);
		foreach (Entity2D entity in _entities)
		{
			entity.Update(delta);
			//sprite2D.GlobalPosition = entity.transform.Origin;
		}
		//GD.Print(Time.GetTicksUsec() - start);
	}

	public override void _PhysicsProcess(double delta)
	{
		ulong start = Time.GetTicksUsec();
		//base._PhysicsProcess(delta);
		foreach (Entity2D entity in _entities)
		{
			entity.UpdatePhysics(delta);
			//	sprite2D.GlobalPosition = entity.GetPosition();
		}
		GD.Print(Time.GetTicksUsec() - start);
	}

	public override void _ExitTree()
	{
		foreach (Entity2D entity in _entities)
		{
			entity.CleanUp();
		}
	}

	public Entity2D SpecificEntity()
	{
		Entity2D entity = new Entity2D();
		MotionComponent2D motion = new MotionComponent2D(entity, (double)1 / 60, 60.0f);
		List<Vector2> path = [new Vector2(500.0f, 500.0f), new Vector2(0.0f, 0.0f), new Vector2(500.0f, 500.0f)];
		motion.SetPath(path);
		BTRoot bt = new BTRoot();
		AIComponent ai = new AIComponent(entity, (double)1 / 15, bt);
		IsAt isat = new IsAt(Vector2.Zero);
		bt.AddEntryPoint(isat);
		entity.AddComponent(motion);
		entity.AddComponent(ai);
		//entity.SetPosition(new Vector2(50, 50));
		RenderingComponent2D rend = new RenderingComponent2D(entity, 0.0, this);
		entity.AddComponent(rend);
		PhysicsComponent2D ph = new PhysicsComponent2D(entity, 0.0, this);
		entity.AddComponent(ph);
		entity.Setup();
		return entity;
	}

	public override void _Draw()
	{
		foreach (Entity2D entity in _entities)
		{
			if (entity.HasComponentByClass(typeof(PhysicsComponent2D)))
			{
				((PhysicsComponent2D)entity.GetComponentByClass(typeof(PhysicsComponent2D))).Draw();
				//GD.Print("ici");
				//PhysicsComponent2D component = (PhysicsComponent2D)entity.GetComponentByClass("PhysicsComponent2D");
				//DrawCircle(component.GetTransform().Origin, (float)component.GetShapeData(), (Color)ProjectSettings.GetSetting("debug/shapes/collision/shape_color"));
			}
		}
	}
}

