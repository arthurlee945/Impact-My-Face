using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations;
using UnityEngine.Windows;

public class PlayerAction : MonoBehaviour
{
    PlayerInput input;
    PlayerControlSchema controls;
    Animator animator;
    int rotationHash, postureX, postureY, jab, hook, isLeftPunch;
    string activePunch;
    float lastMouseXPos = 0;
    string playerName;
    Vector2 AngleControl;

    public bool punchStarted = false;
    public bool IsGuarding = false;
    private bool isRegularPunch = false;
    private bool isStrongPunch = false;

    private float rotationSpeed = 1.5f;

    public Collider leftFist;
    public Collider rightFist;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();
        rotationHash = Animator.StringToHash("Rotation");
        postureX = Animator.StringToHash("Posture X");
        postureY = Animator.StringToHash("Posture Y");
        jab = Animator.StringToHash("Jab");
        hook = Animator.StringToHash("Hook");
        isLeftPunch = Animator.StringToHash("IsLeftPunch");
        this.playerName = gameObject.transform.parent.name;

        input.defaultControlScheme = playerName == "Player1" ? "Controller" : "Keyboard";
        controls = new PlayerControlSchema();
        controls.Enable();
        controls.Player.AngleControl.performed += ctx => AngleControl = ctx.ReadValue<Vector2>();
        controls.Player.AngleControl.canceled += ctx => AngleControl = Vector2.zero;
        controls.Player.Guarding.performed += ctx => IsGuarding = true;
        controls.Player.Guarding.canceled += ctx => IsGuarding = false;
        controls.Player.Regular_P.performed += ctx => isRegularPunch = true;
        controls.Player.Regular_P.canceled += ctx => isRegularPunch = false;
        controls.Player.Strong_P.performed += ctx => isStrongPunch = true;
        controls.Player.Strong_P.canceled += ctx => isStrongPunch = false;
    }
    private void Update()
    {
        
        float horizontal = Input.GetAxis(playerName == "Player1" ? "JoyStickX_P1" : "JoyStickX_P2");
        float vertical = Input.GetAxis(playerName == "Player1" ? "JoyStickY_P1" : "JoyStickY_P2");
        
        updatePlayerPostureWJoystick(AngleControl.x, AngleControl.y);
        rotatePlayer(AngleControl.x);
        if (isRegularPunch)
        {
            activePunch = animator.GetBool(isLeftPunch) ? "jab" : "strongPunch";
            animator.SetTrigger(jab);
        }
        if (isStrongPunch)
        {
            activePunch = "hook";
            animator.SetTrigger(hook);
        }
        animator.SetLayerWeight(2, IsGuarding? 1f : 0f);
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
    void updatePlayerPostureWJoystick(float directionX, float directionY)
    {
        animator.SetFloat(postureX, directionX/2);
        animator.SetFloat(postureY, directionY/2);

        if (animator.GetBool(isLeftPunch) && directionX > 0)
        {
            animator.SetBool(isLeftPunch, false);
        }
        else if (!animator.GetBool(isLeftPunch) && directionX < 0)
        {
            animator.SetBool(isLeftPunch, true);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Animator otherAnimator = collision.gameObject.GetComponent<Animator>();
        PlayerAction otherPA = collision.gameObject.GetComponent<PlayerAction>();
        if (collision.gameObject.tag != "Player" 
            || collision.GetContact(0).thisCollider.tag != "Fist" || punchStarted || otherAnimator.GetLayerWeight(3) == 1f)
            return;
        punchStarted = true;
        AudioManager.Instance.PunchSound(activePunch, otherPA.IsGuarding || collision.GetContact(0).otherCollider.tag != "Target");
        if (collision.GetContact(0).otherCollider.tag != "Target" || otherPA.IsGuarding)
            return;
        GameManager.Instance.UpdateHealthPoint(activePunch, playerName);
        if((otherPA.playerName == "Player1" && GameManager.Instance.Player1Health < 0) 
            || (otherPA.playerName == "Player2" && GameManager.Instance.Player2Health < 0))
        {
            AudioManager.Instance.PunchSound("boom");
            otherAnimator.SetLayerWeight(4, 1f);
            otherAnimator.Play("KO", 4, 0f);
        }
        else
        {
            otherAnimator.SetLayerWeight(3, 1f);
            otherAnimator.Play("Body_Hit", 3, 0f);
        }
    }
    private void PunchAnimationEnded()
    {
        punchStarted = false;
    }
    private void HitAnimationFinished()
    {
        gameObject.GetComponent<Animator>().SetLayerWeight(3, 0f);
    }

}
