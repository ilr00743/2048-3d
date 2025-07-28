using System;
using System.Threading.Tasks;
using UnityEngine;
using VContainer;

public class LevelController : MonoBehaviour, ILevelController
{
    [SerializeField] private CubeLauncher _cubeLauncher;
    [SerializeField] private CubeProvider _cubeProvider;
    [SerializeField] private ScoreLabel _scoreLabel;

    [Inject]
    public void Construct(INumberProvider numberProvider, ICubeRandomizer cubeRandomizer, ICubesCombiner cubesCombiner)
    {
        _cubeProvider.Initialize(numberProvider, cubeRandomizer);
        _scoreLabel.Initialize(numberProvider);
    }

    public void Initialize(
        INumberProvider numberProvider,
        ICubeRandomizer cubeRandomizer,
        ICubesCombiner cubesCombiner)
    {
        _cubeProvider.Initialize(numberProvider, cubeRandomizer);
        _scoreLabel.Initialize(numberProvider);
    }
}