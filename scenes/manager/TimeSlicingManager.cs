using System.Collections.Generic;
using System;
using System.Linq;
using Godot;



public partial class TimeSlicingManager : Node
{
    const float GROWING_TIME = 5.0f;
    private const ushort MAXIMUM_PLANT = 8000;
    private TimeSlicing timeSlicing = new TimeSlicing();
    private Mutex mutex_size = new Mutex();

    private List<Plant> _plants = new List<Plant>();
    private List<Plant> _plants_to_delete = new List<Plant>();
    private List<Plant> _plants_to_add = new List<Plant>();
    private short _max_plant_index = 0;
    private short _plant_counter = 0;
    private List<short> _free_plant_index = new List<short>();
    public override void _Ready()
    {

        /*
        #if MEMORY_LOG
            unsafe{
                int mem_plat_size = System.Runtime.InteropServices.Marshal.SizeOf(typeof(Plant));
                GD.Print("Plant struct size : " + mem_plat_size.ToString());
                GD.Print("Plants array size : " + (mem_plat_size * _plants.Length).ToString());
            }
        #endif
        */
        for (int i = 0; i < MAXIMUM_PLANT; i++)
        {
            _plants.Add(new Plant(Transform2D.Identity, Plant.Type.FLOWER));
        }
    }

    public bool AddPlant(Transform2D transform, Plant.Type type)
    {
        if (_max_plant_index >= MAXIMUM_PLANT)
            return false;

        _plants_to_add.Add(new Plant(transform, type));
        return true;
    }

    public void RemovePlant(Plant plant)
    {
        _plants_to_delete.Add(plant);
    }

    public override void _Process(double delta)
    {
        timeSlicing.Update(delta, _plants, Update);
        if (timeSlicing.IsNewCycle())
        {
            if (_plants_to_add.Count > 0)
            {
                _plants.AddRange(_plants_to_add);
                _plants_to_add.Clear();
                timeSlicing.SetPoolModified();
            }
            if (_plants_to_delete.Count > 0)
            {
                foreach (var plant in _plants_to_delete)
                {
                    _plants.Remove(plant);
                }
                _plants_to_delete.Clear();
                timeSlicing.SetPoolModified();
            }
        }
    }

    private void Update(int plant_index)
    {
        Plant plant = _plants[timeSlicing.GetPoolSize() * timeSlicing.GetFrameIndex() + plant_index];
        plant.time_elapsed += timeSlicing.GetDelta(timeSlicing.GetFrameIndex());
        if (plant.time_elapsed >= GROWING_TIME)
        {
            if (plant.maturity == Plant.Maturity.DEAD)
            {
                RemovePlant(plant);
                return;
            }
            plant.maturity += 1;
        }


    }
}

