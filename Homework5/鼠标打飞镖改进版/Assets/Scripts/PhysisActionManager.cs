using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysisActionManager : IAction
{

    List<DiskData> diskList;
    DiskFactory diskFactory;
    Color color;

    public PhysisActionManager()
    {
        diskList = new List<DiskData>();
        diskFactory = DiskFactory.getInstance();
    }
    void setColor(int c)
    {
        if (c == 1)
        {
            color = Color.red;
        }
        else if (c == 2)
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
            Vector3 position = new Vector3(0, 0, -20f);
            float speed = level * 10f;//飞碟速度
            Vector3 force = new Vector3(
                UnityEngine.Random.Range(-5f, 5f),
                UnityEngine.Random.Range(10f, 12f),
                speed);
            Ruler ruler = new Ruler(color, position, 0, Vector3.zero);//新建一个规则
            diskList.Add(diskFactory.getDisk(force,ruler));//添加飞碟
        }
    }
    public void play()
    {
        for (int i = 0; i < diskList.Count; i++)
        {
            if (diskList[i].gameObject.transform.position.y < 0|| diskList[i].gameObject.transform.position.z > 100.0f|| 
                diskList[i].gameObject.transform.position.x > 100.0f|| diskList[i].gameObject.transform.position.x < -100.0f)//若飞碟低于地面，则回收
            {
                remove(diskList[i]);
            }
        }
    }
    public void move()
    {

    }
    public void remove(DiskData disk)
    {
        disk.removeForce();
        disk.gameObject.transform.position = new Vector3(0, 0, -20f);//重新放置到摄像机后
        diskFactory.freeDisk(disk);//回收
        diskList.Remove(disk);
    }
}
