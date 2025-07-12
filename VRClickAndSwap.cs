using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VRClickAndSwap : MonoBehaviour
{
    public XRRayInteractor rayInteractor; // VR ray interactor
    public GameObject objectToClick;
    public GameObject objectToDisappear;
    public Light lightToDisable;
    public GameObject objectToAppear;

    public Vector3 spawnPosition;
    public Vector3 spawnRotation; // Euler angles

    public AudioClip tingSound;
    public AudioSource audioSource;

    public KeyCode keyToTrigger = KeyCode.Space;  // Key to trigger the functionality, default is Space

    private bool actionTriggered = false;
    private bool isVR;

    void Start()
    {
        isVR = UnityEngine.XR.XRSettings.isDeviceActive;

        // Always show the cursor for WebGL (but still allow camera rotation via mouse drag)
        if (!isVR)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        Debug.Log("VR Mode: " + (isVR ? "Active" : "Not Active"));
    }

    void Update()
    {
        if (actionTriggered) return;

        if (isVR)
        {
            HandleVRInput();
        }
        else
        {
            HandleKeyboardInput();
        }
    }

    // Handle VR input where raycast interaction happens
    void HandleVRInput()
    {
        if (rayInteractor != null && rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            Debug.Log("Raycast hit: " + hit.collider.gameObject.name);  // Log the name of the object being hit

            if (Input.GetButtonDown("Fire1") || Input.GetAxis("TriggerButton") > 0.9f)
            {
                Debug.Log("Button Press Detected - Checking if the correct object is clicked...");
                if (hit.collider.gameObject == objectToClick)
                {
                    Debug.Log("Correct object clicked! Proceeding with swap.");
                    SwapObjects();
                }
                else
                {
                    Debug.Log("Incorrect object clicked: " + hit.collider.gameObject.name);
                }
            }
        }
    }

    // Handle keyboard input for triggering the swap
    void HandleKeyboardInput()
    {
        if (Input.GetKeyDown(keyToTrigger)) // Check if the specified key is pressed
        {
            Debug.Log("Key " + keyToTrigger + " pressed. Triggering object swap...");
            SwapObjects();
        }
    }

    void SwapObjects()
    {
        Debug.Log("Starting swap process...");
        
        if (objectToDisappear != null)
        {
            objectToDisappear.SetActive(false);
            Debug.Log("Object to disappear has been deactivated.");
        }

        if (lightToDisable != null)
        {
            lightToDisable.enabled = false;
            Debug.Log("Light has been disabled.");
        }

        if (objectToAppear != null)
        {
            objectToAppear.transform.position = spawnPosition;
            objectToAppear.transform.rotation = Quaternion.Euler(spawnRotation);
            objectToAppear.SetActive(true);

            Debug.Log("New object spawned at position: " + spawnPosition + " with rotation: " + spawnRotation);

            if (audioSource != null && tingSound != null)
            {
                audioSource.PlayOneShot(tingSound);
                Debug.Log("Playing 'TING' sound.");
            }
        }

        actionTriggered = true;
        Debug.Log("Swap complete with TINGGG at specified coordinates!");
    }
}