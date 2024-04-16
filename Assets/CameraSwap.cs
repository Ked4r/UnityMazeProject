using UnityEngine;
using UnityEngine.InputSystem;

public class CameraSwitcher : MonoBehaviour
{
    public Camera primaryCamera;
    public Camera secondaryCamera;
    public Camera thirdCamera;
    public InputManager inputManager;

    private void Start()
    {
        // Upewnij siê, ¿e na pocz¹tku gry tylko jedna kamera jest aktywna
        primaryCamera.gameObject.SetActive(true);
        secondaryCamera.gameObject.SetActive(false);
        thirdCamera.gameObject.SetActive(false);

        // Subskrybuj akcjê prze³¹czania kamery do konkretnych klawiszy
        inputManager.inputAsset.CameraLook.CameraOne.performed += _ => ActivateCamera(1);
        inputManager.inputAsset.CameraLook.CameraTwo.performed += _ => ActivateCamera(2);
        inputManager.inputAsset.CameraLook.CameraThree.performed += _ => ActivateCamera(3);
    }

    private void ActivateCamera(int cameraNumber)
    {
        // Deaktywuj wszystkie kamery
        primaryCamera.gameObject.SetActive(false);
        secondaryCamera.gameObject.SetActive(false);
        thirdCamera.gameObject.SetActive(false);

        // Aktywuj wybran¹ kamerê
        switch (cameraNumber)
        {
            case 1:
                primaryCamera.gameObject.SetActive(true);
                break;
            case 2:
                secondaryCamera.gameObject.SetActive(true);
                break;
            case 3:
                thirdCamera.gameObject.SetActive(true);
                break;
        }
    }
}
