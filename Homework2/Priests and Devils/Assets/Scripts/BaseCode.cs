using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.MyGame;

namespace Com.MyGame
{
    public enum State { LEFT = 0, LEFT_RIGHT, RIGHT_LEFT, RIGHT, WIN, LOSE };//游戏状态
    public interface Interfaces
    {
        void priestLeftOn();//牧师从左边上船
        void priestRightOn();//牧师从右边上船
        void devilLeftOn();//魔鬼从左边上船
        void devilRightOn();//魔鬼从右边上船
        void moveBoat();//移动船
        void getOffBoat();//下船
    }

    public class Director : System.Object,Interfaces
    {

        private static Director _instance;//单例模式
        private BaseCode _base;
        private GenGameObject genGameobj;
        public State state = State.LEFT;

        public static Director getInstance()
        {
            if (_instance == null)
            {
                _instance = new Director();
            }
            return _instance;
        }
        internal void setBaseCode(BaseCode b)
        {
            if (_base == null)
            {
                _base = b;
            }
        }
        internal void setGenGameObject(GenGameObject obj)
        {
            if (genGameobj == null)
            {
                genGameobj = obj;
            }
        }
        public void priestLeftOn()
        {
            genGameobj.priestLeftOn();
        }
        public void priestRightOn()
        {
            genGameobj.priestRightOn();
        }
        public void devilLeftOn()
        {
            genGameobj.devilLeftOn();
        }
        public void devilRightOn()
        {
            genGameobj.devilRightOn();
        }
        public void moveBoat()
        {
            genGameobj.moveBoat();
        }
        public void getOffBoat()
        {
            genGameobj.getOffBoat();
        }
    }
}

public class BaseCode : MonoBehaviour
{
    void Awake()
    {
        Director my = Director.getInstance();
        my.setBaseCode(this);
    }
}