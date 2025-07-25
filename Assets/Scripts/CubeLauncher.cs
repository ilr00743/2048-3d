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

    private bool _isDragging;
    private Vector3 _dragOffset;
    private Camera _camera;

    [field: SerializeField] public Cube CurrentCube { get; set; }
    [field: SerializeField] public Cube CubePrefab { get; set; }

    private void Start()
    {
        CurrentCube = Instantiate(CubePrefab, _spawnPoint.position, Quaternion.identity);
        var cubeData = _cubesConfig.Cubes.FirstOrDefault(c => c.Index == 2);
        CurrentCube.Initialize(cubeData.Index, cubeData.Color);
        CurrentCube.GetComponent<Rigidbody>().isKinematic = true;
        CurrentCube.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

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
        _dragOffset = CurrentCube.transform.position - mousePosition;
    }

    private void UpdateDrag()
    {
        Vector3 mousePosition = GetMouseWorldPosition();
        Vector3 targetPosition = mousePosition + _dragOffset;

        targetPosition.y = CurrentCube.transform.position.y;
        targetPosition.z = CurrentCube.transform.position.z;
        targetPosition.x = Mathf.Clamp(targetPosition.x, _minX, _maxX);

        CurrentCube.transform.position = targetPosition;
    }

    private void StopDrag()
    {
        _isDragging = false;
        LaunchCube();
        CurrentCube = null;
        CurrentCube = Instantiate(CubePrefab, _spawnPoint.position, Quaternion.identity);
        var cubeData = _cubesConfig.Cubes.FirstOrDefault(c => c.Index == 4);
        CurrentCube.Initialize(cubeData.Index, cubeData.Color);
        CurrentCube.GetComponent<Rigidbody>().isKinematic = true;
        CurrentCube.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }

    private void LaunchCube()
    {
        Rigidbody cubeRb = CurrentCube.GetComponent<Rigidbody>();

        if (cubeRb != null)
        {
            CurrentCube.GetComponent<Rigidbody>().isKinematic = false;
            CurrentCube.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

            Vector3 launchDirection = Vector3.forward;
            cubeRb.AddForce(launchDirection * _launchForce, ForceMode.Impulse);
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Mathf.Abs(_camera.transform.position.z - CurrentCube.transform.position.z);
        return _camera.ScreenToWorldPoint(mousePosition);
    }
}