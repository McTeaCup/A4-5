using UnityEngine;

public class PMovement : MonoBehaviour
{
    PlayerInput _playerInput = default;
    public PlayerProperties objectParameters = default;
    public Camera mainCamera = default;
    private Rigidbody2D _objectRigedbody = default;

    private void Awake()
    {
        _objectRigedbody = GetComponent<Rigidbody2D>();
        _objectRigedbody.gravityScale = 0f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        _playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        transform.position += _playerInput.MovementInput() * objectParameters.movementSpeed * Time.deltaTime;
    }
    private void LateUpdate()
    {
        Vector3 pos = mainCamera.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        transform.position = mainCamera.ViewportToWorldPoint(pos);
    }
}

