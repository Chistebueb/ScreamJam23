using System.Collections;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    public float delay = 0.5f;
    private bool isSeen = false;
    private AudioSource audioSource;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (!audioSource)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void OnBecameVisible()
    {
        if (!isSeen) // Ensure this is only triggered once
        {
            isSeen = true;
            StartCoroutine(DestroySelfAfterDelay(delay));
        }
    }

    IEnumerator DestroySelfAfterDelay(float delay)
    {
        PlaySound();
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    private void PlaySound()
    {
        audioSource.pitch = Random.Range(0.95f, 1.05f);
        audioSource.Play();
    }
}
