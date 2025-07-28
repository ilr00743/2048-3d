using UnityEngine;

public class NumberProvider
{
    private const int Base = 2;

    public int GetNumber(int power)
    {
        return (int)Mathf.Pow(Base, power);
    }
    
    public int GetNextNumber(int currentNumber)
    {
        return GetNumber(GetNextPower(currentNumber));
    }

    public int GetPower(int number)
    {
        return (int)Mathf.Log(number, Base);
    }
    
    public int GetNextPower(int number)
    {
        return GetPower(number) + 1;
    }
}