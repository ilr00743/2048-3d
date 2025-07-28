using System;

public interface ICubeFieldManager
{
    event Action FieldOverflowed;
    void AddCube(Cube cube);
    void RemoveCube(Cube cube);
    int GetCubesCount();
    bool IsFieldFull();
    void Clear();
} 