using UnityEngine;
public class FPSCamera : MonoBehaviour
{

    public Transform Target;
    public float MouseSensitivity = 10f;
    private float verticalRotation;
    private float horizontalRotation;

    void Update()
    {
        // transform.position = Target.position;
        
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        verticalRotation -= mouseY * MouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -60f, 60f);

        horizontalRotation = Target.rotation.eulerAngles.y +  mouseX * MouseSensitivity;

        transform.rotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0);
        Target.rotation = Quaternion.Euler(0, horizontalRotation, 0);
    }
}