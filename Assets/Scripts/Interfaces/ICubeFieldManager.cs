using System;
using Cubes;

namespace Interfaces
{
    public interface ICubeFieldManager
    {
        event Action FieldOverflowed;
        void AddCube(Cube cube);
        void RemoveCube(Cube cube);
        bool IsFieldFull();
    }
} 