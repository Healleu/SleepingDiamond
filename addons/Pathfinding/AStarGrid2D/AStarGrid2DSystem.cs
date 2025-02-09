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

    public List<Vector2I> GetCellPath(Vector2I start, Vector2I end)
    {
        List<Vector2I> path;

        // check cache
        if (_enable_cache)
        {
            Vector2I key = GetKeyFromVector2I(start, end);
            path = _cache.GetValueOrDefault(key, null);
            if (path != null)
            {
                return path;
            }
            Vector2I key_reversed = GetKeyFromVector2I(end, start);
            path = _cache.GetValueOrDefault(key_reversed, null);
            if (path != null)
            {
                return path.Reversed();
            }
        }

        // get path
        path = new List<Vector2>(_astar_grid.GetPointPath(start, end));
        // save in cache
        _cache.Add(GetKeyFromVector2I(start, end), path);


        return path;
    }

    public void ComputeCellPath(Vector2I start, Vector2I end)
    {
        // check cache
        if (_enable_cache)
        {
            Vector2I key = GetKeyFromVector2I(start, end);
            if (_cache.ContainsKey(key))
            {
                return;
            }
        }

        // get path
        Vector2[] path;
        path =  _astar_grid.GetPointPath(start, end);

        // save in cache
        if (_enable_cache)
        {
            _cache.Add(GetKeyFromVector2I(start, end), path);
        }
        
    }

    public Vector2[] GetPathFromCache(Vector2I start, Vector2I end)
    {
        Vector2I key = GetKeyFromVector2I(start, end);
        path = _cache.GetValueOrDefault(key, null);
        return path;
    }

    private Vector2I GetKeyFromVector2I(Vector2I start, Vector2I end)
    {
        return new Vector2I(start.GetHashCode(), end.GetHashCode());
    }

}