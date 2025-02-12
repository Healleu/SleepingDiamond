using Godot;
using System;


public abstract partial class Composition : Resource
{
	public enum ProcessType : byte
	{
		UNUSED, // Don't use updates
		IDLE, // Updates on rendered frame.
		PHYSICS, // Updates synchornized with physics thread. 
	}

	public new virtual Type GetClass()
	{
		return typeof(Composition);
	}

	public virtual void Setup() { }

	public virtual void CleanUp()
	{
		Free();
	}
}