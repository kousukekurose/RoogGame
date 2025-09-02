using Fusion;
using UnityEngine;

public enum PlayerState
{
    Idle,
    Walking,
    Running,
    Jumping
}

public class Player : NetworkBehaviour
{
    private NetworkCharacterController _cc;
    private PlayerState _state = PlayerState.Idle;
    [SerializeField]
    private float walkSpeed = 5f;

    private void Awake()
    {
        _cc = GetComponent<NetworkCharacterController>();
    }

    public override void FixedUpdateNetwork()
    {
        Debug.Log("現在のステート" +  _state);
        //if (GetInput(out NetworkInputData data))
        //{
        //    data.direction.Normalize();
        //    _cc.Move(5 * data.direction * Runner.DeltaTime);
        //}
        if (GetInput(out NetworkInputData data))
        {
            // ステート判定
            _state = (data.direction.magnitude > 0.1f) ? PlayerState.Walking : PlayerState.Idle;

            // スピード設定
            float speed = 0f;
            switch (_state)
            {
                case PlayerState.Walking:
                    speed = walkSpeed;
                    break;
                //case PlayerState.Running:
                //    speed = walkSpeed;
                //    break;
                //case PlayerState.Jumping:
                //    speed = walkSpeed;
                //    break;
                case PlayerState.Idle:
                default:
                    speed = 0f;
                    break;
            }

            data.direction.Normalize();
            _cc.Move(speed * data.direction * Runner.DeltaTime);
        }
        else
        {
            _state = PlayerState.Idle;
        }
    }
}
