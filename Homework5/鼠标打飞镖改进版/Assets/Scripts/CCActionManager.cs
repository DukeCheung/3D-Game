using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCActionManager : IAction {

    List<DiskData> diskList;
    DiskFactory diskFactory;
    float gravity = 9.8f;
    Color color;

    public CCActionManager()
    {
        diskList = new List<DiskData>();
        diskFactory = DiskFactory.getInstance();
    }
    void setColor(int c)
    {
        if(c == 1)
        {
            color = Color.red;
        }
        else if(c == 2)
        {
            color = Color.blue;
        }
        else if (c == 3)
        {
            color = Color.yellow;
        }
    }
    public void randomAction(int ufoNum, int level)
    {
        setColor(level);
        for (int i = 0; i < ufoNum; i++)
        {
            Vector3 position = new Vector3(0, 0, 0);//飞碟的起始位置
            float speed = level * 10f;//飞碟速度
            Vector3 direction = new Vector3(
                UnityEngine.Random.Range(-10f, 10f),
                UnityEngine.Random.Range(40f, 80f),
                UnityEngine.Random.Range(50f, 100f));//飞碟方向
            direction.Normalize();//单位化该方向向量，以免产生较大误差
            Ruler ruler = new Ruler(color, position, speed, direction);//新建一个规则
            diskList.Add(diskFactory.getDisk(ruler));//添加飞碟
        }
    }
    public void play()
    {
        for (int i = 0; i < diskList.Count; i++)
        {
            move(diskList[i]);//移动飞碟
        }
        for (int i = 0; i < diskList.Count; i++)
        {
            if (diskList[i].gameObject.transform.position.y < 0)//若飞碟低于地面，则回收
            {
                remove(diskList[i]);
            }
        }
    }

    public void move(DiskData disk)
    {
        disk.y -= gravity * Time.deltaTime;//模拟重力
        float x = disk.gameObject.transform.position.x + disk.x * Time.deltaTime;
        float y = disk.gameObject.transform.position.y + disk.y * Time.deltaTime;
        float z = disk.gameObject.transform.position.z + disk.z * Time.deltaTime;
        disk.gameObject.transform.position = new Vector3(x, y, z);//下一帧的位置
    }

    public void remove(DiskData disk)
    {
        disk.gameObject.transform.position = new Vector3(0, 0, -100f);//重新放置到摄像机后
        diskFactory.freeDisk(disk);//回收
        diskList.Remove(disk);
    }
}
