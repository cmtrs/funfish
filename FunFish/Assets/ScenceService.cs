using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenceService : MonoBehaviour
{
    public void Start()
    {
        MyGlobal.playMusic();
    }

    public void PlayScreen()
    {
        // SceneManager.LoadScene("Game");
        MyGlobal.playClick();
        MyGlobal.gotoPlayScreen();
    }

    public void HomeScreen()
    {
        MyGlobal.playClick();
        MyGlobal.gotoHome();
        //SceneManager.LoadScene("MainMenu");
    }

    public void CompletedScreen()
    {
        MyGlobal.playClick();
        MyGlobal.gotoCompleted();
        //SceneManager.LoadScene("Completed");
    }

    public void FailedScreen()
    {
        MyGlobal.playClick();
        MyGlobal.gotoFailedScreen();
        //SceneManager.LoadScene("Failed");
    }

}
