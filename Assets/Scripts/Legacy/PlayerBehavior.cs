using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
   
    int isWalkingFHash, isRunningFHash, isWalkingBHash, isRunningBHash;
    //float velocity = 0.0f;
    public float acceleration = 0.1f;
    private Animator animator;
    private Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        isWalkingFHash = Animator.StringToHash("isWalkingF");
        isRunningFHash = Animator.StringToHash("isRunningF");
        isWalkingBHash = Animator.StringToHash("isWalkingB");
        isRunningBHash = Animator.StringToHash("isRunningB");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void MovementController()
    {
        bool forwardPressed = Input.GetKey(KeyCode.W);
        bool backwardPressed = Input.GetKey(KeyCode.S);
        bool runForwardPressed = Input.GetKey(KeyCode.LeftShift) && forwardPressed;
        bool runBackwardPressed = Input.GetKey(KeyCode.LeftShift) && backwardPressed;

        //forward 
        if (animator.GetBool(isWalkingFHash) != forwardPressed)
        {
            animator.SetBool(isWalkingFHash, forwardPressed);
        }
        if (animator.GetBool(isRunningFHash) != runForwardPressed)
        {
            animator.SetBool(isRunningFHash, runForwardPressed);
        }
        //backward
        if (animator.GetBool(isWalkingBHash) != backwardPressed){
            animator.SetBool(isWalkingBHash, backwardPressed);
        }
        if (animator.GetBool(isRunningBHash) != runBackwardPressed)
        {
            animator.SetBool(isRunningBHash, runBackwardPressed);
        }
    }
    
}
