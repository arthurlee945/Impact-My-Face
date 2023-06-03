using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTracking : MonoBehaviour
{
    [SerializeField]
    private GameObject playerChar;
    [SerializeField]
    private float offsetY;

    private Vector3 offset;
    void Awake()
    {
         offset = this.transform.position - playerChar.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.position = playerChar.transform.position + offset;
    }
}
