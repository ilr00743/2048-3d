using Interfaces;
using UnityEngine;

namespace Cubes
{
    public class NumberProvider : INumberProvider
    {
        private const int Base = 2;

        private int GetNumber(int power)
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

        private int GetNextPower(int number)
        {
            return GetPower(number) + 1;
        }
    }
}