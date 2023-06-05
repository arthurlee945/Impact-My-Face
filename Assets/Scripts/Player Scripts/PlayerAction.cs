using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    Animator animator;
    int rotationHash, postureX, postureY, jab, hook, isLeftPunch;

    [SerializeField]
    [Range(0f, 10f)]
    private float rotationSpeed = 2.5f;
    private float lastMouseXPos = 0;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rotationHash = Animator.StringToHash("Rotation");
        postureX = Animator.StringToHash("Posture X");
        postureY = Animator.StringToHash("Posture Y");
        jab = Animator.StringToHash("Jab");
        hook = Animator.StringToHash("Hook");
        isLeftPunch = Animator.StringToHash("IsLeftPunch");
    }
    private void Update()
    {
        float horizontal = Input.GetAxis("Mouse X");
        updatePlayerPosture();
        rotatePlayer(horizontal);
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger(jab);
        }
        if (Input.GetMouseButtonDown(1))
        {
            animator.SetTrigger(hook);
        }
    }
    void FixedUpdate()
    {

    }
    void rotatePlayer(float horizontalRotation)
    {
        transform.Rotate(0, horizontalRotation * rotationSpeed, 0);
        animator.SetFloat(rotationHash, horizontalRotation);
    }
    void updatePlayerPosture()
    {
        Vector2 mousePos =(((Vector2)Input.mousePosition - new Vector2(Screen.width / 2, Screen.height / 2)) / new Vector2(Screen.width, Screen.height))*2;
        animator.SetFloat(postureX, Mathf.Clamp(mousePos.x, -0.5f, 0.5f));
        animator.SetFloat(postureY, Mathf.Clamp(mousePos.y, -0.5f, 0.5f));

        if(animator.GetBool(isLeftPunch) && mousePos.x > lastMouseXPos)
        {
            animator.SetBool(isLeftPunch, false);
        }
        else if(!animator.GetBool(isLeftPunch) && mousePos.x < lastMouseXPos)
        {
            animator.SetBool(isLeftPunch, true);
        }
        lastMouseXPos = mousePos.x;
    }
}
