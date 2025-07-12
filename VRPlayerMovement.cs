using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class VRPlayerMovement : MonoBehaviour
{
    public float moveSpeed = 3.0f;
    public InputActionProperty moveInput;  // Joystick input
    private CharacterController characterController;
    private Vector3 moveDirection;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        Vector2 input = moveInput.action.ReadValue<Vector2>();  // Read joystick movement

        moveDirection = new Vector3(input.x, 0, input.y);  // Convert input to movement
        moveDirection *= moveSpeed * Time.deltaTime;

        characterController.Move(transform.TransformDirection(moveDirection)); // Moves with collision
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log("Collided with: " + hit.gameObject.name);
    }
}