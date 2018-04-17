using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State { START, PLAYING, END};
public interface Interfaces//接口
{
    void setLevel(int i);
    void start();
    void end();
}
public class Director : System.Object,Interfaces//场景控制器
{
    private static Director _instance;
    private GenGameObject genGameobj;
    public State state = State.START;
    public static Director getInstance()
    {
        if (_instance == null)
        {
            _instance = new Director();
        }
        return _instance;
    }
    internal void setGenGameObject(GenGameObject obj)
    {
        if (genGameobj == null)
        {
            genGameobj = obj;
        }
    }
    public void setLevel(int i)
    {
        genGameobj.setLevel(i);
    }
    public void start()
    {
        genGameobj.start();
    }
    public void end()
    {
        genGameobj.end();
    }
    
}
public class ScoreRecorder//记分员
{
    public static ScoreRecorder _instance;
    public int score = 0;

    public static ScoreRecorder getInstance()
    {
        if (_instance == null)
        {
            _instance = new ScoreRecorder();
        }
        return _instance;
    }
    public void record(int s)
    {
        _instance.score += s;
    }
    public int getScore()
    {
        return _instance.score;
    }
    public void reset()
    {
        _instance.score = 0;
    }
}
