using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerGameInput))]
public class PlayerController : MonoBehaviour
{
    Rigidbody2D playerRigidbody;
    [HideInInspector]
    public PlayerGameInput input;
    public AudioSource landingSFX;
    [SerializeField] PlayerManager pm;
    PhysicsMaterial2D physicsMaterial;
    [SerializeField] Transform spriteTransform;
    //AttackAction attackAction => pm.attackAction;

    float countdown = 0f;

    public Vector3 Velocity { get; private set; }
    public FrameInput Input { get; private set; }
    public bool JumpingThisFrame { get; private set; }
    public bool LandingThisFrame { get; private set; }
    public Vector3 RawMovement { get; private set; }

    public PlayerGameInput playerInput;

    private Vector3 _lastPosition;
    private float _currentHorizontalSpeed, _currentVerticalSpeed, _lastJumpPressed;


    private void GatherInput()
    {
        Input = new FrameInput
        {
            JumpDown = playerInput.jumpPressed,
            JumpUp = playerInput.jumpReleased,
            X = playerInput.horizontal
        };
        if (Input.JumpDown)
        {
            _lastJumpPressed = Time.time;
        }
    }

    private void Awake()
    {
        input = GetComponent<PlayerGameInput>();
    }

    private void Start()
    {
        input.EnableGameplayInputs();
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerInput.jumpAction.started += jumpBegin;
        playerInput.AtkAction.started += atkBegin;
        physicsMaterial = playerRigidbody.sharedMaterial;
        initAtk();
    }

    private void OnDestroy()
    {
        playerInput.jumpAction.started -= jumpBegin;
        playerInput.AtkAction.started -= atkBegin;
    }

    private void FixedUpdate()
    {
        countdown += Time.deltaTime;
        checkGrounded();
        calculateMove();

    }

    private void calculateMove()
    {
        if (input.horizontal > 0)
        {
            spriteTransform.localScale = new Vector3(Mathf.Abs(spriteTransform.localScale.x), spriteTransform.localScale.y, spriteTransform.localScale.z);
        }
        else if (input.horizontal < 0)
        {
            spriteTransform.localScale = new Vector3(-Mathf.Abs(spriteTransform.localScale.x), spriteTransform.localScale.y, spriteTransform.localScale.z);
            //Debug.Log(spriteTransform.localScale);
        }

        if (input.horizontal != 0)
        {
            // Set horizontal move speed
            pm._currentHorizontalSpeed += input.horizontal * pm._acceleration * Time.deltaTime;

            // clamped by max frame movement
            pm._currentHorizontalSpeed = Mathf.Clamp(pm._currentHorizontalSpeed, -pm._moveClamp, pm._moveClamp);

        }
        else
        {
            // No input. Let's slow the character down
            pm._currentHorizontalSpeed = Mathf.MoveTowards(pm._currentHorizontalSpeed, 0, pm._deAcceleration * Time.deltaTime);
        }

        playerRigidbody.velocity = new Vector2(pm._currentHorizontalSpeed, playerRigidbody.velocity.y);
    }
    private void checkGrounded()
    {
        RaycastHit2D hit2D = Physics2D.CircleCast(transform.position, pm.jumpCheckRadius, Vector2.down, pm.jumpCheckDistance, pm.terrainLayer);
        if (hit2D.collider != null)
        {
            LandingThisFrame = false;
            if (playerInput.isGrounded == false)
            {
                LandingThisFrame = true;
                landingSFX.Play();
            }
            //Debug.Log(hit2D.collider.gameObject.name);
            playerInput.isGrounded = true;
            physicsMaterial.friction = pm.groundFriction;

        }
        else
        {
            playerInput.isGrounded = false;
            physicsMaterial.friction = 0;
        }
        if (countdown > pm.jumpCD && playerInput.isGrounded)
        {
            playerInput.jumpOK = true;
        }
        else
        {
            playerInput.jumpOK = false;
        }

    }

    private void jumpBegin(InputAction.CallbackContext callback)
    {
        if (playerInput.jumpOK)
        {
            //Debug.Log(Physics2D.gravity);
            playerRigidbody.velocity = new Vector2(pm._currentHorizontalSpeed, Mathf.Sqrt(2 * Mathf.Abs(Physics2D.gravity.y * playerRigidbody.gravityScale) * pm.jumpHeight));
            countdown = 0;
        }
    }
    
    private void initAtk()
    {
        //attackAction.Init(pm);
    }

    private void atkBegin(InputAction.CallbackContext callback)
    {
        if(playerInput.isGrounded)
        {
            //attackAction.attackBegin((int)playerInput.AtkAction.ReadValue<float>());
        }
    }

    private void dealHorizontalBlocking(Collision2D collision)
    {
        Vector2 closestPoint = collision.collider.ClosestPoint(transform.position);
        Vector2 blockDir = closestPoint - new Vector2(transform.position.x, transform.position.y);
        if (Vector2.Dot(blockDir.normalized, new Vector2(pm._currentHorizontalSpeed, 0).normalized) > 0.5)
        {
            Debug.Log(Vector2.Dot(blockDir.normalized, new Vector2(pm._currentHorizontalSpeed, 0).normalized));
            pm._currentHorizontalSpeed = 0;
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        dealHorizontalBlocking(collision);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        dealHorizontalBlocking(collision);
    }
}
