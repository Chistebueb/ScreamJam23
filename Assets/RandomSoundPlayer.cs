using System.Collections;
using UnityEngine;

public class RandomSoundPlayer : MonoBehaviour
{
    public AudioClip[] audioClips; // Drag your audio clips here in the inspector
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("No AudioSource component found on this GameObject.");
            return;
        }

        if (audioClips == null || audioClips.Length == 0)
        {
            Debug.LogError("No audio clips provided.");
            return;
        }

        StartCoroutine(PlayRandomSound());
    }

    private IEnumerator PlayRandomSound()
    {
        while (true)
        {
            // Generate a random time between 30 seconds and 3 minutes
            float waitTime = Random.Range(30, 181);
            yield return new WaitForSeconds(waitTime);

            // Pick a random sound from the array
            int randomIndex = Random.Range(0, audioClips.Length);
            AudioClip clip = audioClips[randomIndex];

            // Play the random sound
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}
