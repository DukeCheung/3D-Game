using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAction
{
    void play();
    void randomAction(int num, int level);
    void remove(DiskData disk);
}
public class IActionManager:IAction
{
    CCActionManager cc;
    PhysisActionManager ph;
    int mode;
    public IActionManager()
    {
        cc = new CCActionManager();
        ph = new PhysisActionManager();
        mode = 0;
    }
    public void setMode(int i)
    {
        mode = i;
    }
    public void play()
    {
        if (mode == 0)
        {
            cc.play();
        }
        else
        {
            ph.play();
        }
    }
    public void randomAction(int num, int level)
    {
        if(mode == 0)
        {
            cc.randomAction(num, level);
        }
        else
        {
            ph.randomAction(num, level);
        }
    }
    public void remove(DiskData disk)
    {
        if (mode == 0)
        {
            cc.remove(disk);
        }
        else
        {
            ph.remove(disk);
        }
    }
}


