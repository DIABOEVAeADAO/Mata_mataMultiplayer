using UnityEngine;
using UnityEngine.InputSystem;
public class WeaponAim : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float distanceFromPlayer = 1.5f;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        RotateTowardsMouse();
    }

    private void RotateTowardsMouse()
    {
        Vector2 MP = Mouse.current.position.ReadValue();
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(MP);
        Vector2 direction = (mousePosition - playerTransform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.position = playerTransform.position + (Vector3)(direction * distanceFromPlayer);
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}