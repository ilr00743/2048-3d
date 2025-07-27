using System;
using System.Collections.Generic;
using System.Linq;
using Configs;
using UnityEngine;
using Random = UnityEngine.Random;

public class CubeProvider : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private CubesConfig _cubesConfig;
    private NumberProvider _numberProvider;

    private List<CubeData> _tempData = new();

    private void Awake()
    {
        _numberProvider = new NumberProvider();
    }

    public Cube CreateCube()
    {
        var cubeData = GetRandomCubeData();
        var cube = Instantiate(_cubePrefab, _spawnPoint.position, Quaternion.identity);
        cube.Initialize(cubeData.Number, cubeData.Color);
        return cube;
    }

    private CubeData GetRandomCubeData()
    {
        return _cubesConfig.Cubes[Random.Range(0, 2)];
    }

    public CubeData GetNextCubeData(int currentNumber)
    {
        var nextNumber = _numberProvider.GetNextNumber(currentNumber);

        var newData = _cubesConfig.Cubes.FirstOrDefault(d => d.Number == nextNumber);

        if (newData != null) return newData;

        if (_tempData.Exists(d => d.Number == nextNumber))
        {
            return _tempData.First(d => d.Number == nextNumber);
        }

        newData = new CubeData { Number = nextNumber, Probability = 0f, Color = GetRandomBrightColor() };
        _tempData.Add(newData);
        
        return newData;
    }

    private Color GetRandomBrightColor()
    {
        return Color.HSVToRGB(
            Random.Range(0f, 1f),
            1f,
            1f
        );
    }
}