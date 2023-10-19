using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // This method is used to switch to the game scene
    public void StartGame()
    {
        // Load the scene with the given name
        // Replace "GameScene" with the name of your game scene
        SceneManager.LoadScene("SampleScene");
    }

    // This method is used to exit the game
    public void ExitGame()
    {
        // Print a message to the console to know that the Exit button works
        // This is especially useful for testing in the Unity editor because Application.Quit() won't work in the editor
        Debug.Log("Exit Button Pressed");

        // Close the application
        Application.Quit();
    }
}
