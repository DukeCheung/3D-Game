using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class UI : NetworkBehaviour
{
    private Action action;
    private string str;
    private int flag = 1;//判断游戏是否结束
    private float timer = 0f;
    private float second = 0f;
    private float minute = 0f;
    private ScoreRecorder s;
    private Director director;
    private int rate = 3;

    void Awake()
    {
        director = Director.getInstance();
        s = ScoreRecorder.getInstance();
    }

    void Update()
    {
        
        action = Director.getInstance().currentSceneController as Action;
        float offsetX = Input.GetAxis("Horizontal");
        float offsetZ = Input.GetAxis("Vertical");
        action.move(offsetX * rate, offsetZ * rate);

        if (!isLocalPlayer)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rate *= 3;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            rate = 3;
        }
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

        if (director.getState() == true)
        {
            flag = 0;
            if (GUI.Button(new Rect(280, 130, 100, 50), "RESET"))
            {
                flag = 1;
                timer = 0;
                minute = 0;
                second = 0;
                director.reset();
                s.reset();
                SceneManager.LoadScene("Scene");
            }
        }
       
    }
}