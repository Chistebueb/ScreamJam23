using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMovement : MonoBehaviour
{
    [SerializeField] private Vector3 spawnPosition;
    [SerializeField] private List<GameObject> gameObjectsToClone;
    [SerializeField] private float spaceBetween = 10f;

    private Vector3 initialPosition;
    private Vector3 offset;
    private GameObject lastSpawnedObject;

    private void Start()
    {
        offset = new Vector3(0, 0, spaceBetween);
        initialPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            transform.position += offset;

            // Clone a random object from the list
            int randomIndex = Random.Range(0, gameObjectsToClone.Count);
            GameObject selectedObject = gameObjectsToClone[randomIndex];

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

    
        }
    }
}