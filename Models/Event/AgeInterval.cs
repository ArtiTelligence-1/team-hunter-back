namespace TeamHunter.Models;

public class AgeInterval
{
    public int From {get; set;}
    private int _to;
    public int To {
        get => _to;
        set {
            if(value >= From)
                _to = value;
            else
                throw new ArgumentException();
        }
    }
}