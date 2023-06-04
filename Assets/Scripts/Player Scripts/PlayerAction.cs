using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    [SerializeField]
    [Range(0f, 10f)]
    private float rotationSpeed = 2.5f;

    private Animator animator;
    private void Awake()
    {
        
    }
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Mouse X");
        float vertical = Input.GetAxis("Mouse Y");
        rotatePlayer(horizontal);
    }
    void rotatePlayer(float horizontalRotation)
    {
        transform.Rotate(0, horizontalRotation * rotationSpeed, 0);
    }

}
