using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracking : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    private Vector3 offset;
    void Awake()
    {
         offset = this.transform.position - player.position;
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        RotateCameraRelativeToPlayer();
    }
    void RotateCameraRelativeToPlayer()
    {
        float targetYAngle = player.eulerAngles.y;
        transform.position = player.position + (Quaternion.Euler(0, targetYAngle + 1, 0) * offset);
        transform.LookAt(player.transform.position + new Vector3(0, 1f, 0));
    }
}
