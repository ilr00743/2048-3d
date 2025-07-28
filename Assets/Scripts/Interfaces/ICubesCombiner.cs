using System;
using Configs;
using Cubes;

namespace Interfaces
{
    public interface ICubesCombiner
    {
        event Action<Cube> Combined;
        void Combine(Cube cube1, Cube cube2, CubeData nextCubeData);
    }
} 