using Configs;
using Interfaces;

namespace Services
{
    public class GameConfig : IGameConfig
    {
        public CubesConfig CubesConfig { get; }

        public GameConfig(CubesConfig cubesConfig)
        {
            CubesConfig = cubesConfig;
        }
    }
} 