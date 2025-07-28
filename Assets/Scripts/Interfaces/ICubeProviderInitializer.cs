using Configs;

public interface ICubeProviderInitializer
{
    void Initialize(INumberProvider numberProvider, ICubeRandomizer cubeRandomizer);
} 