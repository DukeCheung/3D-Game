using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UGUI : MonoBehaviour {
    private RectTransform rectTransform;
    // Use this for initialization
    void Start () {
        rectTransform = this.GetComponent<RectTransform>();;
        Camera.main.transform.position = new Vector3(0, 0, -5);
        Vector3 postion = Camera.main.ScreenToWorldPoint(this.transform.position);
        rectTransform.position = postion + new Vector3(0, 1, 5);
	}
}
