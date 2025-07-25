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
    private bool _isKinematic;

    public int Index => _index;
    public event Action<Cube, Cube> Collide;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Initialize(int index, Color color)
    {
        _index = index;
        _indexes.ForEach(i => i.text = index.ToString());
        _renderer.material.color = color;
        SetKinematic(true);
    }

    public void PushForward(float force)
    {
        _rigidbody.AddForce(Vector3.forward * force, ForceMode.Impulse);
    }
    
    public void Rotate(Vector3 torque)
    {
        _rigidbody.AddTorque(torque, ForceMode.Impulse);
    }

    public void SetKinematic(bool isKinematic)
    {
        _rigidbody.isKinematic = isKinematic;
        _rigidbody.constraints = isKinematic ? RigidbodyConstraints.FreezeAll : RigidbodyConstraints.None;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Cube cube))
        {
            OnCollideWithCube(cube);
        }
    }

    private void OnCollideWithCube(Cube cube)
    {
        if (cube._isKinematic) return;

        if (cube.Index == Index)
        {
            Collide?.Invoke(cube, this);
            Debug.Log($"Same cubes collided: cube 1 - {cube.Index}, cube 2 - {Index}", this);
        }
    }
}