using UnityEngine;
using UnityEngine.InputSystem;

public class CapsuleMove : MonoBehaviour
{
    public InputManager inputManager;
    public StaminaBarScript staminaBarScript;
    public Rigidbody rb;
    public Light flashlight;

    public float moveSpeed = 10f;
    public float runSpeed = 15f;
    public float jumpForce = 15f;
    public float kickForce = 20f;
    public float staminaVal = 0.1f;
    public LayerMask groundLayer;

    public float groundCheckDistance = 0.7f;
    public float smoothTime = 0.1f;

    private Vector3 moveDirection = Vector3.zero;
    private Vector3 currentVelocity;
    private bool isGrounded;

    private void Start()
    {
        inputManager.inputAsset.Player.Jump.started += _ => Jump();
        inputManager.inputAsset.Player.Kick.started += _ => Kick();
        inputManager.inputAsset.Player.Flashlight.started += _ => ToggleFlashlight();
        inputManager.inputAsset.Player.Exit.performed += _ => ExitGame();  // Bind Exit action
        staminaBarScript.SetMaxStamina(GetComponent<PlayerStats>().maxStamina);
    }

    private void FixedUpdate()
    {
        float forward = inputManager.inputAsset.Player.Forward.ReadValue<float>();
        float right = inputManager.inputAsset.Player.Right.ReadValue<float>();
        moveDirection = (transform.right * right + transform.forward * forward).normalized;
        Move();
        CheckGroundStatus();
    }

    void Move()
    {
        bool isSprinting = inputManager.inputAsset.Player.Sprint.ReadValue<float>() > 0;
        float currentSpeed = isSprinting ? runSpeed : moveSpeed;
        Vector3 targetVelocity = moveDirection * currentSpeed;
        targetVelocity.y = rb.velocity.y;

        if (isSprinting)
        {
            GetComponent<PlayerStats>().UseStamina(staminaVal);
        }
        else
        {
            GetComponent<PlayerStats>().RegainStamina(staminaVal);
        }
        staminaBarScript.setStamina(GetComponent<PlayerStats>().currentStamina);

        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref currentVelocity, smoothTime);
    }

    private void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void Kick()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 5f) && hit.collider.CompareTag("Ball"))
        {
            if (hit.collider.TryGetComponent<Rigidbody>(out Rigidbody hitRb))
            {
                hitRb.AddForce(transform.forward * kickForce, ForceMode.Impulse);
            }
        }
    }

    private void CheckGroundStatus()
    {
        isGrounded = Physics.Raycast(transform.position, -Vector3.up, groundCheckDistance, groundLayer);
    }

    private void ToggleFlashlight()
    {
        flashlight.enabled = !flashlight.enabled;
    }

    private void ExitGame()
    {
        Application.Quit();  // Quits the application
    }
}
