using Configs;

public interface ILevelController
{
    void Initialize(INumberProvider numberProvider, ICubeRandomizer cubeRandomizer, ICubesCombiner cubesCombiner);
} 