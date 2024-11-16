using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firstPersonCamera : MonoBehaviour
{
    public float mouseSensitivity = 2.0f;
    private float xRotation = 0f;
    private float yRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        // Lock and hide the cursor when the game starts
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Handle mouse look for camera rotation
        HandleMouseLook();
    }

    void HandleMouseLook()
    {
        // Get mouse input for camera movement
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Apply the horizontal rotation to the player's body (left-right)
        transform.Rotate(Vector3.up * mouseX);

        // Apply the vertical rotation to the camera (up-down)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);  // Prevent the camera from rotating too far up or down
        Camera.main.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        Camera.main.transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}
