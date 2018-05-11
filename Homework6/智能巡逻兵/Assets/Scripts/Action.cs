using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Action
{
    void move(float x, float z);//移动玩家
    void gameOver();//游戏结束
    void changeScore();//加分
}