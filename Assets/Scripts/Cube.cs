using System;
using System.Collections.Generic;
using DG.Tweening;
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
    private Vector3 _defaultScale;
    private bool _isKinematic;

    public bool IsBeingCombined { get; set; }
    public int Number { get; set; }

    public Color Color
    {
        get => _renderer.material.color;
        set => _renderer.material.color = value;
    }

    public event Action<Cube, Cube> Collided;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _rigidbody = GetComponent<Rigidbody>();
        _defaultScale = transform.localScale;
    }

    public void Initialize(int number, Color color)
    {
        Number = number;
        SetNumberTexts(number);
        _renderer.material.color = color;
        SetKinematic(true);
        IsBeingCombined = false;
    }

    public void SetNumberTexts(int number)
    {
        _numberTexts.ForEach(num => num.text = number.ToString());
    }

    public void Push(Vector3 direction, float force)
    {
        _rigidbody.AddForce(direction * force, ForceMode.Impulse);
    }

    private void Rotate(Vector3 torque)
    {
        _rigidbody.AddTorque(torque, ForceMode.Impulse);
    }

    public void SetKinematic(bool isKinematic)
    {
        _isKinematic = isKinematic;
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
        if (IsBeingCombined || cube.IsBeingCombined) return;
        if (cube._isKinematic) return;
        if (cube.Number != Number) return;

        var collisionForce = CalculateCollisionForce(collision);

        if (collisionForce >= _collisionForceThreshold)
        {
            Collided?.Invoke(cube, this);
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

    public void PlayCompleteCombineAnimation()
    {
        if (this == null || transform == null) return;
        
        Push(Vector3.up, 7);
        Rotate(UnityEngine.Random.rotation.eulerAngles);
        transform.localScale = _defaultScale;

        transform.DOShakeScale(0.2f)
            .SetTarget(this)
            .OnComplete(() => 
            {
                if (this != null && transform != null)
                {
                    transform.localScale = _defaultScale;
                }
            });
    }

    public void PlayAppearanceAnimation()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(_defaultScale, 0.3f);
        transform.DORotate(new Vector3(0f, 360f, 0f), 0.3f, RotateMode.FastBeyond360);
    }

    public void StopAppearanceAnimation()
    {
        transform.DOKill();
        transform.localScale = _defaultScale;
    }

    public void OnDestroy()
    {
        DOTween.Kill(transform);
        DOTween.Kill(this);
    }
}