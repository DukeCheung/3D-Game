using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Attack : MonoBehaviour {
    public Slider slider;
    // Use this for initialization
    void Start()
    {
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(Click);
    }
    public void Click()
    {
        if(slider.value >= 10)
        {
            slider.value -= 10;
        }
        else
        {
            slider.value = 0;
        }
    }
}
