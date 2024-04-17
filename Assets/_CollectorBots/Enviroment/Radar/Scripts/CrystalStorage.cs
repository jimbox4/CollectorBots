using System;

public class CrystalStorage
{
    private int _value;

    public event Action ValueChanged;

    public int Value => _value;

    public void AddValue(int value)
    {
        if (value <= 0)
        {
            return;
        }

        _value += value;

        ValueChanged.Invoke();
    }
}
