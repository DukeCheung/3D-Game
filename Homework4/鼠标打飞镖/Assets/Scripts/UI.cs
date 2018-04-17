using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    Director director;
    ScoreRecorder s;
    Interfaces interfaces;
    private float timer = 0f;
    private int flag = 0;//判断游戏是否结束
    private float second = 0f;
    private float minute = 0f;
    private string str;

    void Awake()
    {
        s = ScoreRecorder.getInstance();
        interfaces = Director.getInstance() as Interfaces;
        director = Director.getInstance();
    }

    void Update()
    {
        if (flag == 1)
        {
            timer += Time.deltaTime;
            if (timer >= 1f)
            {
                second++;
                timer = 0;
            }
            if (second >= 60)
            {
                minute++;
                second = 0;
            }
            if (minute >= 60)
            {
                minute = 0;
            }
        }
    }

    void OnGUI()
    {
        str = string.Format("{0:00}:{1:00}", minute, second);//计时器
        str = "Time: " + str;
        GUIStyle style = new GUIStyle();
        style.fontSize = 20;
        GUI.Label(new Rect(520, 0, 100, 200), str, style);


        int score = s.getScore();//记分
        string ss = "Score: " + score.ToString();
        GUI.Label(new Rect(0, 0, 100, 200), ss, style);

        if(director.state == State.START)
        {
            if (GUI.Button(new Rect(0, 130, 60, 30), "START"))
            {
                interfaces.start();
                flag = 1;
            }
        }
        else if(director.state == State.PLAYING)
        {
            if (GUI.Button(new Rect(0, 25, 60, 30), "Level 1"))
            {
                interfaces.setLevel(1);
            }
            if (GUI.Button(new Rect(0, 60, 60, 30), "Level 2"))
            {
                interfaces.setLevel(2);
            }
            if (GUI.Button(new Rect(0, 95, 60, 30), "Level 3"))
            {
                interfaces.setLevel(3);
            }
            if (GUI.Button(new Rect(0, 130, 60, 30), "END"))
            {
                interfaces.end();
                flag = 0;
            }
        }
        else if(director.state == State.END)
        {
            if (GUI.Button(new Rect(0, 130, 60, 30), "RESET"))
            {
                interfaces.start();
                s.reset();
                flag = 1;
                timer = 0;
                minute = 0;
                second = 0;
            }
        }
    }
}
