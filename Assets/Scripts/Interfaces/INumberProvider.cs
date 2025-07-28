public interface INumberProvider
{
    int GetNumber(int power);
    int GetNextNumber(int currentNumber);
    int GetPower(int number);
    int GetNextPower(int number);
} 