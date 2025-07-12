using UnityEngine;

public class ObjectCollision : MonoBehaviour
{
    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        if (characterController == null)
        {
            Debug.LogError("No CharacterController found on XR Rig. Please add one.");
        }
    }

    void Update()
    {
        // Ensures the XR Rig doesn't pass through objects
        PreventPenetration();
    }

    void PreventPenetration()
    {
        Collider[] colliders = Physics.OverlapCapsule(transform.position, transform.position + Vector3.up * characterController.height, characterController.radius);

        foreach (Collider col in colliders)
        {
            if (col.CompareTag("coltag"))
            {
                Debug.Log("Blocked by: " + col.gameObject.name);
                // Adjusts movement to stop penetration (optional)
            }
        }
    }
}