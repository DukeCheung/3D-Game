using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Director : System.Object
{
    private static Director _instance;
    private bool isEnd = false;//标志游戏是否结束
    public SceneController currentSceneController { get; set; }
    public static Director getInstance()
    {
        if (_instance == null)
        {
            _instance = new Director();
        }
        return _instance;
    }
    public bool getState()
    {
        return _instance.isEnd;
    }
    public void end()
    {
        _instance.isEnd = true;
    }
    public void reset()//重新开始游戏
    {
        _instance.isEnd = false;
    }
    public int getFPS()
    {
        return Application.targetFrameRate;
    }
    public void setFPS(int fps)
    {
        Application.targetFrameRate = fps;
    }
}
