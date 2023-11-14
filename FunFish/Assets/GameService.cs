using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameService : MonoBehaviour
{
    private string _oldQuestion = "";
    // Start is called before the first frame update
    void Start()
    {
        displayNewQuestion();
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

        for (var i = 0; i < 4; i++)
        {
            GameObject.Find("btnAnswer" + i).GetComponent<Image>().color = Color.yellow;
            GameObject.Find("btnAnswer" + i).GetComponentInChildren<TextMeshProUGUI>().text = _choices[i].ToString();
        }

    }

    public void showCorrectAnswer()
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
    }

    public void incSlider()
    {
        var slider = GameObject.Find("slider").GetComponent<Slider>();
        if (slider.value >= slider.maxValue)
        {
            slider.value = slider.minValue;
        }
        else
        {
            slider.value = slider.value + 1;
        }

        incScore();
    }


    public void incScore()
    {
        var obj = GameObject.Find("txtScore").GetComponent<TextMeshProUGUI>();
        var score = int.Parse(obj.text);
        score++;
        obj.text = score.ToString();
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

            //Debug.Log("Clicked button with value: " + value);
        }
    }

}
