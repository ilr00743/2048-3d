using System;
using System.Collections.Generic;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "Cubes Config", menuName = "2048/Cubes Config", order = 0)]
    public class CubesConfig : ScriptableObject
    {
        [SerializeField] private List<CubeData> _cubes;
        [SerializeField] private int _maxCubesOnField = 10;
        
        public IReadOnlyList<CubeData> Cubes => _cubes;
        public int MaxCubesOnField => _maxCubesOnField;
    }

    [Serializable]
    public class CubeData
    {
        public int Number;
        public float Probability;
        public Color Color;
    }
}