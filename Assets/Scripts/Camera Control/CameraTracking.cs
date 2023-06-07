using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracking : MonoBehaviour
{
    [SerializeField]
    public Transform player;
    [SerializeField]
    private float AngleY = 1f;
    private Vector3 offset;
    void Awake()
    {
         offset = this.transform.position - player.position;
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        RotateCameraRelativeToPlayer();
    }
    void RotateCameraRelativeToPlayer()
    {
        float targetYAngle = player.eulerAngles.y;
        transform.position = player.position + (Quaternion.Euler(0, targetYAngle + 1, 0) * offset);
        transform.LookAt(player.transform.position + new Vector3(0, AngleY, 0));
    }
}
