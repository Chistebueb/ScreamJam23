using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public GameObject player;

    public void RestartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void KillPlayer()
    {
        player.SetActive(false);
    }
}
