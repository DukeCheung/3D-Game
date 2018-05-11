using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolFactory : MonoBehaviour {

    List<GameObject> list = new List<GameObject>();
    public List<GameObject> getPatrolmans()
    {
        int[] x = new int[2] { -10, 5 };
        int[] z = new int[3] { -5, 0, 5 };
        Vector3[] location = new Vector3[9];
        int count = 0;
        for(int i = 0;i < 2; i++)
        {
            for(int j = 0;j < 3; j++)
            {
                GameObject patrolman = Instantiate(Resources.Load("Prefabs/Patrolman"), new Vector3(0, 0, 7), Quaternion.identity) as GameObject;
                location[count] = new Vector3(x[i], 0, z[j]);
                patrolman.transform.position = location[count];
                patrolman.GetComponent<Animator>().SetBool("run", true);//默认巡逻兵为移动
                list.Add(patrolman);
                count++;
            }
        }
        return list;
    }
    public void removeRigid()//结束后调用，移除刚体，以免对象之间继续碰撞
    {
        for(int i = 0;i < list.Count; i++)
        {
            list[i].GetComponent<Animator>().SetBool("run", false);
            list[i].GetComponent<Animator>().SetBool("idle", true);
            Destroy(list[i].GetComponent<Rigidbody>());
        }
    }
}
