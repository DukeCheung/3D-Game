using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.MyGame;

public class UI : MonoBehaviour {

    Director dir;
    Interfaces userInterface;
    private float timer = 0f;
    private int flag = 0;//判断游戏是否结束
    private float second = 0f;
    private float minute = 0f;
    private string str;


    void Awake()
    {
        dir = Director.getInstance();
        userInterface = Director.getInstance() as Interfaces;
    }
    void Update()
    {
        if(flag == 0)
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
        GUIStyle style = new GUIStyle();
        style.fontSize = 20;
        GUI.Label(new Rect(0, 0, 100, 200), str, style);

        if (dir.state == State.WIN)
        {
            flag = 1;
            GUIStyle word = new GUIStyle();
            word.normal.textColor = new Color(0, 0, 1);//设置字体颜色 
            word.fontSize = 35;//字体大小
            GUI.TextField(new Rect(290, 20, 80, 50), "WIN", word);
            Debug.Log("Win");
        }
        else if(dir.state == State.LOSE)
        {
            flag = 1;
            GUIStyle word = new GUIStyle();
            word.normal.textColor = new Color(1, 0, 0);
            word.fontSize = 35;
            GUI.TextField(new Rect(290, 20, 80, 50), "LOSE", word);
            Debug.Log("Lose");
        }
        else if(dir.state == State.LEFT||dir.state == State.RIGHT)//其他状态下不能点击，例如移动过程中
        {    
            if (GUI.Button(new Rect(470, 100, 80, 50), "PriestOn"))
            {
                userInterface.priestOn();
            }
            if (GUI.Button(new Rect(555, 100, 80, 50), "DevilOn"))
            {
                userInterface.devilOn();
            }
            if (GUI.Button(new Rect(470, 170, 80, 50), "GetOff"))
            {
                userInterface.getOffBoat();
            }
            if (GUI.Button(new Rect(555, 170, 80, 50), "MOVE"))
            {
                userInterface.moveBoat();
            }
        }
    }
}
