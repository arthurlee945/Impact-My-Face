using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rb;
    private float velocityX = 0f;
    private float velocityZ = 0f;
    int velocityZHash, velocityXHash;
    [SerializeField]
    float acceleration = 30f;
    [SerializeField]
    float deceleration = 30f;
    [SerializeField]
    float maximumRunVelocity = 1.5f;
    [SerializeField]
    float maximumWalkVelocity = 0.5f;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        velocityXHash = Animator.StringToHash("Velocity X");
        velocityZHash = Animator.StringToHash("Velocity Z");
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        float verticalMovement = Input.GetAxis("Vertical");
        float horizontalMovement = Input.GetAxis("Horizontal");
        bool runPressed = Input.GetKey(KeyCode.LeftShift);
        //Direction and max velocity for animation
        float currentMaxVelocity = runPressed ? maximumRunVelocity : maximumWalkVelocity;
        float verticalDirection = verticalMovement > 0 ? 1 : verticalMovement < 0 ? -1 : 0;
        float horizontalDirection = horizontalMovement > 0 ? 1 : horizontalMovement < 0 ? -1 : 0;
        //animation renderww
        AnimationRenderer(verticalDirection, horizontalDirection, currentMaxVelocity);

        //player movement
        AddPlayerMovement(verticalDirection, horizontalDirection, currentMaxVelocity);
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
    void RotatePlayerRelativeToCamera(float verticalMovement, float horizontalMovement)
    {

    }
}
