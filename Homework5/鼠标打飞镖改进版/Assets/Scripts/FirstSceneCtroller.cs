using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.MyGame;

public class FirstSceneCtroller : MonoBehaviour,SceneCtroller,Interfaces
{
    Director director;
    IActionManager ac;
    ScoreRecorder score;
    float interval = 0;//发射飞镖的间隔时间
    int level = 0;//难度级别
    public int ufoNum = 1;//每一次发射飞碟的数目

    void Awake()
    {
        director = Director.getInstance();//单例类
        director.currentSceneCtroller = this;
        score = ScoreRecorder.getInstance();
        LoadResource();
    }

    public void LoadResource()
    {
        ac = new IActionManager();
    }

    void Update()
    {
        interval += Time.deltaTime;
        if (interval >= 1f && director.state == State.PLAYING && level > 0)//若间隔大于1s且处于play且已设置好level
        {
            ac.randomAction(ufoNum, level);
            interval = 0;//间隔重新置为0
        }
        ac.play();

        if (Input.GetButtonDown("Fire1"))//开火
        {
            Vector3 mousePosition = Input.mousePosition;
            Camera cam = Camera.main;
            Ray ray = cam.ScreenPointToRay(mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                score.record(level);//计分
                recycle(hit.transform.gameObject.GetComponent<DiskData>());//回收
            }
        }
    }

    void recycle(DiskData disk)
    {
        ac.remove(disk);
    }

    public void start()//开始玩
    {
        director.state = State.PLAYING;
    }
    public void end()//结束
    {
        setLevel(0);
        director.state = State.END;
    }
    public void setLevel(int i)//设置难度
    {
        level = i;
    }
    public void setMode(int i)
    {
        ac.setMode(i);
    }
}