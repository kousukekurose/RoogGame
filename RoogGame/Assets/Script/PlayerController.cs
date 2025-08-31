using UnityEngine;
using UnityEngine.InputSystem;


public enum PlayerState
{
    Idle,
    Move,
    Attack
}

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody rb;
    private Vector2 moveInput;
    public PlayerState currentState;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        if (moveInput != Vector2.zero)
        {
            currentState = PlayerState.Move;
        }
        else
        {
            currentState = PlayerState.Idle;
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("攻撃");
            currentState = PlayerState.Attack;
        }
        currentState = moveInput != Vector2.zero ? PlayerState.Move : PlayerState.Idle;
    }

    public void FixedUpdate()
    {
        Debug.Log(currentState + "現在のステート");
        if (moveInput == Vector2.zero && currentState == PlayerState.Move)
        {
            currentState = PlayerState.Idle;
        }

        switch (currentState)
        {
            case PlayerState.Idle:
                break;
            case PlayerState.Move:
                Vector3 movement = new Vector3(moveInput.x, 0, moveInput.y) * moveSpeed * Time.fixedDeltaTime;
                rb.MovePosition(rb.position + movement);
                break;
            case PlayerState.Attack:
                break;
            default:
                break;
        }
    }

}
