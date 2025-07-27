using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour
{
    [SerializeField] private List<TMP_Text> _numberTexts;
    [SerializeField] private float _collisionForceThreshold;
    private Renderer _renderer;
    private Rigidbody _rigidbody;
    
    private bool _isKinematic;

    public int Number { get; set; }
    public Color Color { get => _renderer.material.color; set => _renderer.material.color = value; }
    public event Action<Cube, Cube> Collided;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Initialize(int number, Color color)
    {
        Number = number;
        SetNumberTexts(number);
        _renderer.material.color = color;
        SetKinematic(true);
    }

    public void SetNumberTexts(int number)
    {
        _numberTexts.ForEach(num => num.text = number.ToString());
    }

    public void Push(Vector3 direction, float force)
    {
        _rigidbody.AddForce(direction * force, ForceMode.Impulse);
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
            OnCollideWithCube(cube, collision);
        }
    }

    private void OnCollideWithCube(Cube cube, Collision collision)
    {
        if (cube._isKinematic) return;

        if (cube.Number != Number) return;

        var collisionForce = CalculateCollisionForce(collision);
        
        if (collisionForce >= _collisionForceThreshold)
        {
            Collided?.Invoke(cube, this);
            Debug.Log($"Same cubes collided: cube 1 - {cube.Number}, cube 2 - {Number}, current force: {collisionForce}", this);
        }
        else
        {
            Debug.Log($"Collision force is too low. Current force: {collisionForce}, threshold: {_collisionForceThreshold}", this);
        }
    }
    
    private float CalculateCollisionForce(Collision collision)
    {
        return collision.relativeVelocity.magnitude;
    }
}