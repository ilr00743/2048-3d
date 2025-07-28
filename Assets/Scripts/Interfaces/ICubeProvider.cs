using Configs;
using Cubes;

namespace Interfaces
{
    public interface ICubeProvider
    {
        Cube CreateCube();
        CubeData GetNextCubeData(int currentNumber);
    }
} 