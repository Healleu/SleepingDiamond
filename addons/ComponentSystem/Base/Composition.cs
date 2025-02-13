using Godot;

public abstract partial class Composition : Resource
{
	public enum ProcessType : byte
	{
		UNUSED, // Don't use updates
		IDLE, // Updates on rendered frame.
		PHYSICS, // Updates synchornized with physics thread. 
	}

	public virtual void Setup() { }

	public new string GetClass()
	{
		return GetType().Name;
	}

	public virtual void CleanUp()
	{
		Free();
	}
}