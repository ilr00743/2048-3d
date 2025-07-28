using System;
using UnityEngine;

public class CubeLauncher : MonoBehaviour, ICubeLauncher
{
    [SerializeField] private float _minX;
    [SerializeField] private float _maxX;
    [SerializeField] private float _launchForce = 10f;

    private bool _isDragging;
    private Vector3 _dragOffset;
    private Camera _camera;
    private Cube _currentCube;

    public event Action<Cube> CubeDetached;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (_currentCube == null) return;
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartDrag();
        }

        if (Input.GetMouseButton(0) && _isDragging)
        {
            UpdateDrag();
        }

        if (Input.GetMouseButtonUp(0))
        {
            StopDrag();
        }
    }

    private void StartDrag()
    {
        _isDragging = true;
        Vector3 mousePosition = GetMouseWorldPosition();
        _dragOffset = _currentCube.transform.position - mousePosition;
    }

    private void UpdateDrag()
    {
        Vector3 mousePosition = GetMouseWorldPosition();
        Vector3 targetPosition = mousePosition + _dragOffset;

        targetPosition.y = _currentCube.transform.position.y;
        targetPosition.z = _currentCube.transform.position.z;
        targetPosition.x = Mathf.Clamp(targetPosition.x, _minX, _maxX);

        _currentCube.transform.position = targetPosition;
    }

    private void StopDrag()
    {
        _isDragging = false;
        LaunchCube();
        DetachCube();
    }

    private void LaunchCube()
    {
        _currentCube.SetKinematic(false);
        _currentCube.Push(Vector3.forward, _launchForce);
        _currentCube.StopAppearanceAnimation();
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Mathf.Abs(_camera.transform.position.z - _currentCube.transform.position.z);
        return _camera.ScreenToWorldPoint(mousePosition);
    }
    
    public void AttachCube(Cube cube)
    {
        _currentCube = cube;
        _currentCube.SetKinematic(true);
    }
    
    private void DetachCube()
    {
        _currentCube.SetKinematic(false);
        CubeDetached?.Invoke(_currentCube);
        _currentCube = null;
    }
}