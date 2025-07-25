using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private List<TMP_Text> _indexes;
    
    private Renderer _renderer;
    private Rigidbody _rigidbody;

    private int _index;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void Initialize(int index, Color color)
    {
        _indexes.ForEach(i => i.text = index.ToString());
        _renderer.material.color = color;
    }
}