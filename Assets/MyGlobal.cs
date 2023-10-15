using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public static class MyGlobal
{
    private static int _answer;
    private static int _score;
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

    public static void incCurrentScore()
    {
        _score++;
    }
}
