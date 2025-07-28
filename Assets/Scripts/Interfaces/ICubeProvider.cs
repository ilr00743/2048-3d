using Configs;

public interface ICubeProvider
{
    Cube CreateCube();
    CubeData GetNextCubeData(int currentNumber);
} 