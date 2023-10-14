using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenceService : MonoBehaviour
{
    public void PlayScreen()
    {
        SceneManager.LoadScene("Game");
    }

    public void HomeScreen()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
