using Configs;

public class GameConfig : IGameConfig
{
    public CubesConfig CubesConfig { get; }

    public GameConfig(CubesConfig cubesConfig)
    {
        CubesConfig = cubesConfig;
    }
} 