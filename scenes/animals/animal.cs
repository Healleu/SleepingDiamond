using Godot;

public partial class Animal : Entity2D
{
    public enum SexeState : byte {
	    UNDEFINED,
	    MALE,
	    FEMALE,
    }

    protected byte _age = 0;
    protected string _birthdate = "0101";
    protected SexeState _sexe = SexeState.UNDEFINED;
 
    protected byte _food = 100;
    protected byte _sleep = 100;
    protected bool _can_reproduce = false;

    public SexeState GetSexe()
    {
        return _sexe;
    }

    public bool CanReproduce()
    {
        return _can_reproduce;
    }

}
