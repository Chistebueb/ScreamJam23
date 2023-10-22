using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateObjectsOnTrigger : MonoBehaviour
{
    [SerializeField] private List<GameObject> gameObjectsToRemove;
    [SerializeField] private List<GameObject> gameObjectsToActivate;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            foreach (GameObject go in gameObjectsToRemove)
            {
                go.SetActive(false);
            }
            foreach (GameObject go in gameObjectsToActivate)
            {
                go.SetActive(true);
            }
        }
    }
}
