using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstSceneController : MonoBehaviour, SceneController, Action {

    Director director;
    PatrolFactory factory;
    ScoreRecorder score;
    Camera cam;
    List<GameObject> list = new List<GameObject>();
    GameObject player;
    public float rotate_speed = 5.0f;//旋转速度

    public void LoadResources()
    {
        director = Director.getInstance();
        director.currentSceneController = this;

    }
    void Awake()
    {
        LoadResources();
        factory = Singleton<PatrolFactory>.Instance;
        score = ScoreRecorder.getInstance();
        list = factory.getPatrolmans();
        Instantiate(Resources.Load("Prefabs/Plane"), new Vector3(0, 0, 0), Quaternion.identity);
        cam = Camera.main;
        player = Instantiate(Resources.Load("Prefabs/player"), new Vector3(0, 0, 7), Quaternion.identity) as GameObject;
        cam.transform.parent = player.transform;
        cam.transform.position = new Vector3(0, 5, 0);
        cam.transform.localEulerAngles = new Vector3(40, 0, 0);
        Enable();
    }
    void Enable()//订阅事件
    {
        PatrolmanController.hit += gameOver;
        PatrolmanController.scoreRecord += changeScore;
    }
    void Disable()//取消订阅
    {
        PatrolmanController.hit -= gameOver;
        PatrolmanController.scoreRecord -= changeScore;;
    }
    public void move(float x, float z) {
        if (!director.getState())
        {
 
            player.GetComponent<Animator>().SetBool("run", true);
            player.transform.Translate(0, 0, z * Time.deltaTime);
            player.transform.Rotate(0, x * rotate_speed * 3 * Time.deltaTime, 0);
            if (player.transform.position.y != 0)
            {
                player.transform.position = new Vector3(player.transform.position.x, 0, player.transform.position.z);
            }
            if (player.transform.localEulerAngles.x != 0 || player.transform.localEulerAngles.z != 0)
            {
                player.transform.localEulerAngles = new Vector3(0, player.transform.localEulerAngles.y, 0);
            }
        }
    }

    public void gameOver() {
        director.end();
        player.GetComponent<Animator>().SetBool("run", false);
        player.GetComponent<Animator>().SetBool("idle", true);
        Destroy(player.GetComponent<Rigidbody>());
        factory.removeRigid();
        Disable();
    }
    public void changeScore()
    {
        score.record();
    }
}
