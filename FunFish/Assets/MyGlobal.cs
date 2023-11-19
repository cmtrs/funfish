using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class MyGlobal : MonoBehaviour
{
    private static string _deviceId = "";
    private static int _answer;
    private static int _firstNumber;
    private static int _secondNumber;
    private static int[] _options;
    private static int _score = 0;
    private static bool _soundOn = false;
    private static bool _musicOn = false;
    private static bool _isFirstLoad = true;

    //// Static reference to the MonoBehaviour instance
    //private static MyGlobal instance;

    //// Awake is called when the script instance is being loaded
    //void Awake()
    //{
    //    if (instance == null)
    //    {
    //        instance = this;
    //    }
    //    else
    //    {
    //        Debug.LogError("There is more than one CoroutineStarter instance in the scene.");
    //    }
    //}
    public static bool getIsFirstLoad()
    {
        return _isFirstLoad;
    }

    public static void setIsFirstLoadFalse()
    {
        _isFirstLoad = false;
    }

    public static void setCurrentAnswer(int answer)
    {
        _answer = answer;
    }

    public static int getCurrentAnswer()
    {
        return _answer;
    }

    public static int getCurrentScore()
    {
        return _score;
    }

    public static void setCurrentScore(int score)
    {
        _score = score;
    }

    public static void setFirstNumber(int firstNumber)
    {
        _firstNumber = firstNumber;
    }

    public static void setSecondNumber(int secondNumber)
    {
        _secondNumber = secondNumber;
    }

    public static int getFirstNumber()
    {
        return _firstNumber;
    }

    public static int getSecondNumber()
    {
        return _secondNumber;
    }

    public static void setChoices(int[] choices)
    {
        _options = choices;
    }

    public static int[] getChoices()
    {
        return _options;
    }

    #region GoTo 
    public static void gotoPlayScreen()
    {
        SceneManager.LoadScene("Game");
    }

    public static void gotoHome()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public static void gotoCompleted()
    {
        SceneManager.LoadScene("Completed");
    }

    public static void gotoFailedScreen()
    {
        SceneManager.LoadScene("Failed");
    }


    #endregion

    #region Music & Sound

    public static bool getSoundOn()
    {
        return _soundOn;
    }

    public static void setSoundOn(bool on)
    {
        _soundOn = on;
    }

    public static bool getMusicOn()
    {
        return _musicOn;
    }

    public static void setMusicOn(bool on)
    {
        _musicOn = on;
    }

    #endregion

    public static void playCorrect()
    {
        if (getSoundOn())
        {
            playSound("Sounds/correct");
        }
    }

    public static void playHit()
    {
        if (getSoundOn())
        {
            playSound("Sounds/hit");
        }
    }

    public static void playWin()
    {
        if (getSoundOn())
        {
            playSound("Sounds/win");
        }
    }

    public static void playLose()
    {
        if (getSoundOn())
        {
            playSound("Sounds/lose");
        }
    }

    public static void playWrong()
    {
        if (getSoundOn())
        {
            playSound("Sounds/wrong");
        }
    }

    public static void playClick()
    {
        if (getSoundOn()) { 
            playSound("Sounds/click"); 
        }
    }

    public static void playMusic()
    {
        if (getMusicOn())
        {
            playSound("Sounds/music");
        }
    }

    private static void playSound(string filename)
    {
        // Load the AudioClip from the Resources folder
        AudioClip clip = Resources.Load<AudioClip>(filename);

        if (clip != null)
        {
            // Find an AudioSource in the scene
            AudioSource audioSource = FindObjectOfType<AudioSource>();

            // If there's no existing AudioSource, create a new GameObject with an AudioSource
            if (audioSource == null)
            {
                GameObject audioObject = new GameObject("AudioObject");
                audioSource = audioObject.AddComponent<AudioSource>();
            }
            // Play the sound
            audioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogError("Audio file not found in Resources: " + filename);
        }
    }

    public static void playMusic(string filename)
    {
        // Load the AudioClip from the Resources folder
        AudioClip clip = Resources.Load<AudioClip>(filename);

        if (clip != null)
        {
            // Find an AudioSource in the scene
            AudioSource audioSource = FindObjectOfType<AudioSource>();

            // If there's no existing AudioSource, create a new GameObject with an AudioSource
            if (audioSource == null)
            {
                GameObject audioObject = new GameObject("AudioObject");
                audioSource = audioObject.AddComponent<AudioSource>();
            }

            // Here's the important part for looping
            audioSource.loop = true; // Set the AudioClip to loop
            audioSource.clip = clip; // Assign the AudioClip to the AudioSource
            audioSource.Play(); // Start playing the assigned AudioClip
        }
        else
        {
            Debug.LogError("Audio file not found in Resources: " + filename);
        }
    }

    public static GameObject findObjectIncludingInactive(string objectName)
    {
        // This gets all active and inactive GameObjects in the scene.
        List<GameObject> allObjects = new List<GameObject>();
        foreach (GameObject obj in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
        {
            if (obj.hideFlags == HideFlags.None)
            {
                allObjects.Add(obj);
            }
        }

        // Now you loop through them and find the object with the name you specified.
        foreach (GameObject obj in allObjects)
        {
            if (obj.name == objectName)
            {
                return obj; // Found the object. Return it.
            }
        }

        return null; // Object not found.
    }

    public static string getDeviceId()
    {
        if (string.IsNullOrEmpty(_deviceId))
        {
            _deviceId = SystemInfo.deviceUniqueIdentifier;
        }
        return _deviceId;
    }


    // Static method that takes an instance of MonoBehaviour to start the coroutine
    //private static void StartSaveSettingCoroutine()
    //{
    //    f(instance != null)
    //    {
    //        instance.StartCoroutine(MyCoroutine());
    //    }
    //    else
    //    {
    //        Debug.LogError("Instance of CoroutineStarter not set.");
    //    }
    //    var url = getFunFishUrl() + "&score=" + getCurrentScore() + "&soundOn=" + getSoundOn() + "&musicOn=" + getMusicOn();
    //    monoBehaviourInstance.StartCoroutine(MyGlobal.getDataCoroutine(url, (data) => { }));
    //}

    //public static void saveUserSetting()
    //{
    //    StartSaveSettingCoroutine();
    //}        


    public static string getFunFishSaveSettingUrl()
    {
        var url = getFunFishUrl() + "&score=" + getCurrentScore() + "&soundOn=" + getSoundOn() + "&musicOn=" + getMusicOn();
        url = url.Replace("getsetting", "setsetting");
        return url;
    }


    public static string getFunFishUrl()
    {
        var url = "https://funfish.azurewebsites.net/game/getsetting?deviceId=" + MyGlobal.getDeviceId();
        return url;
    }

    public static string[] splitString(string input, string delimiter)
    {
        // Use the StringSplitOptions.RemoveEmptyEntries option to remove empty entries if needed
        return input.Split(new string[] { delimiter }, System.StringSplitOptions.None);
    }
      
    public delegate void DataReceivedCallback(string data);
    public static IEnumerator getDataCoroutine(string url, DataReceivedCallback callback)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error: " + webRequest.error);
            }
            else
            {
                // Use the data, assuming it's text
                string data = webRequest.downloadHandler.text;

                // Invoke the callback with the fetched data
                callback?.Invoke(data);
            }
        }
    }
}
