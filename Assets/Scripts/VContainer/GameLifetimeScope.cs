using Configs;
using Cubes;
using Interfaces;
using Randomizer;
using Services;
using UI;
using UnityEngine;
using VContainer.Unity;

namespace VContainer
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private CubesConfig _cubesConfig;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_cubesConfig);
            builder.Register<IGameConfig, GameConfig>(Lifetime.Singleton);

            builder.Register<INumberProvider, NumberProvider>(Lifetime.Singleton);
            builder.Register<ICubeRandomizer, WeightedCubeRandomizer>(Lifetime.Singleton);
            builder.Register<ICubesCombiner, CubesCombiner>(Lifetime.Singleton);
            builder.Register<IScoreService, ScoreService>(Lifetime.Singleton);
            builder.Register<ICubeFieldManager>(container => 
                new CubeFieldManager(_cubesConfig.MaxCubesOnField), Lifetime.Singleton);

            var cubeProvider = FindObjectOfType<CubeProvider>();
            var scoreLabel = FindObjectOfType<ScoreLabel>();
            var cubeLauncher = FindObjectOfType<CubeLauncher>();
            var losePanelController = FindObjectOfType<LosePanelController>();

            if (cubeProvider != null)
            {
                builder.RegisterComponent(cubeProvider);
                builder.RegisterComponent<ICubeProvider>(cubeProvider);
            }

            if (scoreLabel != null)
            {
                builder.RegisterComponent(scoreLabel);
                builder.RegisterComponent<IScoreLabel>(scoreLabel);
            }

            if (cubeLauncher != null)
            {
                builder.RegisterComponent(cubeLauncher);
                builder.RegisterComponent<ICubeLauncher>(cubeLauncher);
            }

            if (losePanelController != null)
            {
                builder.RegisterComponent(losePanelController);
            }

            builder.RegisterEntryPoint<GameEntryPoint>();
        }
    }
}
