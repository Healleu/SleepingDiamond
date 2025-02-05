using System.Collections.Generic;
using System;
using System.Linq;
using Godot;



public partial class PlantManager : Node
{
    const float GROWING_TIME = 5.0f;
    private const ushort MAXIMUM_PLANT = 8000;
    private const byte CYCLE_NUMBER = 60;
    //private Plant[] _plants = new Plant[MAXIMUM_PLANT];
    private Plant[] _plants = new Plant[MAXIMUM_PLANT];
    private float[] _deltas = new float[CYCLE_NUMBER];
    private short _plant_counter = MAXIMUM_PLANT - 1;
    private short _max_plant_index = MAXIMUM_PLANT - 1;
    private bool _pool_modified = true;
    private byte _frame_index = 0;
    private short _plant_to_update = 0;
    private short _plant_remained = 0;
    private short _pool_size = 0;
    private Mutex mutex_size = new Mutex();


    private List<short> _free_plant_index = new List<short>();
    public override void _Ready()
    {
#if UNIT_TEST
            _Test();
#endif

#if MEMORY_LOG
            unsafe{
                int mem_plat_size = System.Runtime.InteropServices.Marshal.SizeOf(typeof(Plant));
                GD.Print("Plant struct size : " + mem_plat_size.ToString());
                GD.Print("Plants array size : " + (mem_plat_size * _plants.Length).ToString());
            }
#endif
        for (int i = 0; i < MAXIMUM_PLANT; i++)
        {
            _plants[i] = new Plant(Transform2D.Identity, Plant.Type.FLOWER);
        }
        GD.Print("");
        Entity ent = new Entity();
        GD.Print(ent.GetClass());
        AddChild(ent);
    }

    public bool AddPlant(Transform2D transform, Plant.Type type)
    {
        short plant_index = 0;
        if (_max_plant_index >= MAXIMUM_PLANT)
            return false;

        if (_free_plant_index.Count() > 0)
        {
            plant_index = _free_plant_index.Min();
        }
        else
        {
            plant_index = _max_plant_index;
            _max_plant_index += 1;
        }
        ref Plant plant = ref _plants[plant_index];
        plant.Set(transform, type, plant_index);
        _pool_modified = true;
        _plant_counter += 1;
        return true;
    }

    public void RemovePlantFromIndex(short index)
    {
        if (index >= _max_plant_index)
            return;
        ref Plant plant = ref _plants[index];
        plant.Reset();
        if (index == _max_plant_index - 1)
        {
            short id = index;
            while (_plants[id].type == Plant.Type.UNDEFINED)
                id--;
            _max_plant_index = id;
        }

        _free_plant_index.Add(index);
        _pool_modified = true;
        _plant_counter -= 1;
    }

    public void RemovePlant(ref Plant plant)
    {
        plant.Reset();
        mutex_size.Lock();
        if (plant.index == _max_plant_index - 1)
        {
            short id = plant.index;
            while (_plants[id].type == Plant.Type.UNDEFINED)
                id--;
            _max_plant_index = id;
        }

        _pool_modified = true;
        mutex_size.Unlock();
        _plant_counter -= 1;
    }

    public override void _Process(double delta)
    {
        // BUG need to increase only frame used
        for (byte _delta_index = 0; _delta_index < _deltas.Length; _delta_index++)
            _deltas[_delta_index] += (float)delta;
        //
        if (_is_new_cycle() && _pool_modified)
        {
            _pool_modified = false;
            _plant_to_update = (short)(_max_plant_index + 1);
            _plant_remained = _plant_to_update;
            _pool_size = _get_pool_size(_plant_to_update);
        }
        if (_pool_size == 0)
            return;
        //
        long taskId = WorkerThreadPool.AddGroupTask(Callable.From<int>(Update), Math.Min(_pool_size, _plant_remained));
        WorkerThreadPool.WaitForGroupTaskCompletion(taskId);
        _deltas[_frame_index] = 0.0f;
        _frame_index += 1;
        _plant_remained -= _pool_size;
        //
        if (_plant_remained <= 0)
        {
            _plant_remained = _plant_to_update;
            _frame_index = 0;
        }
    }

    private void Update(int plant_index)
    {
        ref Plant plant = ref _plants[_pool_size * _frame_index + plant_index];
        plant.time_elapsed += _deltas[_frame_index];
        if (plant.time_elapsed >= GROWING_TIME)
        {
            if (plant.maturity == Plant.Maturity.DEAD)
            {
                RemovePlant(ref plant);
                return;
            }
            plant.maturity += 1;
        }


    }

    private bool _is_new_cycle()
    {
        return _frame_index == 0;
    }

    private short _get_pool_size(short plant_count)
    {
        return (short)Math.Ceiling(plant_count / (double)CYCLE_NUMBER);
    }

    #region UnitTest
    private void _Test()
    {
        GD.Print("Unit Test : PlantManager");
        const byte COUNT = 7;
        byte sucess = COUNT;
        if (_get_pool_size(-1) != 0)
        {
            GD.PrintErr("Error - _get_pool_size - args: -1");
            sucess -= 1;
        }
        if (_get_pool_size(0) != 0)
        {
            GD.PrintErr("Error - _get_pool_size - args : 0");
            sucess -= 1;
        }
        if (_get_pool_size(1) != 1)
        {
            GD.PrintErr("Error - _get_pool_size - args : 1");
            sucess -= 1;
        }
        if (_get_pool_size(59) != 1)
        {
            GD.PrintErr("Error - _get_pool_size - args : 59");
            sucess -= 1;
        }
        if (_get_pool_size(60) != 1)
        {
            GD.PrintErr("Error - _get_pool_size - args : 60");
            sucess -= 1;
        }
        if (_get_pool_size(61) != 2)
        {
            GD.PrintErr("Error - _get_pool_size - args : 61");
            sucess -= 1;
        }
        if (_get_pool_size(10000) != 167)
        {
            GD.PrintErr("Error - _get_pool_size - args : 10000");
            sucess -= 1;
        }
        GD.Print("PlantManager Done - Result : " + sucess.ToString() + "/" + COUNT.ToString());
    }
    #endregion
}

