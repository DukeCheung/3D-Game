using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calculator : MonoBehaviour {


    private string textAreaString = "0";//文本框显示的字符串
    private string preResult = "";//记录上一次运算的结果，字符串
    public int mode = 0;//模式有四种，加减乘除，默认为0;

    public void Cal(int m, int n, string s)//四种计算模式
    {
        if(mode == 1)//加
        {
            string temp = textAreaString.Substring(preResult.Length+1);
            double num = System.Convert.ToDouble(preResult) + System.Convert.ToDouble(temp);
            textAreaString = preResult = num.ToString("0.0");
            mode = n;
        }
        else if (mode == 2)//减
        {
            string temp = textAreaString.Substring(preResult.Length+1);
            double num = System.Convert.ToDouble(preResult) - System.Convert.ToDouble(temp);
            textAreaString = preResult = num.ToString("0.0");
            mode = n;
        }
        else if (mode == 3)//乘
        {
            string temp = textAreaString.Substring(preResult.Length+1);
            double num = System.Convert.ToDouble(preResult) * System.Convert.ToDouble(temp);
            textAreaString = preResult = num.ToString("0.0");
            mode = n;
        }
        else if (mode == 4)//除
        {
            string temp = textAreaString.Substring(preResult.Length+1);
            double num = System.Convert.ToDouble(preResult) / System.Convert.ToDouble(temp);
            textAreaString = preResult = num.ToString("0.0");
            mode = n;
        }
        if (textAreaString == "0.0")//去除末尾多余的0
        {
            preResult = textAreaString = "0";
        }
        else if (textAreaString.Substring(textAreaString.Length - 2) == ".0")
        {
            preResult = textAreaString = textAreaString.Substring(0, textAreaString.Length - 2);
        }
        if(mode == 1)
        {
            textAreaString += "+";
        }
        else if(mode == 2)
        {
            textAreaString = textAreaString + "-";
        }
        else if(mode == 3)
        {
            textAreaString += "×";
        }
        else if(mode == 4)
        {
            textAreaString += "÷";
        }
        Debug.Log("Succeed!");//提示
    }
    void OnGUI()
    {
        textAreaString = GUI.TextArea(new Rect(210, 40, 215, 25), textAreaString);//文本框
        if (GUI.Button(new Rect(210, 70, 50, 50), "AC"))//BUTTON
        {
            textAreaString = "0";
            mode = 0;
        }
        if (GUI.Button(new Rect(265, 70, 50, 50), "←"))
        {
            if (textAreaString != "0")
            {
                if (textAreaString.Length != 1)
                {
                    textAreaString = textAreaString.Substring(0, textAreaString.Length - 1);
                }
                else
                    textAreaString = "0";
            }
            else
            {
                mode = 0;
            }
        }
        if (GUI.Button(new Rect(320, 70, 50, 50), "."))
        {
            textAreaString = textAreaString+".";
        }
        if (GUI.Button(new Rect(375, 70, 50, 50), "×"))
        {
            if (mode == 0)
            {
                preResult = textAreaString;
                textAreaString = textAreaString + "×";
                mode = 3;
            }
            else
                Cal(mode, 4, textAreaString);
            
        }
        if (GUI.Button(new Rect(210, 125, 50, 50), "7"))
        {
            if (textAreaString == "0")
            {
                textAreaString = "";
            }
            textAreaString = textAreaString + "7";
        }
        if (GUI.Button(new Rect(265, 125, 50, 50), "8"))
        {
            if (textAreaString == "0")
            {
                textAreaString = "";
            }
            textAreaString = textAreaString + "8";
        }
        if (GUI.Button(new Rect(320, 125, 50, 50), "9"))
        {
            if (textAreaString == "0")
            {
                textAreaString = "";
            }
            textAreaString = textAreaString + "9";
        }
        if (GUI.Button(new Rect(375, 125, 50, 50), "÷"))
        {
            if (mode == 0)
            {
                preResult = textAreaString;
                textAreaString = textAreaString + "÷";
                mode = 4;
            }
            else
                Cal(mode, 3, textAreaString);
        }
        if (GUI.Button(new Rect(210, 180, 50, 50), "4"))
        {
            if (textAreaString == "0")
            {
                textAreaString = "";
            }
            textAreaString = textAreaString + "4";
        }
        if (GUI.Button(new Rect(265, 180, 50, 50), "5"))
        {
            if (textAreaString == "0")
            {
                textAreaString = "";
            }
            textAreaString = textAreaString + "5";
        }
        if (GUI.Button(new Rect(320, 180, 50, 50), "6"))
        {
            if (textAreaString == "0")
            {
                textAreaString = "";
            }
            textAreaString = textAreaString + "6";
        }
        if (GUI.Button(new Rect(375, 180, 50, 50), "+"))
        {
        
            if (mode == 0)
            {
                preResult = textAreaString;
                textAreaString = textAreaString + "+";
                mode = 1;
            }
            else
                Cal(mode, 1, textAreaString);
        }
        if (GUI.Button(new Rect(210, 235, 50, 50), "1"))
        {
            if (textAreaString == "0")
            {
                textAreaString = "";
            }
            textAreaString = textAreaString + "1";
        }
        if (GUI.Button(new Rect(265, 235, 50, 50), "2"))
        {
            if (textAreaString == "0")
            {
                textAreaString = "";
            }
            textAreaString = textAreaString + "2";
        }
        if (GUI.Button(new Rect(320, 235, 50, 50), "3"))
        {
            if (textAreaString == "0")
            {
                textAreaString = "";
            }
            textAreaString = textAreaString + "3";
        }
        if (GUI.Button(new Rect(375, 235, 50, 50), "-"))
        {
            if (mode == 0)
            {
                preResult = textAreaString;
                textAreaString = textAreaString + "-";
                mode = 2;
            }
            else
                Cal(mode, 2, textAreaString);
        }
        if (GUI.Button(new Rect(210, 290, 105, 50), "0"))
        {
            if (textAreaString == "0")
            {
                textAreaString = "";
            }
            textAreaString = textAreaString + "0";
        }
        if (GUI.Button(new Rect(320, 290, 105, 50), "="))
        {
            Cal(mode, 0, textAreaString);//计算
        }
    }
}
