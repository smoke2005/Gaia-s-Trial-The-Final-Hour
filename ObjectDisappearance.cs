using UnityEngine;

public class ObjectDisappearance: MonoBehaviour
{
    public GameObject[] objectsToDisappear; // Array to store the 4 objects
    public GameObject extraObjectToDisappear; // The extra object that disappears when all 4 objects are gone

    private int objectCounter = 0; // Counter to track how many objects have disappeared

    public KeyCode[] keyCodes; // Array of key codes corresponding to each object

    void Update()
    {
        // Loop through the keyCodes and check if the corresponding key is pressed
        for (int i = 0; i < keyCodes.Length; i++)
        {
            if (Input.GetKeyDown(keyCodes[i]) && objectCounter < objectsToDisappear.Length)
            {
                // Disable the object assigned to the key
                objectsToDisappear[i].SetActive(false);
                Debug.Log(objectsToDisappear[i].name + " has disappeared!");

                // Increment the object counter
                objectCounter++;

                // Check if all 4 objects have been removed
                if (objectCounter == 4)
                {
                    // Disable the extra object when all objects are gone
                    if (extraObjectToDisappear != null)
                    {
                        extraObjectToDisappear.SetActive(false);
                        Debug.Log(extraObjectToDisappear.name + " has disappeared after all objects are removed!");
                    }
                }
            }
        }
    }
}