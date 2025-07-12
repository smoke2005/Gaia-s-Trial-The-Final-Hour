using UnityEngine;
using UnityEngine.XR;
using System.Collections.Generic;

public class ChoiceSequenceManager : MonoBehaviour
{
    public GameObject[] setA; // A option visuals
    public GameObject[] setB; // B option visuals
    public GameObject[] setC; // Question visuals

    public GameObject successObject;
    public GameObject failureObject;

    [Tooltip("Optional: Where the success/failure objects should appear")]
    public Transform spawnPoint;

    [Tooltip("Expected answer sequence: only 'A' or 'B', length = 2")]
    public List<string> expectedSequence = new List<string>();  // Example: ["A", "B"]

    private List<string> userSequence = new List<string>();
    private int currentIndex = 0;
    private bool isVR;

    void Start()
    {
        isVR = XRSettings.isDeviceActive;

        if (expectedSequence.Count != 2)
        {
            Debug.LogError("ExpectedSequence must contain exactly 2 values: 'A' or 'B'.");
        }

        // Make sure result objects are hidden at start
        successObject.SetActive(false);
        failureObject.SetActive(false);
    }

    void Update()
    {
        if (currentIndex < 2)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                HandleKeyChoice("A");
            }
            else if (Input.GetKeyDown(KeyCode.N))
            {
                HandleKeyChoice("B");
            }
        }
    }

    void HandleKeyChoice(string choice)
    {
        Debug.Log($"User pressed key for: {choice}");

        userSequence.Add(choice);

        // Hide current question and options
        if (setA.Length > currentIndex && setA[currentIndex] != null)
            setA[currentIndex].SetActive(false);
        if (setB.Length > currentIndex && setB[currentIndex] != null)
            setB[currentIndex].SetActive(false);
        if (setC.Length > currentIndex && setC[currentIndex] != null)
            setC[currentIndex].SetActive(false);

        currentIndex++;

        if (currentIndex == 2)
        {
            EvaluateSequence();
        }
    }

    void EvaluateSequence()
    {
        bool match = userSequence.Count == 2 &&
                     userSequence[0] == expectedSequence[0] &&
                     userSequence[1] == expectedSequence[1];

        if (match)
        {
            if (spawnPoint != null)
                successObject.transform.position = spawnPoint.position;

            successObject.SetActive(true);
            Debug.Log("✅ Sequence correct!");
        }
        else
        {
            if (spawnPoint != null)
                failureObject.transform.position = spawnPoint.position;

            failureObject.SetActive(true);
            Debug.Log("❌ Sequence incorrect.");
        }
    }
}