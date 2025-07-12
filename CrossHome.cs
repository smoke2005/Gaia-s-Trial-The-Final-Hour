using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Collider))]
public class CrossHome: MonoBehaviour
{
    public Transform teleportTarget;       // Where to teleport the player
    public GameObject playerObject;        // Who to teleport (e.g., XR Rig or player capsule)

    [Header("Optional Feedback")]
    public AudioClip clickSound;
    public AudioSource audioSource;
    public float delayBeforeTeleport = 1f;

    private bool isVR;

    void Start()
    {
        isVR = UnityEngine.XR.XRSettings.isDeviceActive;
        Debug.Log("Platform: " + (isVR ? "VR" : "WebGL/Desktop"));

        if (isVR)
        {
            if (GetComponent<XRBaseInteractable>() == null)
            {
                gameObject.AddComponent<XRSimpleInteractable>();
                Debug.Log("XR Interactable auto-added to: " + gameObject.name);
            }

            XRBaseInteractable xrInteractable = GetComponent<XRBaseInteractable>();
            xrInteractable.selectEntered.AddListener(OnSelectEnteredXR);
        }
    }

    void OnDestroy()
    {
        if (isVR && GetComponent<XRBaseInteractable>() != null)
        {
            XRBaseInteractable xrInteractable = GetComponent<XRBaseInteractable>();
            xrInteractable.selectEntered.RemoveListener(OnSelectEnteredXR);
        }
    }

    void Update()
    {
        if (!isVR && Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log("WebGL: 'H' key pressed to teleport.");
            HandleTeleport();
        }
    }

    private void OnSelectEnteredXR(SelectEnterEventArgs args)
    {
        Debug.Log("VR Interact on: " + gameObject.name);
        HandleTeleport();
    }

    void HandleTeleport()
    {
        if (audioSource != null && clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }

        StartCoroutine(TeleportAfterDelay());
    }

    System.Collections.IEnumerator TeleportAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeTeleport);

        if (playerObject != null && teleportTarget != null)
        {
            playerObject.transform.position = teleportTarget.position;
            playerObject.transform.rotation = teleportTarget.rotation;
            Debug.Log("Teleportation complete.");
        }
        else
        {
            Debug.LogWarning("Teleport target or player object not assigned.");
        }
    }
}