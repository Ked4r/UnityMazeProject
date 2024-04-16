using UnityEngine;
using UnityEngine.InputSystem;

public class ChildrenMover : MonoBehaviour
{
    public InputManager inputManager;
    public float moveSpeed = 5f;

    private void FixedUpdate()
    {
        Vector2 movementInput = inputManager.inputAsset.Player.PlayerMovement.ReadValue<Vector2>();
        // Konwersja Vector2 na Vector3
        Vector3 moveDirection = new Vector3(movementInput.x, 0f, movementInput.y).normalized * moveSpeed;

        foreach (Transform child in transform)
        {
            Rigidbody childRb = child.GetComponent<Rigidbody>();
            if (childRb != null)
            {
                // Stosowanie bezpoœredniego przemieszczenia zamiast dodawania si³y
                childRb.MovePosition(childRb.position + moveDirection * Time.fixedDeltaTime);
            }
        }
    }
}
