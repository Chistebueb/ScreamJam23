using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMovement : MonoBehaviour
{
    [SerializeField] private Vector3 spawnPosition;
    [SerializeField] private List<GameObject> gameObjectsToClone;
    [SerializeField] private float spaceBetween = 10f;
    [SerializeField] private GameObject warning;

    private Vector3 initialPosition;
    private Vector3 offset;
    private GameObject lastSpawnedObject;
    private int numOfLightBefore;

    //Important! 9 is the number of lights in the first prebuilt room, and might be changed
    int[] difference = { 9, 0};
    private bool nextRoom = false;

    private void Start()
    {
        offset = new Vector3(0, 0, spaceBetween);
        initialPosition = transform.position;
    }

    private void Update()
    {
        if (nextRoom)
        {
            nextRoom = false;
            difference[1] = difference[0];
            difference[0] = Deactivate.numOfLights - numOfLightBefore;
            Debug.Log(difference[0] + difference[1]);
            Debug.Log(difference[0] + " + " + difference[1]);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            numOfLightBefore = Deactivate.numOfLights;
            if (Deactivate.numOfLights > difference[0] + difference[1] && difference[1] != 0)
            {
                warning.SetActive(true);
            }

            transform.position += offset;

            // Clone a random object from the list
            int randomIndex = Random.Range(0, gameObjectsToClone.Count);
            GameObject selectedObject = gameObjectsToClone[randomIndex];
            selectedObject.SetActive(true);

            // Set the spawn position for the first object
            if (selectedObject.name.StartsWith("r", System.StringComparison.OrdinalIgnoreCase))
            {
                // Remove the cloned object from the list
                gameObjectsToClone.RemoveAt(randomIndex);
            }
            // Set the spawn position for the first object
            if (lastSpawnedObject == null)
            {
                lastSpawnedObject = Instantiate(selectedObject, spawnPosition, Quaternion.identity);
            }
            else
            {
                Vector3 newSpawnPosition = lastSpawnedObject.transform.position + offset;
                lastSpawnedObject = Instantiate(selectedObject, newSpawnPosition, Quaternion.identity);
            }
            nextRoom = true;
            selectedObject.SetActive(false);
        }
    }
}