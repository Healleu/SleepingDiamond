using System;
using System.Collections.Generic;
using Godot;

public partial class TimeSlicing : RefCounted
{
    protected const byte CYCLE_NUMBER = 60;
    protected float[] _deltas = new float[CYCLE_NUMBER];
    protected short _max_object_index = 0;
    protected bool _pool_modified = true;
    protected byte _frame_index = 0;
    protected short _object_remained = 0;
    protected short _pool_size = 0;

    public void Update<T>(double delta, List<T> objects, Action<int> method)
    {
        ulong start = Time.GetTicksUsec();
        // TODO need to increase only frame used
        for (byte _delta_index = 0; _delta_index < _deltas.Length; _delta_index++)
            _deltas[_delta_index] += (float)delta;
        //
        if (IsNewCycle() && _pool_modified)
        {
            _pool_modified = false;
            _object_remained = (short)(objects.Count);
            _pool_size = _GetPoolSize((short)(objects.Count));
        }
        if (_pool_size == 0)
            return;
        //
        long taskId = WorkerThreadPool.AddGroupTask(Callable.From<int>(method), Math.Min(_pool_size, _object_remained));
        WorkerThreadPool.WaitForGroupTaskCompletion(taskId);
        _deltas[_frame_index] = 0.0f;
        _frame_index += 1;
        _object_remained -= _pool_size;
        //
        if (_object_remained <= 0)
        {
            _object_remained = (short)(objects.Count); ;
            _frame_index = 0;
        }
        GD.Print(Time.GetTicksUsec() - start);
    }

    public bool IsNewCycle()
    {
        return _frame_index == 0;
    }

    private short _GetPoolSize(short object_count)
    {
        return (short)Math.Ceiling(object_count / (double)CYCLE_NUMBER);
    }

    public void SetPoolModified()
    {
        _pool_modified = true;
    }

    public byte GetFrameIndex()
    {
        return _frame_index;
    }

    public short GetPoolSize()
    {
        return _pool_size;
    }

    public float GetDelta(byte _frame_index)
    {
        return _deltas[_frame_index];
    }

}


