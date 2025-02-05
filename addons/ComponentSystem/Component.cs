using Godot;

public abstract partial class Component : RefCounted
{
	public enum ProcessType : byte
	{
		UNUSED, // Don't use updates
		IDLE, // Updates on rendered frame.
		PHYSICS, // Updates synchornized with physics thread. 
	}
	protected string _name = "Undefined";
	protected Entity _entity = null;
	protected bool _enable = true;
	protected ProcessType _process_type = ProcessType.IDLE;
	protected double _update_period = 1.0f;
	protected double _elapsed_time = 0.0f;

	public virtual void Setup() { }

	public void Update(double delta)
	{
		if (_enable)
		{
			_elapsed_time += delta;
			if (_elapsed_time >= _update_period)
			{
				_Process(_elapsed_time);
				_elapsed_time = 0.0f;
			}
		}

	}

	protected abstract void _Process(double delta);

	public Component(Entity entity, double update_period)
	{
		_entity = entity;
		_update_period = update_period;
	}

	public void set_enable(bool enable)
	{
		_enable = enable;
	}

	public virtual Entity GetEntity()
	{
		return _entity;
	}

	public new virtual string GetClass()
	{
		return "Component";
	}

	public ProcessType GetProcessType()
	{
		return _process_type;
	}

	public string GetName()
	{
		return _name;
	}

}