using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    PlayerInput input;
    PlayerControlSchema controls;
    private Animator animator;
    private float velocityX = 0f;
    private float velocityZ = 0f;
    private string playerName;
    int velocityZHash, velocityXHash;
    [SerializeField]
    float acceleration = 4f;
    [SerializeField]
    float deceleration = 4f;
    [SerializeField]
    float maximumRunVelocity = 1.5f;
    [SerializeField]
    float maximumWalkVelocity = 0.5f;
    private Vector2 movement = new Vector2();
    bool isRunning = false;
    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();
        velocityXHash = Animator.StringToHash("Velocity X");
        velocityZHash = Animator.StringToHash("Velocity Z");
        playerName = gameObject.transform.parent.name;

   
        controls = new PlayerControlSchema();
        controls.Enable();
        input.defaultControlScheme = playerName == "Player1" ? "Controller" : "Keyboard";
        controls.Player.Movement.performed += ctx => movement = ctx.ReadValue<Vector2>();
        controls.Player.Movement.canceled += ctx => movement = Vector2.zero;
        controls.Player.Sprint.performed += ctx => isRunning = true;
        controls.Player.Sprint.canceled += ctx => isRunning = false;

    }
    private void Update()
    {
        //bool runPressed = Input.GetKey(KeyCode.LeftShift);
        bool runPressed = Input.GetButton(playerName == "Player1" ? "Sprint_P1": "Sprint_P2");
        //Direction and max velocity for animation
        float currentMaxVelocity = isRunning ? maximumRunVelocity : maximumWalkVelocity;
        float verticalDirection = movement.y;
        float horizontalDirection = movement.x;
        //animation render
        AnimationRenderer(verticalDirection, horizontalDirection, currentMaxVelocity);

        //player movement
        AddPlayerMovement(verticalDirection, horizontalDirection, currentMaxVelocity);
    }
    private void Movement_Performed(InputAction.CallbackContext ctx)
    {
        Debug.Log(ctx);
    }
    void AddPlayerMovement(float verticalMovement , float horizontalMovement, float speed )
    {
        verticalMovement *= Time.deltaTime * speed * 2;
        horizontalMovement *= Time.deltaTime * speed * 2;
        transform.Translate(horizontalMovement, 0, verticalMovement);
    }
    void AnimationRenderer(float verticalMovement, float horizontalMovement, float currentMaxVelocity)
    {
        AxisMovementControl(ref velocityZ, verticalMovement, currentMaxVelocity);
        AxisMovementControl(ref velocityX, horizontalMovement, currentMaxVelocity);

        animator.SetFloat(velocityXHash, velocityX);
        animator.SetFloat(velocityZHash, velocityZ);
    }
    void AxisMovementControl(ref float velocity, float movementValue, float currentMaxVelocity)
    {
        //movement
        if ((movementValue == 1 && velocity <= currentMaxVelocity) || (movementValue == -1 && velocity >= -currentMaxVelocity)  )
        {
            velocity = Mathf.Clamp(velocity + movementValue * Time.deltaTime * acceleration, -currentMaxVelocity, currentMaxVelocity);
        }
        //lock to max velocity
        else if ( movementValue != 0 && (velocity > currentMaxVelocity || velocity < -currentMaxVelocity))
        {
            if (velocity > (currentMaxVelocity - 0.05) && (velocity < currentMaxVelocity + 0.05))
            {
                velocity = currentMaxVelocity;
            }
            else if (velocity > currentMaxVelocity || velocity < -currentMaxVelocity)
            {
                velocity += -movementValue * Time.deltaTime * deceleration;
            }
        }
        //decelerate -> adds on top of opposite direction to decelerate faster
        if (((movementValue == 0 || movementValue == 1) && velocity<0f) || ((movementValue == 0 || movementValue == -1) && velocity > 0f))
        //else if (movementValue == 0 && velocity != 0f)
        {
            if (velocity > -0.05 && velocity < 0.05)
            {
                velocity = 0;
                return;
            }
            velocity += (velocity > 0f ? -1 : 1) * Time.deltaTime * deceleration;
        }
    }
    public void OnMove(InputAction.CallbackContext ctx)
    {
        movement = ctx.ReadValue<Vector2>();
    }
    public void OnSprint(InputAction.CallbackContext ctx)
    {
        isRunning = ctx.action.triggered;
    }
}
