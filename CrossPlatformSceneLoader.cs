using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class CrossPlatformSceneLoader: MonoBehaviour
{
    public string sceneToLoad;

    [Header("Optional Feedback")]
    public AudioClip clickSound;
    public AudioSource audioSource;
    public float delayBeforeSceneLoad = 1f;

    private bool isVR;

    void Start()
    {
        isVR = UnityEngine.XR.XRSettings.isDeviceActive;
        Debug.Log("Platform: " + (isVR ? "VR" : "WebGL/Desktop"));

        if (isVR)
        {
            // Add XR Interactable if missing
            if (GetComponent<XRBaseInteractable>() == null)
            {
                gameObject.AddComponent<XRSimpleInteractable>();
                Debug.Log("XR Interactable auto-added to: " + gameObject.name);
            }

            var interactable = GetComponent<XRBaseInteractable>();
            interactable.selectEntered.AddListener(OnSelectEnteredXR);
        }
    }

    void OnDestroy()
    {
        if (isVR && GetComponent<XRBaseInteractable>() != null)
        {
            var interactable = GetComponent<XRBaseInteractable>();
            interactable.selectEntered.RemoveListener(OnSelectEnteredXR);
        }
    }

    void Update()
    {
        if (!isVR && Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("WebGL: 'F' key pressed to load scene: " + sceneToLoad);
            HandleSceneLoad();
        }
    }

    private void OnSelectEnteredXR(SelectEnterEventArgs args)
    {
        Debug.Log("VR Interact on: " + gameObject.name);
        HandleSceneLoad();
    }

    void HandleSceneLoad()
    {
        if (audioSource != null && clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }

        StartCoroutine(LoadSceneAfterDelay());
    }

    IEnumerator LoadSceneAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeSceneLoad);
        SceneManager.LoadScene(sceneToLoad);
    }
}