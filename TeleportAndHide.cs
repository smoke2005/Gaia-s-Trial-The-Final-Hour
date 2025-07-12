using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class TeleportAndHide : MonoBehaviour
{
    public Transform teleportTarget; // The location to move the player/object to
    public GameObject playerObject;  // Object to move (e.g., XR Rig or player capsule)
    public AudioClip teleportSound;
    public AudioSource audioSource;

    private bool isVR;

    void Start()
    {
        isVR = XRSettings.isDeviceActive;
        Debug.Log("Running on: " + (isVR ? "VR" : "WebGL/Desktop"));

        if (isVR)
        {
            if (GetComponent<XRBaseInteractable>() == null)
            {
                var simpleInteractable = gameObject.AddComponent<XRSimpleInteractable>();
                Debug.Log("Added XR Interactable to object: " + gameObject.name);
            }

            XRBaseInteractable xrInteractable = GetComponent<XRBaseInteractable>();
            xrInteractable.selectEntered.AddListener(OnSelectEnteredVR);
        }
    }

    void Update()
    {
        if (!isVR && Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("WebGL: B key pressed");
            HandleTeleportAndHide();
        }
    }

    private void OnSelectEnteredVR(SelectEnterEventArgs args)
    {
        Debug.Log("VR: Object clicked via ray interactor");
        HandleTeleportAndHide();
    }

    void HandleTeleportAndHide()
    {
        // Hide the object
        gameObject.SetActive(false);

        // Move the player to the new location
        if (playerObject != null && teleportTarget != null)
        {
            playerObject.transform.position = teleportTarget.position;
            playerObject.transform.rotation = teleportTarget.rotation;
        }

        // Optional sound effect
        if (audioSource != null && teleportSound != null)
        {
            audioSource.PlayOneShot(teleportSound);
        }

        Debug.Log("Teleportation complete.");
    }

    void OnDestroy()
    {
        if (isVR && GetComponent<XRBaseInteractable>() != null)
        {
            GetComponent<XRBaseInteractable>().selectEntered.RemoveListener(OnSelectEnteredVR);
        }
    }
}