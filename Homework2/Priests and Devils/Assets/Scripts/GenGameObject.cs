using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.MyGame;

public class GenGameObject : MonoBehaviour {
    public float speed = 1.0f;//船只移动速度
    List<GameObject> priest_left = new List<GameObject>();//左岸牧师
    List<GameObject> priest_right = new List<GameObject>();
    List<GameObject> devil_left = new List<GameObject>();//左岸魔鬼
    List<GameObject> devil_right = new List<GameObject>();
    Vector3 shore_left = new Vector3(-7, 0, 0);//存储左岸位置
    Vector3 shore_right = new Vector3(2, 0, 0);
    Vector3 boat_left = new Vector3(-4.9f, 0, 0);//存储左船位置
    Vector3 boat_right = new Vector3(-0.1f, 0, 0);
    Vector3 priLeftPos = new Vector3(-8.2f, 1.5f, 0);//左船牧师位置
    Vector3 priRightPos = new Vector3(2.2f, 1.5f, 0);
    Vector3 devilLeftPos = new Vector3(-7, 0.9f, 0);//左船魔鬼位置
    Vector3 devilRightPos = new Vector3(1, 0.9f, 0);
    GameObject[] Boat = new GameObject[2];//船上的乘客
    GameObject boat;//船
    Director dir;//导演
    
