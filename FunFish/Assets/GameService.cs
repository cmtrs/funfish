using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameService : MonoBehaviour
{
    private string _oldQuestion = "";
    public int _currentTime = 0; // Number of seconds for the countdown
    public int _timeCounter = 15;
    private TextMeshProUGUI _countdownDisplay;

    // Start is called before the first frame update
    void Start()
    {

        //_countdownDisplay = GameObject.Find("txtTimer").GetComponent<TextMeshProUGUI>();
        //_currentTime = _timeCounter;

        var txtScore = GameObject.Find("txtScore").GetComponent<TextMeshProUGUI>();
        txtScore.text = MyGlobal.getCurrentScore().ToString();

        displayNewQuestion();
        // Start the countdown
      //  StartCoroutine(CountdownToStart());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void displayNewQuestion()
    {
        var txt = getRandomEquation();

        // Make sure question is not repeated
        while (txt == _oldQuestion)
        {
            txt = getRandomEquation();
        }
        _oldQuestion = txt;

        GameObject.Find("txtQuestion").GetComponent<TextMeshProUGUI>().text = txt;
        var answer = MyGlobal.getCurrentAnswer();
        int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        shuffle(numbers);
        var list = new List<int>();
        for (var i = 0; i < numbers.Length; i++)
        {
            if (numbers[i] != answer)
            {
                list.Add(numbers[i]);
            }
            if (list.Count >= 3)
            {
                break;
            }
        }
        int[] _choices;
        list.Add(answer);
        _choices = list.ToArray();
        shuffle(_choices);

        MyGlobal.setChoices(_choices);

        for (var i = 0; i < 4; i++)
        {
            GameObject.Find("btnAnswer" + i).GetComponent<Image>().color = Color.yellow;
            GameObject.Find("btnAnswer" + i).GetComponentInChildren<TextMeshProUGUI>().text = _choices[i].ToString();
        }

    }

    public void highlightCorrectAnswer()
    {
        for (var i = 0; i < 4; i++)
        {
            var v = int.Parse(GameObject.Find("btnAnswer" + i).GetComponentInChildren<TextMeshProUGUI>().text);
            if (v == MyGlobal.getCurrentAnswer())
            {
                GameObject.Find("btnAnswer" + i).GetComponent<Image>().color = Color.white;
                break;
            }
        }

        GameObject.Find("txtQuestion").GetComponent<TextMeshProUGUI>().text = MyGlobal.getFirstNumber() + " ÷ " + MyGlobal.getSecondNumber() + " = " + MyGlobal.getCurrentAnswer();

    }

    public void incSlider()
    {
        incScore();
        var slider = GameObject.Find("slider").GetComponent<Slider>();
        slider.value = slider.value + 1;
        if (slider.value >= slider.maxValue)
        {
            MyGlobal.gotoCompleted();
        }
    }


    public void incScore()
    {
        var obj = GameObject.Find("txtScore").GetComponent<TextMeshProUGUI>();
        var score = int.Parse(obj.text);
        score++;
        obj.text = score.ToString();
        MyGlobal.setCurrentScore(score);

        StartCoroutine(MyGlobal.getDataCoroutine(MyGlobal.getFunFishSaveSettingUrl(), (data) => { }));
    }

    private string getRandomEquation()
    {
        int dividend, divisor, reminder;
        dividend = Random.Range(2, 10); // Random number between 2 and 9
        var list = new List<int>();
        // dividend = 2; // Test value
        for (var i = 1; i <= dividend; i++)
        {
            reminder = dividend % i;
            if (reminder == 0)
            {
                list.Add(i);
            }
        }

        var nums = list.ToArray();
        shuffle(nums);
        divisor = nums[0];

        // If the divided by 1 or same number then
        if ((divisor == 1) || (divisor == dividend))
        {
            if (nums.Length > 2) // Picks a number that's different
            {
                foreach (var num in nums)
                {
                    if (num != divisor)
                    {
                        divisor = num;
                        break;
                    }
                }
            }
        }

        MyGlobal.setFirstNumber(dividend);
        MyGlobal.setSecondNumber(divisor);
        MyGlobal.setCurrentAnswer(dividend / divisor);

        return $"{dividend} ÷ {divisor} =";
    }

    public void shuffle(int[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            int temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }

    public void checkAnswer(Button clickedButton)
    {
        TextMeshProUGUI tmp = clickedButton.GetComponentInChildren<TextMeshProUGUI>();
        if (tmp != null)
        {
            int value = int.Parse(tmp.text);
            if (value == MyGlobal.getCurrentAnswer())
            {
                MyGlobal.playCorrect();

                throwBall();
                incSlider();
                displayNewQuestion();
            }
            else
            {
                MyGlobal.playWrong();

                highlightCorrectAnswer();
                DelayedAction(1.4f, () => { displayNewQuestion(); });
            }
        }
    }

    public void throwBall()
    {
        GameObject otherObject = GameObject.Find("Player");
        if (otherObject != null)
        {
            var obj = otherObject.GetComponent<PlayerController>();
            if (obj != null)
            {
                obj.throwBubble();
            }
        }
    }
    private void DelayedAction(float delaySeconds, System.Action callback)
    {
        StartCoroutine(DelayCoroutine(delaySeconds, callback));
    }


    private IEnumerator DelayCoroutine(float delaySeconds, System.Action callback)
    {
        // Wait for the specified number of seconds
        yield return new WaitForSeconds(delaySeconds);

        // Invoke the callback function after the delay
        callback?.Invoke();
    }


    //IEnumerator CountdownToStart()
    //{
    //    while (_currentTime > 0)
    //    {
    //        // Display the time remaining
    //        _countdownDisplay.text = _currentTime.ToString();

    //        yield return new WaitForSeconds(1f); // wait for a second

    //        _currentTime--; // decrement the countdown
    //    }

    //    // When countdown reaches zero, you might want to start the game, or trigger some event.
    //    // countdownDisplay.text = "GO!"; // For example, indicating that it's time to start

    //    // Add whatever you want to happen after the countdown ends
    //    // StartGame(); or any other custom method
    //    // SceneManager.LoadScene("Failed");
    //    MyGlobal.gotoFailedScreen();
    //}

}
