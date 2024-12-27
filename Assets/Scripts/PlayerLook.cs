using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private string mouseXInputName = "Mouse X";
    [SerializeField] private string mouseYInputName = "Mouse Y";
    [SerializeField] private float mouseSensitivity = 150f;

    [SerializeField] private Transform playerBody;
    private float xAxisClamp;// Variable to track vertical rotation
    private float yAxisClamp; // Variable to track horizontal rotation
    private bool m_cursorIsLocked = true;

    private void Awake()
    {
        LockCursor();
        // inicializar valores
        xAxisClamp = 0.0f;
        yAxisClamp = 0.0f;
    }

    private void LockCursor()
    {
       
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            m_cursorIsLocked = false;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            m_cursorIsLocked = true;
        }

        if (m_cursorIsLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if (!m_cursorIsLocked)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        
    }

    private void Update()
    {
        CameraRotation();
    }

    private void CameraRotation()
    {
        float mouseX = Input.GetAxis(mouseXInputName) * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis(mouseYInputName) * mouseSensitivity * Time.deltaTime;

        xAxisClamp += mouseY;

        if (xAxisClamp > 65.0f)
        {
            xAxisClamp = 65.0f;
            mouseY = 0.0f;
            ClampXAxisRotationToValue(65.0f);
        }
        else if (xAxisClamp < -65.0f)
        {
            xAxisClamp = -65.0f;
            mouseY = 0.0f;
            ClampXAxisRotationToValue(65.0f);
        }

        // Horizontal rotation clamping
        yAxisClamp += -mouseX;
        float maxHorizontalAngle = 90.0f; // Adjust this value to set the horizontal clamp range
        yAxisClamp = Mathf.Clamp(yAxisClamp, -maxHorizontalAngle, maxHorizontalAngle);

        // Apply rotations
        transform.localRotation = Quaternion.Euler(-xAxisClamp, -yAxisClamp, 0.0f);
        playerBody.localRotation = Quaternion.Euler(0.0f, -yAxisClamp, 0.0f);

        transform.position = new Vector3(playerBody.position.x, playerBody.position.y + 1.8f, playerBody.position.z + 0.3f);
        //transform.Rotate(Vector3.left * mouseY);
        //playerBody.Rotate(Vector3.up * (-mouseX));
    }

    private void ClampXAxisRotationToValue(float value)
    {
        Vector3 eulerRotation = transform.eulerAngles;
        eulerRotation.x = value;
        transform.eulerAngles = eulerRotation;
    }
    

}
