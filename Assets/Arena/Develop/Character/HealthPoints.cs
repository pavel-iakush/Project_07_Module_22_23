using UnityEngine;

public class HealthPoints
{
    private int _value;

    public HealthPoints(int value)
    {
        _value = value;
    }

    public int Value
    {
        get
        {
            return _value;
        }
        set
        {
            _value = value;

            if (_value < 0)
            {
                _value = 0;
                Debug.Log("Game over");
            }
        }
    }

    public void TakeDamage(int damage)
    {
        Value -= damage;
        Debug.Log(_value);
    }
}