using System;

public interface ICubeLauncher
{
    event Action<Cube> CubeDetached;
    void AttachCube(Cube cube);
} 