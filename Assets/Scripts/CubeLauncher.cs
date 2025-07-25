using System.Linq;
using Configs;
using UnityEngine;

public class CubeLauncher : MonoBehaviour
{
    [SerializeField] private float _minX;
    [SerializeField] private float _maxX;
    [SerializeField] private float _launchForce = 10f;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private CubesConfig _cubesConfig;
    [SerializeField] private Cube _cubePrefab;

    private bool _isDragging;
    private Vector3 _dragOffset;
    private Camera _camera;
    private Cube _currentCube;


    private void Start()
    {
        _currentCube = Instantiate(_cubePrefab, _spawnPoint.position, Quaternion.identity);
        var cubeData = _cubesConfig.Cubes.FirstOrDefault(c => c.Index == 2);
        
        _currentCube.Initialize(cubeData.Index, cubeData.Color);
        
        _currentCube.SetKinematic(true);

        _camera = Camera.main;
    }

    private void Update()
    {
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
        
        _currentCube = null;
        _currentCube = Instantiate(_cubePrefab, _spawnPoint.position, Quaternion.identity);
        
        var cubeData = _cubesConfig.Cubes.FirstOrDefault(c => c.Index == 4);
        _currentCube.Initialize(cubeData.Index, cubeData.Color);
        
        _currentCube.SetKinematic(true);
    }

    private void LaunchCube()
    {
        _currentCube.SetKinematic(false);
        _currentCube.PushForward(_launchForce);
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Mathf.Abs(_camera.transform.position.z - _currentCube.transform.position.z);
        return _camera.ScreenToWorldPoint(mousePosition);
    }
}