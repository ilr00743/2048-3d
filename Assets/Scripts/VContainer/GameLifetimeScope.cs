using VContainer;
using VContainer.Unity;
using Configs;
using UnityEngine;

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
        var levelController = FindObjectOfType<LevelController>();
        var losePanelController = FindObjectOfType<LosePanelController>();

        if (cubeProvider != null)
        {
            builder.RegisterComponent(cubeProvider);
            builder.RegisterComponent<ICubeProvider>(cubeProvider);
            builder.RegisterComponent<ICubeProviderInitializer>(cubeProvider);
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

        if (levelController != null)
        {
            builder.RegisterComponent(levelController);
            builder.RegisterComponent<ILevelController>(levelController);
        }

        if (losePanelController != null)
        {
            builder.RegisterComponent(losePanelController);
        }

        builder.RegisterEntryPoint<GameEntryPoint>();
    }
}
