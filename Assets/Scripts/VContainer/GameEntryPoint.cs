using System;
using System.Threading;
using System.Threading.Tasks;
using Cubes;
using Interfaces;
using UI;
using UnityEngine;
using VContainer.Unity;

namespace VContainer
{
    public class GameEntryPoint : IAsyncStartable
    {
        private readonly CubeLauncher _cubeLauncher;
        private readonly ICubeProvider _cubeProvider;
        private readonly IScoreService _scoreService;
        private readonly ICubesCombiner _cubesCombiner;
        private readonly ICubeFieldManager _cubeFieldManager;
        private readonly LosePanelController _losePanelController;

        public GameEntryPoint(
            CubeLauncher cubeLauncher,
            ICubeProvider cubeProvider,
            IScoreService scoreService,
            ICubesCombiner cubesCombiner,
            ICubeFieldManager cubeFieldManager,
            LosePanelController losePanelController)
        {
            _cubeLauncher = cubeLauncher;
            _cubeProvider = cubeProvider;
            _scoreService = scoreService;
            _cubesCombiner = cubesCombiner;
            _cubeFieldManager = cubeFieldManager;
            _losePanelController = losePanelController;
        }

        public async Task StartAsync(CancellationToken cancellation)
        {
            Application.targetFrameRate = 60;
            
            _cubeLauncher.CubeDetached += OnCubeDetached;
            _cubesCombiner.Combined += OnCubesCombined;
            _cubeFieldManager.FieldOverflowed += OnFieldOverflowed;

            var newCube = _cubeProvider.CreateCube();
            _cubeLauncher.AttachCube(newCube);
        }

        private async void OnCubeDetached(Cube cube)
        {
            _cubeFieldManager.AddCube(cube);
            cube.Collided += OnCubesCollided;
            await Task.Delay(TimeSpan.FromSeconds(1));
        
            if (!_cubeFieldManager.IsFieldFull())
            {
                var newCube = _cubeProvider.CreateCube();
                _cubeLauncher.AttachCube(newCube);
            }
        }

        private void OnCubesCollided(Cube cube1, Cube cube2)
        {
            _scoreService.AddScore(cube1.Number);
        
            _cubeFieldManager.RemoveCube(cube1);
            _cubeFieldManager.RemoveCube(cube2);
        
            var nextCubeData = _cubeProvider.GetNextCubeData(cube1.Number);
            _cubesCombiner.Combine(cube1, cube2, nextCubeData);

            cube1.Collided -= OnCubesCollided;
            cube2.Collided -= OnCubesCollided;
        }

        private void OnCubesCombined(Cube cube)
        {
            _cubeFieldManager.AddCube(cube);
            cube.Collided += OnCubesCollided;
        }

        private void OnFieldOverflowed()
        {
            _losePanelController.Show();
            _cubeLauncher.gameObject.SetActive(false);
        }
    }
} 