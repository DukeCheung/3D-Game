using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolmanController : MonoBehaviour {

    private float speed = 2f;
    private int length = 0;
    private int direction = 0;
    private float x;
    private float z;
    public delegate void Hit();
    public static event Hit hit;
    public delegate void Score();
    public static event Score scoreRecord;
    private bool canFollow = true;
    Director dir;
    Vector3 target;

    void Start()
    {
        setNewPath();
        dir = Director.getInstance();
    }
    void Update()
    {
        if (!dir.getState())
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, target, speed * Time.deltaTime);
            if (Vector3.Distance(target, transform.position) < 0.1f)
            {
                direction = (direction + 1) % 4;
                setNewPath();
            }
            if (this.transform.localEulerAngles.x != 0 || this.transform.localEulerAngles.z != 0)
            {
                this.transform.localEulerAngles = new Vector3(0, this.transform.localEulerAngles.y, 0);
            }
            if (this.transform.position.y != 0)
            {
                this.transform.position = new Vector3(this.transform.position.x, 0, this.transform.position.z);
            }
        }
    }

    void OnCollisionEnter(Collision collision)//碰撞
    {  
        if(collision.gameObject.tag == "patrolman")
        {
            
            canFollow = false;//添加变量，当与其他巡逻兵或者墙体碰撞时，优先处理
            direction = (direction + 1) % 4;
            setNewPath();
        }
        else if(collision.gameObject.tag == "wall")
        {
            canFollow = false;
            direction = (direction + 1) % 4;
            setNewPath();
        }
        if (collision.gameObject.tag == "player" && !dir.getState()&&canFollow)
        {
            if (hit != null)
            {
                this.GetComponent<Animator>().SetBool("attack", true);
                dir.end();
                hit();
            }
        }
    }
    void OnCollisionExit(Collision collision)//退出碰撞
    {
        if(collision.gameObject.tag == "patrolman"|| collision.gameObject.tag == "wall")
        {
            canFollow = true;
        }
    }

    void OnTriggerEnter(Collider other)//进入触发器范围
    {
        if(other.transform.tag == "player" && !dir.getState() && canFollow)
        {
            target = other.transform.position;
            this.transform.LookAt(other.transform.position);
            speed *= 2;
        }
    }

    void OnTriggerExit(Collider other)//退出触发器范围
    {
        if (other.transform.tag == "player" && !dir.getState() && canFollow)
        {
            if (scoreRecord != null)
            {
                scoreRecord();
            }
            setNewPath();
            speed /= 2;
        }
    }

    void setNewPath()
    {
        System.Random ran = new System.Random();
        length = ran.Next(5, 6);
        x = this.transform.position.x;
        z = this.transform.position.z;
        if (direction == 0)
        {
            x -= length;
        }
        else if (direction == 1)
        {
            z += length;
        }
        else if (direction == 2)
        {
            x += length;
        }
        else if (direction == 3)
        {
            z -= length;
        }
        if(x < -15)//若超出地图，则设置边缘
        {
            x = -14;
        }
        if(x > 15)
        {
            x = 14;
        }
        if(z < -15)
        {
            z = -14;
        }
        if(z > 15)
        {
            z = 14;
        }
        target = new Vector3(x, 0, z);
        this.transform.LookAt(target);
    }
}
