using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskFactory : MonoBehaviour
{
    public DiskData diskPrefab;//飞碟预制
    public static DiskFactory _instance;
    List<DiskData> used;//已用过的
    List<DiskData> free;//未用

    public static DiskFactory getInstance()
    {
        if (_instance == null)
        {
            _instance = Singleton<DiskFactory>.Instance;//产生单例
            _instance.used = new List<DiskData>();
            _instance.free = new List<DiskData>();
        }
        return _instance;
    }

    public DiskData getDisk(Ruler ruler)//根据参数设置飞碟
    {
        DiskData disk;
        if (free.Count > 0)//若不空，则从中找
        {
            disk = free[0];
            free.RemoveAt(0);
        }
        else
        {
            disk = Instantiate(diskPrefab);
        }
        disk.ruler = ruler;
        used.Add(disk);
        return disk;
    }

    public void freeDisk(DiskData disk)//回收飞碟
    {
        _instance.free.Add(disk);
        if (!_instance.used.Remove(disk))//抛出错误
        {
            throw new System.Exception();
        }
    }
}