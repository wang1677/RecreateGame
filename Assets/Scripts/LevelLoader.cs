using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // Import the SceneManagement namespace

public class LevelLoader : MonoBehaviour
{
    // Method to load a level by name
    public void LoadLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);  // Load the scene with the specified name
    }
}