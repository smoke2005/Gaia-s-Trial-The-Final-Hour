using UnityEngine;

public class PlatformSwitcher : MonoBehaviour
{
    public GameObject xrOrigin;
    public GameObject webPlayer;

    void Start()
    {
#if UNITY_WEBGL
        xrOrigin.SetActive(false);
        webPlayer.SetActive(true);
#elif UNITY_ANDROID || UNITY_STANDALONE
        xrOrigin.SetActive(true);
        webPlayer.SetActive(false);
#endif
    }
void StartErrorDebug()
{
    Debug.Log("Game started in: " + Application.platform);
    Debug.Log("Active Camera: " + Camera.main.name);
}
}