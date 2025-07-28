using System;
using System.Threading.Tasks;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private CubeLauncher _cubeLauncher;
    [SerializeField] private CubeProvider _cubeProvider;
    [SerializeField] private ScoreLabel _scoreLabel;
    
    private CubesCombiner _cubesCombiner;

    private void Awake()
    {
        _cubesCombiner = new CubesCombiner();
        _cubeLauncher.CubeDetached += OnCubeDetached;
        _cubesCombiner.Combined += OnCubesCombined;
    }

    private void Start()
    {
        var newCube = _cubeProvider.CreateCube();
        _cubeLauncher.AttachCube(newCube);
    }

    private async void OnCubeDetached(Cube cube)
    {
        cube.Collided += OnCubesCollided;
        await Task.Delay(TimeSpan.FromSeconds(1));
        var newCube = _cubeProvider.CreateCube();
        _cubeLauncher.AttachCube(newCube);
    }

    private void OnCubesCollided(Cube cube1, Cube cube2)
    {
        _scoreLabel.AddScore(cube1.Number);
        
        var nextCubeData = _cubeProvider.GetNextCubeData(cube1.Number);
        _cubesCombiner.Combine(cube1, cube2, nextCubeData);

        cube1.Collided -= OnCubesCollided;
        cube2.Collided -= OnCubesCollided;
    }

    private void OnCubesCombined(Cube cube)
    {
        cube.Collided += OnCubesCollided;
    }
}