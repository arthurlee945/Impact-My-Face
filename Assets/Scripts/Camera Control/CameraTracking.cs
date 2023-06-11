using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracking : MonoBehaviour
{

    [SerializeField] private Transform playerContainer;
    [SerializeField]
    private Vector3 currentOffsetVector;
    [SerializeField]
    private float AngleY = 1f;

    private Transform player;
    private Vector3 offset;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        StartCoroutine(AssignPlayer());
    }

    void LateUpdate()
    {
        if (player == null)
            return;
        RotateCameraRelativeToPlayer();
    }
    private IEnumerator AssignPlayer()
    {
        if(playerContainer.childCount == 0)
        {
            yield return null;
        }
        player = playerContainer.GetChild(0);
        offset = currentOffsetVector - player.position;
    }
    void RotateCameraRelativeToPlayer()
    {
        if (player == null)
            return;
        float targetYAngle = player.eulerAngles.y;
        transform.position = player.position + (Quaternion.Euler(0, targetYAngle + 1, 0) * offset);
        transform.LookAt(player.transform.position + new Vector3(0, AngleY, 0));
    }
}
