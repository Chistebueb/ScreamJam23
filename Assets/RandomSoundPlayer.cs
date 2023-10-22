using System.Collections;
using UnityEngine;

public class RandomSoundPlayer : MonoBehaviour
{
    public AudioClip[] audioClips; // Drag your audio clips here in the inspector
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false;
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
            float waitTime = Random.Range(20, 80);
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
