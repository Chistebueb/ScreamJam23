using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerOn : MonoBehaviour
{
    [SerializeField] private List<GameObject> gameObjectsToRemove;
    [SerializeField] private List<GameObject> gameObjectsToActivate;
    [SerializeField] private GameObject indicator;
    [SerializeField] private static GameObject indicatorObject;
    public static int numOfLights = 0;
    private bool isTriggered = false;
    private AudioSource audioSource;

    void Start()
    {
        numOfLights++;
        if (indicatorObject == null && indicator != null)
        {
            indicatorObject = indicator;
        }
        else if (indicator == null && indicatorObject != null)
        {
            indicator = indicatorObject;
        }

        audioSource = GetComponent<AudioSource>();
        if (!audioSource)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        if (isTriggered && Input.GetKeyDown(KeyCode.E))
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
            numOfLights--;
            PlaySound();
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

    private void PlaySound()
    {
        audioSource.pitch = Random.Range(0.95f, 1.05f);
        audioSource.Play();
    }
}
