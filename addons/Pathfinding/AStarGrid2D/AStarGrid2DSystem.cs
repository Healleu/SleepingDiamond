using Godot;
using System.Collections.Generic;


public partial class AStarGrid2DSystem : Node
{

    private bool _enable_cache = false;
    private AStarGrid2D _astar_grid = new AStarGrid2D();
    private Dictionary<Vector2I, Vector2[]> _cache = new Dictionary<Vector2I, Vector2[]>();

    public void setup(Rect2I region, Vector2I cell_size, AStarGrid2D.DiagonalModeEnum diagonal_mode = AStarGrid2D.DiagonalModeEnum.Always)
    {
        _astar_grid.Clear();
        _astar_grid.SetRegion(region);
        _astar_grid.SetCellSize(cell_size);
        _astar_grid.SetOffset(cell_size / 2);
        _astar_grid.SetDiagonalMode(diagonal_mode);
        _astar_grid.Update();
    }

    public void Update()
    {
        _astar_grid.Update();
    }

    public Vector2[] GetCellPath(Vector2I start, Vector2I end)
    {
        Vector2[] path;

        // check cache
        if (_enable_cache)
        {
            Vector2I key = GetKeyFromVector2I(start, end);
            path = _cache.GetValueOrDefault(key, null);
            if (path != null)
            {
                return path;
            }
        }

        // get path
        path =  _astar_grid.GetPointPath(start, end);
        // save in cache
        _cache.Add(GetKeyFromVector2I(start, end), path);


        return path;
    }

    private Vector2I GetKeyFromVector2I(Vector2I start, Vector2I end)
    {
        return new Vector2I(start.GetHashCode(), end.GetHashCode());
    }

}