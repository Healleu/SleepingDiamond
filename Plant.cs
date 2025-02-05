using Godot;

public partial class Plant : RefCounted
{
    public enum Maturity : byte
    {
        UNDEFINED,
        YOUNG,
        GROWING,
        MATURE,
        DEAD,
    }

    public enum Type : byte
    {
        UNDEFINED,
        FLOWER,
        BUSH,
        TREE,
    }

    public Transform2D transform = Transform2D.Identity; // 3 x 8 = 24
    public float time_elapsed = 0.0f; // 4
    public Maturity maturity = Maturity.MATURE; // 1
    public Type type = Type.FLOWER; // 1
    public short index = -1;

    public Plant(Transform2D transform, Type type)
    {
        this.transform = transform;
        this.type = type;
    }

    public void Set(Transform2D transform, Type type, short index)
    {
        this.transform = transform;
        this.type = type;
        this.index = index;
    }

    public void Reset()
    {
        transform = Transform2D.Identity;
        time_elapsed = 0.0f;
        maturity = Maturity.UNDEFINED;
        type = Type.UNDEFINED;
    }

}