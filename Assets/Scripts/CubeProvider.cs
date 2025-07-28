using System.Collections.Generic;
using System.Linq;
using Configs;
using UnityEngine;
using Random = UnityEngine.Random;
using VContainer;

public class CubeProvider : MonoBehaviour, ICubeProvider, ICubeProviderInitializer
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private CubesConfig _cubesConfig;
    
    private INumberProvider _numberProvider;
    private ICubeRandomizer _cubeRandomizer;
    private List<CubeData> _tempData = new();

    [Inject]
    public void Construct(INumberProvider numberProvider, ICubeRandomizer cubeRandomizer)
    {
        _numberProvider = numberProvider;
        _cubeRandomizer = cubeRandomizer;
    }

    public void Initialize(INumberProvider numberProvider, ICubeRandomizer cubeRandomizer)
    {
        _numberProvider = numberProvider;
        _cubeRandomizer = cubeRandomizer;
    }

    public Cube CreateCube()
    {
        var cubeData = _cubeRandomizer.GetRandomCubeData();
        var cube = Instantiate(_cubePrefab, _spawnPoint.position, Quaternion.identity);
        cube.Initialize(cubeData.Number, cubeData.Color);
        cube.PlayAppearanceAnimation();

        return cube;
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
        return Color.HSVToRGB(Random.Range(0f, 1f), 1f, 1f);
    }
}