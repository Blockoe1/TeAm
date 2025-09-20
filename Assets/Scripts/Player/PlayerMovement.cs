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

    private bool jumping = false;

    private bool IsOnGround() => Physics2D.BoxCast(transform.position, Vector2.one * 1.9f, 0, Vector2.down, 0.1f, 1 << LayerMask.NameToLayer("Ground"));//Physics2D.Raycast(transform.position, Vector2.down, 1.017f, 1 << LayerMask.NameToLayer("Ground"));

    [SerializeField] private int _gasLayer = 9;
    [Header("End Scenes")]
    [SerializeField] private int _loseScene;
    [SerializeField] private int _winScene;

    private void Awake()
    {
        pRigidBody2D = GetComponent<Rigidbody2D>();
        pInput = GetComponent<PlayerInput>();

        pInput.currentActionMap.Enable();
        move = pInput.currentActionMap.FindAction("Move");
        jump = pInput.currentActionMap.FindAction("Jump");

        jump.started += Jump_started;
        jump.canceled += Jump_canceled;
    }

    private void Jump_canceled(InputAction.CallbackContext obj)
    {
        jumping = false;
    }

    private void Jump_started(InputAction.CallbackContext obj)
    {
        jumping = true;
    }

    private void OnDestroy()
    {
        jump.started -= Jump_started;
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
