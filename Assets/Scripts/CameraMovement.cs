using UnityEngine;
using Cinemachine;

public class CameraMovement : MonoBehaviour
{

    [SerializeField] private CinemachineVirtualCamera vcam;

    private float panSpeed = 10f;
    //private float panBorderThickness = 20f;

    [SerializeField] private Vector2 panLimit;

    private void LateUpdate()
    {
        Vector3 pos = vcam.transform.position;

        if (Input.GetKey(KeyCode.W)) //|| Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            pos.y += panSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S)) //|| Input.mousePosition.y <= panBorderThickness)
        {
            pos.y -= panSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A)) //|| Input.mousePosition.x <= panBorderThickness)
        {
            pos.x -= panSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D)) //|| Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            pos.x += panSpeed * Time.deltaTime;
        }

        pos.x = Mathf.Clamp(pos.x, -panLimit.x, panLimit.x);
        pos.y = Mathf.Clamp(pos.y, -panLimit.y, panLimit.y);

        transform.position = pos;
    }
}
