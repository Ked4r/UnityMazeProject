using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Dziedziczenie po PlayerStats
public class PlayerMovement : PlayerStats
{
    private InputAsset input = null;
    private Vector2 movementInput = Vector2.zero;
    bool movementPressed;
    bool runPressed;
    bool jumpPressed;
    private Rigidbody rb = null;
    private float moveSpeed = 2f;
    private float rotationSpeed = 45f; // Degrees per second

    Animator animator;
    int isWalkingHash;
    int isRunningHash;
    int isJumpingHash;



    private void Awake()
    {
        
        input = new InputAsset();
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true; // Ensure the Rigidbody is kinematic
        animator = GetComponent<Animator>();


        input.Player.PlayerMovement.performed += ctx =>
        {
            movementInput = ctx.ReadValue<Vector2>();
            movementPressed = movementInput.x != 0 || movementInput.y != 0;
        };
        input.Player.PlayerMovement.canceled += ctx =>
        {
            movementInput = Vector2.zero;
            movementPressed = false;
        };
        input.Player.Sprint.performed += ctx => runPressed = ctx.ReadValueAsButton();
        input.Player.Sprint.canceled += ctx => runPressed = false;
        input.Player.Jump.performed += ctx => jumpPressed = ctx.ReadValueAsButton();
        input.Player.Jump.canceled += ctx => jumpPressed = false;

        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        isJumpingHash = Animator.StringToHash("isJumping");
    }

    private void handleMovement()
    {
        if (movementPressed)
        {
            Vector3 movement = new Vector3(movementInput.x, 0, movementInput.y);
            Vector3 moveVelocity = movement.normalized * moveSpeed * Time.deltaTime;
            rb.MovePosition(rb.position + moveVelocity);
        }

        animator.SetBool(isWalkingHash, movementPressed);
        animator.SetBool(isRunningHash, movementPressed && runPressed);
        // Assuming jumping is handled elsewhere or just an animation trigger
        animator.SetBool(isJumpingHash, jumpPressed);
    }

    private void handleRotation()
    {
        if (movementPressed)
        {
            Vector3 direction = new Vector3(movementInput.x, 0, movementInput.y);
            if (direction.magnitude > 0.1f) // Check to ensure we have a valid direction
            {
                Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
                rb.rotation = Quaternion.RotateTowards(rb.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }

    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }

    private void FixedUpdate()
    {
        handleMovement();
        handleRotation();
    }
}
