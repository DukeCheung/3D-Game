using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreRecorder:System.Object//记分员
{
    private int score = 0;

    private static ScoreRecorder instance;
    public static ScoreRecorder getInstance()
    {
        if(instance == null)
        {
            instance = new ScoreRecorder();
        }
        return instance;
    }
    public void record()//加分
    {
        instance.score++;
    }
    public int getScore()
    {
        return instance.score;
    }
    public void reset()
    {
        instance.score = 0;
    }
}
