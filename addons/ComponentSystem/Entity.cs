using System.Collections.Generic;
using Godot;

public partial class Entity : Node
{
	protected List<Component> _components = new List<Component>();
	protected List<Component> _components_idle = new List<Component>();
	protected List<Component> _components_physics = new List<Component>();

	protected virtual void Setup()
	{
		_components_idle.Clear();
		_components_physics.Clear();
		foreach(var component in _components)
		{
			if(component.GetProcessType() == Component.ProcessType.IDLE)
			{
				_components_idle.Add(component);
			}
			if(component.GetProcessType() == Component.ProcessType.PHYSICS)
			{
				_components_physics.Add(component);
			}
		}	
	}

	public override void _Ready()
	{
		SetProcessMode(ProcessModeEnum.Disabled);
		//CallDeferred("set_process_mode", 4);
		Setup();
	}

	public virtual void Update(double delta){
        foreach (var component in _components_idle){
            component.Update(delta);
        }
    }

	public virtual void UpdatePhysics(double delta){
        foreach (var component in _components_physics){
            component.Update(delta);
        }
    }

    public bool HasComponent(Component component)
	{
		return _components.Contains(component);
	}
	public bool HasComponentByName(string component_name) 
	{ 
		foreach (var component in _components)
		{
			if(component.GetName() == component_name)
				return true;
		}
		return false;
	}
	public bool HasComponentByClass(string component_class) 
	{ 
		foreach (var component in _components)
		{
			if(component.GetClass() == component_class)
				return true;
		}
		return false;
	}

    public Component GetComponentByName(string component_name) 
	{
		foreach (var component in _components)
		{
			if(component.GetName() == component_name)
				return component;
		}
		return null;
	}

	public Component GetComponentByClass(string component_class) 
	{
		foreach (var component in _components)
		{
			if(component.GetClass() == component_class)
				return component;
		}
		return null;
	}

	public void AddComponent(Component component) 
	{
		_components.Add(component);
		Setup();
	}

	public void RemoveComponent(Component component)
	{
		_components.Remove(component);
		Setup();
	}


    public new virtual string GetClass()
    {
        return "Entity";
	}
	
	
}
