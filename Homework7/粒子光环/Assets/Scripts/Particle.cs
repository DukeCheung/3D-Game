using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    public ParticleSystem particleSys;
    private ParticleSystem.Particle[] particleArray;//存储粒子的数组
    private int number = 5000;//粒子数量
    private float[] particleAngle;//每个粒子的角度
    private float[] particleRadius;//每个粒子的实际半径
    private float maxRadius = 10f;//最大半径
    private float minRadius = 6f;//最小半径
    private float time = 0;//当前粒子的时间
    public float speed = 0.05f;//粒子速度
    private float range = 0.05f;//游离范围 
    private int t = 5;//分层
    void Start()
    {
        particleSys = GetComponent<ParticleSystem>();
        particleArray = new ParticleSystem.Particle[number];
        particleSys.maxParticles = number;//设置最大粒子量
        particleAngle = new float[number];
        particleRadius = new float[number];

        Init();  
    }

    void Update()
    {
        for (int i = 0; i < number; i++)
        {
            if (i % 2 == 0)//偶数加
            {
                particleAngle[i] += speed * (i % t + 1);//分层，之所以要加一，防止speed*0
            }
            else
            {
                particleAngle[i] -= speed * (i % t + 1);
            }
            
            particleAngle[i] = (particleAngle[i] + 360) % 360;//角度增加
            float radian = particleAngle[i] / 180 * Mathf.PI;//转化为弧度，参数需要

            time = (time + Time.deltaTime) % 100;//时间增加
            particleRadius[i] += (Mathf.PingPong(time/minRadius/maxRadius, range) - range / 2.0f);//使用函数，使之显示出游离状态

            
            particleArray[i].position = new Vector3(particleRadius[i] * Mathf.Cos(radian), particleRadius[i] * Mathf.Sin(radian), 0f);//设置位置
        }
        particleSys.SetParticles(particleArray, particleArray.Length);//设置粒子系统的粒子数量
    }


    public void Init()
    {
        particleSys.Emit(number);//发射粒子
        particleSys.GetParticles(particleArray);


        for (int i = 0; i < number; i++)
        { 
            float randomAngle = Random.Range(0f, 360f);//随机角度  
            float radian = randomAngle / 180 * Mathf.PI;//角度变换成弧度  
            float midRadius = (maxRadius + minRadius) / 2; 
            float minRate = Random.Range(1.0f, midRadius / minRadius);//内半径与中间半径比例
            float maxRate = Random.Range(midRadius / maxRadius, 1.0f);//外半径与中间半径比例
            float radius = Random.Range(minRadius * minRate, maxRadius * maxRate);
            //若直接将区间设置为最小与最大半径，则粒子将会均匀分布在这之间，经过上面两步操作之后，粒子将会大半集中在中部，通过计算提高比例

            particleAngle[i] = randomAngle;
            particleRadius[i] = radius;
            particleArray[i].position = new Vector3(radius * Mathf.Cos(radian), radius * Mathf.Sin(radian), 0.0f);//每个粒子坐标 
        }
        particleSys.SetParticles(particleArray, particleArray.Length);//设置粒子系统的粒子数量
    }

}