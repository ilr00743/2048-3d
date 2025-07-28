using System;
using System.Collections.Generic;
using UnityEngine;

public class CubeFieldManager : ICubeFieldManager
{
    private readonly List<Cube> _cubesOnField = new();
    private readonly int _maxCubesOnField;
    private readonly Action _onFieldOverflow;

    public event Action FieldOverflowed;

    public CubeFieldManager(int maxCubesOnField, Action onFieldOverflow = null)
    {
        _maxCubesOnField = maxCubesOnField;
        _onFieldOverflow = onFieldOverflow;
    }

    public void AddCube(Cube cube)
    {
        if (!_cubesOnField.Contains(cube))
        {
            _cubesOnField.Add(cube);
            CheckFieldOverflow();
        }
    }

    public void RemoveCube(Cube cube)
    {
        _cubesOnField.Remove(cube);
    }

    public int GetCubesCount()
    {
        return _cubesOnField.Count;
    }

    public bool IsFieldFull()
    {
        return _cubesOnField.Count >= _maxCubesOnField;
    }

    private void CheckFieldOverflow()
    {
        if (IsFieldFull())
        {
            FieldOverflowed?.Invoke();
            _onFieldOverflow?.Invoke();
        }
    }

    public void Clear()
    {
        _cubesOnField.Clear();
    }
} 