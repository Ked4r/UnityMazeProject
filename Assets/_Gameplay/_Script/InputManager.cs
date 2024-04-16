using UnityEngine;

public class InputManager : MonoBehaviour
{
    public InputAsset inputAsset;


    private void Awake()
    {
        inputAsset = new InputAsset();

    }

    private void OnEnable()
    {
        inputAsset.Enable();
    }


    private void OnDisable()
    {

        inputAsset.Disable();
    }
}
