using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{

    public InputManager inputManager;
    public float mouseSensitivity = 90f;
    public Transform Body;

    private float xRotation = 0;
    private float yRotation = 0;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = inputManager.inputAsset.CameraLook.MouseX.ReadValue<float>() 
            * mouseSensitivity * Time.deltaTime;
        float mouseY = inputManager.inputAsset.CameraLook.MouseY.ReadValue<float>()
            * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
        Body.Rotate(Vector3.up * mouseX);

    }
}
