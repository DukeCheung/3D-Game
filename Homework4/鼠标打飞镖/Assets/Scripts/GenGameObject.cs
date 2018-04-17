using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenGameObject : MonoBehaviour
{
    Director director;
    List<DiskData> diskList;
    DiskFactory diskFactory;
    ScoreRecorder score;
    float gravity = 9.8f;
    float interval = 0;//发射飞镖的间隔时间
    int level = 0;//难度级别
    Color color = Color.red;
    float speed = 0;//飞碟速度
    public int ufoNum = 3;//每一次发射飞碟的数目

    void Awake()
    {
        director = Director.getInstance();//单例类
        score = ScoreRecorder.getInstance();
        diskFactory = DiskFactory.getInstance();
        director.setGenGameObject(this);
        diskList = new List<DiskData>();
    }

    void Update()
    {
        interval += Time.deltaTime;
        if (interval >= 1f && director.state == State.PLAYING&&level>0)//若间隔大于1s且处于play且已设置好level
        {
            for (int i = 0; i < ufoNum; i++)
            {
                Vector3 position = new Vector3(0, 0, 0);//飞碟的起始位置
                speed = level * 20f;//飞碟速度
                Vector3 direction = new Vector3(
                    UnityEngine.Random.Range(-10f, 10f),
                    UnityEngine.Random.Range(0, 80f),
                    UnityEngine.Random.Range(50f, 100f));//飞碟方向
                direction.Normalize();//单位化该方向向量，以免产生较大误差
                Ruler ruler = new Ruler(color, position, speed, direction);//新建一个规则
                diskList.Add(diskFactory.getDisk(ruler));//添加飞碟
            }

            interval = 0;//间隔重新置为0
        }

        for (int i = 0; i < diskList.Count; i++)
        {
            move(diskList[i]);//移动飞碟
        }

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

        for (int i = 0; i < diskList.Count; i++)
        {
            if (diskList[i].gameObject.transform.position.y < 0)//若飞碟低于地面，则回收
            {
                recycle(diskList[i]);
            }
        }
    }

    void recycle(DiskData disk)
    {
        disk.gameObject.transform.position = new Vector3(0, 0, -100f);//重新放置到摄像机后
        diskFactory.freeDisk(disk);//回收
        diskList.Remove(disk);
    }
    void move(DiskData disk)
    {
        disk.y -= gravity * Time.deltaTime;//模拟重力
        float x = disk.gameObject.transform.position.x + disk.x * Time.deltaTime;
        float y = disk.gameObject.transform.position.y + disk.y * Time.deltaTime;
        float z = disk.gameObject.transform.position.z + disk.z * Time.deltaTime;
        disk.gameObject.transform.position = new Vector3(x, y, z);//下一帧的位置
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
        if (i == 1)
        {
            color = Color.red;
        }
        else if (i == 2)
        {
            color = Color.blue;
        }
        else if(i == 3)
        {
            color = Color.yellow;
        }
    }
}