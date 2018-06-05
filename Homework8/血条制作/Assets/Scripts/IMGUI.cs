using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IMGUI : MonoBehaviour {
    private Rect blood;//血条
    private float value;//代表当前血量
	// Use this for initialization
	void Start () {
        value = 100f;     
        blood = new Rect(250, 100, 150, 20);
	}
	
	// Update is called once per frame
	void OnGUI()
    {
        if (GUI.Button(new Rect(20, 20, 80, 40), "Attack"))
        {
            if (value >= 10)
            {
                value -= 10;
            }
            else
            {
                value = 0;

            }
        }
        if (GUI.Button(new Rect(20, 130, 80, 40), "Revive"))
        {
            value = 100;
        }
        GUI.HorizontalScrollbar(blood, 0.0f, value, 0.0f, 100f);
    }
}
