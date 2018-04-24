using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.MyGame;

namespace Com.MyGame
{
    public enum State { START, PLAYING, END };
    public interface SceneCtroller
    {
        void LoadResource();
    }
  
    public class Director : System.Object//场景控制器
    {
        private static Director _instance;
        public SceneCtroller currentSceneCtroller { get; set; }
        public State state = State.START;
        public static Director getInstance()
        {
            if (_instance == null)
            {
                _instance = new Director();
            }
            return _instance;
        }

    }
    public class ScoreRecorder//记分员
    {
        public static ScoreRecorder _instance;
        public int score = 0;

        public static ScoreRecorder getInstance()
        {
            if (_instance == null)
            {
                _instance = new ScoreRecorder();
            }
            return _instance;
        }
        public void record(int s)
        {
            _instance.score += s;
        }
        public int getScore()
        {
            return _instance.score;
        }
        public void reset()
        {
            _instance.score = 0;
        }
    }
}


