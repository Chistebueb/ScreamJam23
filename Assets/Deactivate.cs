using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deactivate : MonoBehaviour
{

    [SerializeField] private List<GameObject> gameObjectsToRemove;
    [SerializeField] private List<GameObject> gameObjectsToActivate;
    [SerializeField] private GameObject indicator;
    private bool isTriggered = false;

    void Update()
    {
        if(isTriggered && Input.GetKeyDown(KeyCode.E))
        {
            indicator.SetActive(false);
            isTriggered = false;

            Collider m_Collider = GetComponent<Collider>();
            m_Collider.enabled = !m_Collider.enabled;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") 
        {
            isTriggered = true;
            indicator.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isTriggered = false;
            indicator.SetActive(false);
        }
    }
}
