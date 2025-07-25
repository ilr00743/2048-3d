using System;
using System.Collections.Generic;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "Cubes Config", menuName = "2048/Cubes Config", order = 0)]
    public class CubesConfig : ScriptableObject
    {
        [SerializeField] private List<CubeData> _cubes;
        public IReadOnlyList<CubeData> Cubes => _cubes;
    }

    [Serializable]
    public class CubeData
    {
        public int Index;
        public float Probability;
        public Color Color;
    }
}