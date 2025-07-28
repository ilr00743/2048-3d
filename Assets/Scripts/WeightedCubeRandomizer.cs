using System.Collections.Generic;
using System.Linq;
using Configs;
using UnityEngine;

public class WeightedCubeRandomizer : ICubeRandomizer
{
    private readonly List<CubeData> _cubes;
    private float _totalProbability;
    
    public WeightedCubeRandomizer(IGameConfig gameConfig)
    {
        _cubes = gameConfig.CubesConfig.Cubes.ToList();
        CalculateTotalProbability();
    }
    
    public CubeData GetRandomCubeData()
    {
        if (_cubes == null || _cubes.Count == 0)
        {
            return null;
        }
        
        float randomValue = Random.Range(0f, _totalProbability);
        float currentWeight = 0f;
        
        foreach (var cube in _cubes)
        {
            currentWeight += cube.Probability;
            
            if (randomValue <= currentWeight)
            {
                return cube;
            }
        }
        
        return _cubes[0];
    }
    
    private void CalculateTotalProbability()
    {
        _totalProbability = 0f;
        
        foreach (CubeData cube in _cubes)
        {
            _totalProbability += cube.Probability;
        }
    }
}