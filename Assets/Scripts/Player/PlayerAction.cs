using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    Animator animator;
    int rotationHash, postureX, postureY, jab, hook, isLeftPunch;
    string activePunch;
    float lastMouseXPos = 0;
    bool punchStarted = false;


    [SerializeField]
    [Range(0f, 10f)]
    private float rotationSpeed = 2.5f;


    public Collider leftFist;
    public Collider rightFist;

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
            activePunch = animator.GetBool(isLeftPunch) ?  "jab" : "strongPunch";
            animator.SetTrigger(jab);
        }
        if (Input.GetMouseButtonDown(1))
        {
            activePunch = "hook";
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
        Vector2 mousePos = (((Vector2)Input.mousePosition - new Vector2(Screen.width / 2, Screen.height / 2)) / new Vector2(Screen.width, Screen.height)) * 2;
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
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Player" 
            || collision.GetContact(0).thisCollider.tag != "Fist" || punchStarted)
            return;
        punchStarted = true;
        AudioManager.Instance.PunchSound(activePunch);
        if (collision.GetContact(0).otherCollider.tag != "Target")
        {
            return;
        }


    }


    private void PunchAnimationFinished()
    {
        punchStarted = false;
    }
}