    void Awake()
    {
        dir = Director.getInstance();
        dir.setGenGameObject(this);
        load();//加载游戏场景
    }
    void Update()
    {
        changePosition(priest_left, priLeftPos);//逐帧校验每个对象的位置
        changePosition(priest_right, priRightPos);
        changePosition(devil_left, devilLeftPos);
        changePosition(devil_right, devilRightPos);
        if (dir.state == State.LEFT_RIGHT)
        {
            boat.transform.position = Vector3.MoveTowards(boat.transform.position, boat_right, Time.deltaTime * speed);//从左移动船只到右边
            if (boat.transform.position == boat_right)//到了之后改变游戏状态
            {
                dir.state = State.RIGHT;
            }
        }
        else if (dir.state == State.RIGHT_LEFT)
        {
            boat.transform.position = Vector3.MoveTowards(boat.transform.position, boat_left, Time.deltaTime * speed);
            if (boat.transform.position == boat_left)
            {
                dir.state = State.LEFT;
            }
        }
        check();//检测输赢
    }
    void load()
    {
        Instantiate(Resources.Load("Prefabs/Shore"), shore_left, Quaternion.identity);
        Instantiate(Resources.Load("Prefabs/Shore"), shore_right, Quaternion.identity);
        boat = Instantiate(Resources.Load("Prefabs/Boat"), boat_left, Quaternion.identity) as GameObject;
        for (int i = 0;i < 3; i++)
        {
            priest_left.Add(Instantiate(Resources.Load("Prefabs/Priest")) as GameObject);
            devil_left.Add(Instantiate(Resources.Load("Prefabs/Devil")) as GameObject);
        }
    }
    public void changePosition(List<GameObject> obj, Vector3 pos)
    {
        float distance = 0.4f;//两个物体之间的水平距离
        for (int i = 0; i < obj.Count; i++)
        {
            obj[i].transform.position = new Vector3(pos.x+distance*i, pos.y, pos.z);
        }
    }
    public bool isBoatEmpty()//判断船只是否为空
    {
        for(int i = 0;i < 2; i++)
        {
            if (Boat[i] != null)
            {
                return false;
            }
        }
        return true;
    }
    public bool isBoatFull()
    {
        for (int i = 0; i < 2; i++)
        {
            if (Boat[i] == null)
            {
                return false;
            }
        }
        return true;
    }
    public void moveBoat()//移动船只
    {
        if (!isBoatEmpty())
        {
            if (dir.state == State.LEFT)
            {
                dir.state = State.LEFT_RIGHT;
            }
            else if(dir.state == State.RIGHT)
            {
                dir.state = State.RIGHT_LEFT;
            }
        }
    }
    public void priestOn()//牧师上船
    {
        if (!isBoatFull())
        {
            if(dir.state == State.LEFT && priest_left.Count != 0)//左岸
            {
                GameObject p = priest_left[priest_left.Count - 1];
                priest_left.RemoveAt(priest_left.Count - 1);
                p.transform.parent = boat.transform;//跟随船移动
                if (Boat[0] == null)
                {
                    Boat[0] = p;
                    Boat[0].transform.position = new Vector3(-5.2f, 0.8f, 0);
                }
                else
                {
                    Boat[1] = p;
                    Boat[1].transform.position = new Vector3(-4.6f, 0.8f, 0);
                }
            }
            else if(dir.state == State.RIGHT && priest_right.Count != 0)//右岸
            {
                GameObject p = priest_right[priest_right.Count - 1];
                priest_right.RemoveAt(priest_right.Count - 1);
                p.transform.parent = boat.transform;
                if (Boat[0] == null)
                {
                    Boat[0] = p;
                    Boat[0].transform.position = new Vector3(-0.3f, 0.8f, 0);
                }
                else
                {
                    Boat[1] = p;
                    Boat[1].transform.position = new Vector3(0.2f, 0.8f, 0);
                }
            }
            
        }
    }
    public void devilOn()//魔鬼上船
    {
        if (!isBoatFull())
        {
            if(dir.state == State.LEFT && devil_left.Count != 0)
            {
                GameObject p = devil_left[devil_left.Count - 1];
                devil_left.RemoveAt(devil_left.Count - 1);
                p.transform.parent = boat.transform;
                if (Boat[0] == null)
                {
                    Boat[0] = p;
                    Boat[0].transform.position = new Vector3(-5.2f, 0.3f, 0);
                }
                else
                {
                    Boat[1] = p;
                    Boat[1].transform.position = new Vector3(-4.6f, 0.3f, 0);
                }
            }
            else if(dir.state == State.RIGHT && devil_right.Count != 0)
            {
                GameObject p = devil_right[devil_right.Count - 1];
                devil_right.RemoveAt(devil_right.Count - 1);
                p.transform.parent = boat.transform;
                if (Boat[0] == null)
                {
                    Boat[0] = p;
                    Boat[0].transform.position = new Vector3(-0.3f, 0.3f, 0);
                }
                else
                {
                    Boat[1] = p;
                    Boat[1].transform.position = new Vector3(0.2f, 0.3f, 0);
                }
            }
        }
    }
    public void getOffBoat()
    {
        for(int i = 0;i < 2; i++)
        {
            if (Boat[i] != null)
            {
                Boat[i].transform.parent = null;
                if (dir.state == State.LEFT)
                {
                    if (Boat[i].tag == "Priest")
                        priest_left.Add(Boat[i]);
                    else if (Boat[i].tag == "Devil")
                        devil_left.Add(Boat[i]);
                }
                else if (dir.state == State.RIGHT)
                {
                    if (Boat[i].tag == "Priest")
                        priest_right.Add(Boat[i]);
                    else if (Boat[i].tag == "Devil")
                        devil_right.Add(Boat[i]);
                }
                Boat[i] = null;
                break;
            }
        }
    }
    public void check()
    {
        if(priest_right.Count == 3 && devil_right.Count == 3)
        {
            dir.state = State.WIN;
            return;
        }
        int priestBoat = 0, devilBoat = 0, priest_Left = 0, priest_Right = 0, devil_Left = 0, devil_Right = 0;
        for(int i = 0;i < 2; i++)
        {
            if (Boat[i] != null)
            {
                if(Boat[i].tag == "Priest")
                {
                    priestBoat++;
                }
                else if(Boat[i].tag == "Devil")
                {
                    devilBoat++;
                }
            }
        }
        if(dir.state == State.LEFT)
        {
            priest_Left = priest_left.Count + priestBoat;
            devil_Left = devil_left.Count + devilBoat;
            devil_Right = devil_right.Count;
            priest_Right = priest_right.Count;
        }
        else if(dir.state == State.RIGHT)
        {
            priest_Left = priest_left.Count;
            devil_Left = devil_left.Count;
            devil_Right = devil_right.Count + devilBoat;
            priest_Right = priest_right.Count + priestBoat;
        }
        if ((priest_Left < devil_Left&&priest_Left!=0) || (priest_Right < devil_Right&&priest_Right!=0))
        {
            dir.state = State.LOSE;
        }
    }
}
