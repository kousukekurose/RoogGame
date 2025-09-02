using UnityEngine;
using UnityEngine.InputSystem;


public enum PlayerState1
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
    public PlayerState1 currentState;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        if (moveInput != Vector2.zero)
        {
            currentState = PlayerState1.Move;
        }
        else
        {
            currentState = PlayerState1.Idle;
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("攻撃");
            currentState = PlayerState1.Attack;
        }
        currentState = moveInput != Vector2.zero ? PlayerState1.Move : PlayerState1.Idle;
    }

    public void FixedUpdate()
    {
        Debug.Log(currentState + "現在のステート");
        if (moveInput == Vector2.zero && currentState == PlayerState1.Move)
        {
            currentState = PlayerState1.Idle;
        }

        switch (currentState)
        {
            case PlayerState1.Idle:
                break;
            case PlayerState1.Move:
                Vector3 movement = new Vector3(moveInput.x, 0, moveInput.y) * moveSpeed * Time.fixedDeltaTime;
                rb.MovePosition(rb.position + movement);
                break;
            case PlayerState1.Attack:
                break;
            default:
                break;
        }
    }

}
