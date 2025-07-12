using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Collider))]
public class IntroMove: MonoBehaviour
{
    public Transform playerObject;
    public AudioSource audioSource;

    [Header("Target Positions")]
    public float dropY = -1.545f;
    public float backZ = -79.565f;

    public float moveSpeed = 10000000000000f;

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

        StartCoroutine(DropAndSlideRoutine());
    }

    System.Collections.IEnumerator DropAndSlideRoutine()
    {
        // Drop in Y
        Vector3 dropTarget = new Vector3(playerObject.position.x, dropY, playerObject.position.z);
        while (Vector3.Distance(playerObject.position, dropTarget) > 0.01f)
        {
            playerObject.position = Vector3.MoveTowards(playerObject.position, dropTarget, moveSpeed * Time.deltaTime);
            yield return null;
        }

        // Slide back in Z
        Vector3 slideTarget = new Vector3(playerObject.position.x, playerObject.position.y, backZ);
        while (Vector3.Distance(playerObject.position, slideTarget) > 0.01f)
        {
            playerObject.position = Vector3.MoveTowards(playerObject.position, slideTarget, moveSpeed * Time.deltaTime);
            yield return null;
        }

        Debug.Log("Slide & Drop complete!");
    }
}