using System;
using System.Collections.Generic;
using Cubes;
using Interfaces;

namespace Services
{
    public class CubeFieldManager : ICubeFieldManager
    {
        private readonly List<Cube> _cubesOnField = new();
        private readonly int _maxCubesOnField;

        public event Action FieldOverflowed;

        public CubeFieldManager(int maxCubesOnField)
        {
            _maxCubesOnField = maxCubesOnField;
        }

        public void AddCube(Cube cube)
        {
            if (!_cubesOnField.Contains(cube))
            {
                _cubesOnField.Add(cube);
                CheckFieldOverflow();
            }
        }

        public void RemoveCube(Cube cube)
        {
            _cubesOnField.Remove(cube);
        }

        public bool IsFieldFull()
        {
            return _cubesOnField.Count >= _maxCubesOnField;
        }

        private void CheckFieldOverflow()
        {
            if (IsFieldFull())
            {
                FieldOverflowed?.Invoke();
            }
        }
    }
} 