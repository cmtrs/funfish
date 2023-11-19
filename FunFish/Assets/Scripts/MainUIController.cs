using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class MainUIController : MonoBehaviour
{

    public GameObject _panelSetting; // Assign in inspector or find it via script

    void Start()
    {
        // dipslayMusicSoundStatus();

        // SystemInfo.deviceUniqueIdentifier;

        if (MyGlobal.getIsFirstLoad())
        {
            SetButtonInteractable("btnPlay", false);
            var url = MyGlobal.getFunFishUrl();
            StartCoroutine(MyGlobal.getDataCoroutine(url, UpdateContent));
        }
        else
        {
            dipslayMusicSoundStatus();
            //  StartCoroutine(MyGlobal.getDataCoroutine(MyGlobal.getFunFishSaveSettingUrl(), (data) => { }));
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetButtonInteractable(string name, bool isInteractable)
    {
        // Find the button GameObject by name
        GameObject buttonObj = GameObject.Find(name);
        if (buttonObj != null)
        {
            // Get the Button component
            Button button = buttonObj.GetComponent<Button>();
            if (button != null)
            {
                // Set the interactable state of the button
                button.interactable = isInteractable;

                // Get the TextMeshProUGUI component, assuming it's a direct child as before
                TextMeshProUGUI textComponent = buttonObj.GetComponentInChildren<TextMeshProUGUI>();
                if (textComponent != null)
                {
                    // Change the color of the text to indicate the button's state
                    textComponent.color = isInteractable ? Color.white : Color.gray; // Change the colors as desired
                }
            }
            else
            {
                Debug.LogError("No Button component found on the GameObject with name " + name);
            }
        }
        else
        {
            Debug.LogError("No GameObject with name " + name + " found in the scene.");
        }
    }

    void UpdateContent(string data)
    {
        var info = MyGlobal.splitString(data, ",");
        MyGlobal.setCurrentScore(Convert.ToInt32(info[0]));
        MyGlobal.setSoundOn(Convert.ToBoolean(info[1]));
        MyGlobal.setMusicOn(Convert.ToBoolean(info[2]));
        MyGlobal.setIsFirstLoadFalse();
        dipslayMusicSoundStatus();
        SetButtonInteractable("btnPlay", true);
    }
    void updateSetting()
    {

    }


    private void QuitGame()
    {
        // Destroy all objects before quitting
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        foreach (GameObject go in allObjects)
        {
            Destroy(go);
        }

        // If we are running in the editor
#if UNITY_EDITOR
        // Stop playing the scene
        UnityEditor.EditorApplication.isPlaying = false;
#else
    // Quit the application
    Application.Quit();
#endif
    }

    private string getYesNoText(bool on)
    {
        string result = "NO";
        if (on)
        {
            result = "YES";
        }
        return result;
    }

    public void setSoundOn()
    {
        MyGlobal.setSoundOn(!MyGlobal.getSoundOn());
        dipslayMusicSoundStatus();
    }

    public void setMusicOn()
    {
        MyGlobal.setMusicOn(!MyGlobal.getMusicOn());
        dipslayMusicSoundStatus();
    }

    public void playButton()
    {
        MyGlobal.playClick();
        MyGlobal.gotoPlayScreen();
    }

    private void dipslayMusicSoundStatus()
    {
        // GameObject.Find("btnMusic").GetComponentInChildren<TextMeshProUGUI>().text = getYesNoText(MyGlobal.getMusicOn());
        //  GameObject.Find("btnSound").GetComponentInChildren<TextMeshProUGUI>().text = getYesNoText(MyGlobal.getSoundOn());

        _panelSetting.transform.Find("btnMusic").GetComponentInChildren<TextMeshProUGUI>().text = getYesNoText(MyGlobal.getMusicOn());
        _panelSetting.transform.Find("btnSound").GetComponentInChildren<TextMeshProUGUI>().text = getYesNoText(MyGlobal.getSoundOn());

        //MyGlobal.FindObjectIncludingInactive("btnMusic").GetComponentInChildren<TextMeshProUGUI>().text = getYesNoText(MyGlobal.getMusicOn());
        // MyGlobal.FindObjectIncludingInactive("btnSound").GetComponentInChildren<TextMeshProUGUI>().text = getYesNoText(MyGlobal.getSoundOn());
    }

    public void showSettingPanel()
    {
        MyGlobal.playClick();
        _panelSetting.SetActive(true);
    }

    public void hideSettingPanel()
    {
        MyGlobal.playClick();
        _panelSetting.SetActive(false);

        StartCoroutine(MyGlobal.getDataCoroutine(MyGlobal.getFunFishSaveSettingUrl(), (data) => { }));

    }

    public void exitApp()
    {
        MyGlobal.playClick();
        QuitGame();
        //Application.Quit();
    }

}
