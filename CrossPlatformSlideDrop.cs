using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Collider))]
public class CrossPlatformSlideDrop: MonoBehaviour
{
    public Transform playerObject;
    public AudioSource audioSource;

    [Header("Target Z Position")]
    public float targetZ = -73.818f;

    private bool isVR;
    private bool hasStarted = false;

    void Start()
    {
        isVR = UnityEngine.XR.XRSettings.isDeviceActive;
        Debug.Log("Running on: " + (isVR ? "VR" : "WebGL/Desktop"));

        if (isVR)
        {
            if (GetComponent<XRBaseInteractable>() == null)
            {
                gameObject.AddComponent<XRSimpleInteractable>();
                Debug.Log("Added XR Interactable to: " + gameObject.name);
            }

            XRBaseInteractable interactable = GetComponent<XRBaseInteractable>();
            interactable.selectEntered.AddListener(OnVRSelect);
        }
    }

    void OnDestroy()
    {
        if (isVR && GetComponent<XRBaseInteractable>() != null)
        {
            XRBaseInteractable interactable = GetComponent<XRBaseInteractable>();
            interactable.selectEntered.RemoveListener(OnVRSelect);
        }
    }

    void Update()
    {
        if (!isVR && Input.GetKeyDown(KeyCode.O) && !hasStarted)
        {
            Debug.Log("WebGL: 'O' key pressed.");
            StartMovement();
        }
    }

    private void OnVRSelect(SelectEnterEventArgs args)
    {
        if (!hasStarted)
        {
            Debug.Log("VR: Interacted with object.");
            StartMovement();
        }
    }

    void StartMovement()
    {
        hasStarted = true;

        if (audioSource != null && !audioSource.isPlaying)
            audioSource.Play();

        // Just a frame delay to allow audio to play, then instantly move
        StartCoroutine(InstantZSlide());
    }

    System.Collections.IEnumerator InstantZSlide()
    {
        yield return null; // wait 1 frame

        Vector3 pos = playerObject.position;
        playerObject.position = new Vector3(pos.x, pos.y, targetZ);

        Debug.Log("Teleported to Z = " + targetZ);
    }
}