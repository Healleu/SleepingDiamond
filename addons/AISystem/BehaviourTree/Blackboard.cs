using Godot;
using Godot.Collections;

public partial class Blackboard
{
    private Dictionary<string, Variant> _contents = new Dictionary<string, Variant>();

    public void SetValue(string key, Variant value)
    {
        if (_contents.ContainsKey(key))
        {
            _contents[key] = value;
        }
        else
        {
            _contents.Add(key, value);
        }
    }

    public Variant GetValue(string key)
    {
        return _contents.ContainsKey(key) ? _contents[key] : default;
    }

    public bool RemoveValue(string key)
    {
        return _contents.Remove(key);
    }

    public void Clear()
    {
        _contents.Clear();
    }

}
