using System;
using Configs;
using DG.Tweening;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class CubesCombiner
{
    public event Action<Cube> Combined;
    
    public void Combine(Cube cube1, Cube cube2, CubeData nextCubeData)
    {
        var middlePosition = GetMiddlePosition(cube1, cube2);
        
        cube1.SetKinematic(true);
        cube2.SetKinematic(true);
    
        var duration = 0.08f;
    
        DOTween.Sequence()
            .Join(cube1.transform.DOMove(middlePosition, duration))
            .Join(cube2.transform.DOMove(middlePosition, duration))
            .Join(cube2.transform.DORotate(cube1.transform.rotation.eulerAngles, duration))
            .Join(DOTween.To(() => cube1.Color, color => cube1.Color = color, nextCubeData.Color, duration))
            .Join(DOTween.To(() => cube2.Color, color => cube2.Color = color, nextCubeData.Color, duration))
            .AppendCallback(() => OnCombined(cube1, cube2, nextCubeData.Number));
    }
    
    private void OnCombined(Cube cube1, Cube cube2, int newNumber)
    {
        cube1.SetKinematic(false);
        cube1.Number = newNumber;
        cube1.SetNumberTexts(newNumber);
        Object.Destroy(cube2.gameObject);
    
        Combined?.Invoke(cube1);
    
        cube1.Push(Vector3.up, 7);
        cube1.Rotate(Random.rotation.eulerAngles);
    }
    
    private Vector3 GetMiddlePosition(Cube cube1, Cube cube2)
    {
        var position1 = cube1.transform.position;
        var position2 = cube2.transform.position;
    
        var halfVector = (position1 - position2) / 2f;
        var middlePosition = position2 + halfVector;
    
        return middlePosition;
    }
}