using System.Collections.Generic;

public partial class Entity : Composition
{
	protected List<Component> _components = new List<Component>();
	protected List<Component> _components_idle = new List<Component>();
	protected List<Component> _components_physics = new List<Component>();

	public override void Setup()
	{
		_components_idle.Clear();
		_components_physics.Clear();
		foreach (var component in _components)
		{
			component.Setup();
			if (component.GetProcessType() == Component.ProcessType.IDLE)
			{
				_components_idle.Add(component);
			}
			if (component.GetProcessType() == Component.ProcessType.PHYSICS)
			{
				_components_physics.Add(component);
			}
		}
	}

	public virtual void Update(double delta)
	{
		foreach (var component in _components_idle)
		{
			component.Update(delta);
		}
	}

	public virtual void UpdatePhysics(double delta)
	{
		foreach (var component in _components_physics)
		{
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
			if (component.GetName() == component_name)
				return true;
		}
		return false;
	}
	public bool HasComponentByType(string component_type)
	{
		foreach (var component in _components)
		{
			if (component.GetType().ToString() == component_type)
				return true;
		}
		return false;
	}

	public Component GetComponentByName(string component_name)
	{
		foreach (var component in _components)
		{
			if (component.GetName() == component_name)
				return component;
		}
		return null;
	}

	public Component GetComponentByType(string component_type)
	{
		foreach (var component in _components)
		{
			if (component.GetType().ToString() == component_type)
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

	public override void CleanUp()
	{
		foreach (var component in _components)
		{
			component.CleanUp();
		}
		//Free();
	}

}
