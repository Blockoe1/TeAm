using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

//Unity sucks, they spell behavior with a U
public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private int maxCyoteTime = 10;

    [SerializeField] private float maxSpeed = 8f;
    [SerializeField] private float acceleration = 8f;

    [SerializeField] private float jumpForce;

    private Rigidbody2D pRigidBody2D;
    private PlayerInput pInput;

    private InputAction move;
    private InputAction jump;

    private float velocityX;

    private int cyoteTimer;

    private bool isMoving = false;
    private bool jumping = false;

    private float moveDirection;
    public bool IsOnGround() => Physics2D.BoxCast(transform.position, Vector2.one * 1.8f, 0, Vector2.down, 0.255f, 1 << LayerMask.NameToLayer("Ground"));//Physics2D.Raycast(transform.position, Vector2.down, 1.017f, 1 << LayerMask.NameToLayer("Ground"));

    [SerializeField] private int _gasLayer = 9;
    [Header("End Scenes")]
    [SerializeField] private int _loseScene;
    [SerializeField] private int _winScene;

    public bool IsMoving { get => isMoving; set => isMoving = value; }
    public bool IsJumping { get => jumping; set => jumping = value; }
    public float MoveDirection { get => moveDirection; set => moveDirection = value; }

    private void Awake()
    {
        pRigidBody2D = GetComponent<Rigidbody2D>();
        pInput = GetComponent<PlayerInput>();

        pInput.currentActionMap.Enable();
        move = pInput.currentActionMap.FindAction("Move");
        jump = pInput.currentActionMap.FindAction("Jump");

        move.started += Move_started;
        move.canceled += Move_canceled;
        jump.started += Jump_started;
        jump.canceled += Jump_canceled;
    }



    private void Move_started(InputAction.CallbackContext obj)
    {
        isMoving = true;
    }
    private void Move_canceled(InputAction.CallbackContext obj)
    {
        isMoving = false;
    }

    private void Jump_started(InputAction.CallbackContext obj)
    {
        jumping = true;
    }
    private void Jump_canceled(InputAction.CallbackContext obj)
    {
        jumping = false;
    }

    private void OnDestroy()
    {
        move.started -= Move_started;
        move.canceled -= Move_canceled;
        jump.started -= Jump_started;
        jump.canceled -= Jump_canceled;
    }

    private void FixedUpdate()
    {
        bool justJumped = false;
        if (jumping && cyoteTimer > 0)
        {
            pRigidBody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            cyoteTimer = 0;
            justJumped = true;
        }

        if (!IsOnGround())
        {
            cyoteTimer--;
        } 
        else if(!justJumped)
        {
            cyoteTimer = maxCyoteTime;
        }

        velocityX = Mathf.MoveTowards(velocityX, move.ReadValue<float>() * maxSpeed, acceleration);
        pRigidBody2D.linearVelocity = new(velocityX, pRigidBody2D.linearVelocity.y);

        
        moveDirection = move.ReadValue<float>();
        PlayerAnimation.Instance.FlipSprite();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == _gasLayer)
        {
            Debug.Log("Hit");
            PlayerStatScript.RemainingHealth--;
            if (PlayerStatScript.RemainingHealth <= 0)
            {
                Debug.Log("RIP");
                SceneManager.LoadScene(_loseScene);
            }
        }
    }
}
