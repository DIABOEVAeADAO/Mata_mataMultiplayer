using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;

public class PlayerMove2D : NetworkBehaviour
{
    public float velocity = 5f;
    public InputActionReference MoveAction;

    private Rigidbody2D rb;
    private float inputX;

    void OnEnable() => MoveAction.action.Enable();
    void OnDisable() => MoveAction.action.Disable();
    void Start() => rb = GetComponent<Rigidbody2D>();

    void Update()
    {
        if (!IsOwner) return;
        inputX = MoveAction.action.ReadValue<Vector2>().x;
    }

    void FixedUpdate()
    {
        if (!IsOwner) return;

        Vector2 targetPosition = rb.position + new Vector2(inputX * velocity * Time.fixedDeltaTime, 0f);
        rb.MovePosition(targetPosition);
    }
}