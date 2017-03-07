using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Text.RegularExpressions;


public class Commands : MonoBehaviour 
{
    Text text;
    Regex whileRegex = new Regex(@"^while\\((?:\\d*\\.)?\\d+\\)$");
    List<string> hmsAPIList = new List<string>();
    public GameObject haathiObj;

    bool isCurrPformAndroid = false;

    public Texture turnlefttexture;
    public Texture goforwardtexture;
    public Texture turnrighttexture;
    public Texture eattexture;

    int buttonSize = 190;
    int buttonGap = 30;
    
	// Use this for initialization
    public GameObject textgameobject;
    Text commTxt;
	void Start () 
	{
        if (Application.platform == RuntimePlatform.Android)
        {
            isCurrPformAndroid = true;
        }
        //gt = GetComponent<GUIText>();
        commTxt = GameObject.Find("Canvas").GetComponentsInChildren<Text>()[0];
        hmsAPIList.Add("goforward();");
        hmsAPIList.Add("turnleft();");
        hmsAPIList.Add("turnright();");
        hmsAPIList.Add("eat();");
	}


    void OnGUI()
    {
        GUI.backgroundColor = Color.clear;
        //TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);

        if (isCurrPformAndroid)
        {
            if (GUI.Button(new Rect(10, Screen.height - buttonSize - 20, buttonSize, buttonSize), turnlefttexture))
            {
                haathiObj.SendMessage("addCommandToQueue", "turnleft();");
            }
            if (GUI.Button(new Rect(10, Screen.height - 2 * (buttonSize + buttonGap), buttonSize, buttonSize), eattexture))
            {
                haathiObj.SendMessage("addCommandToQueue", "eat();");
            }
            if (GUI.Button(new Rect(10 + buttonSize + buttonGap, Screen.height - buttonSize - 20, buttonSize, buttonSize), goforwardtexture))
            {
                haathiObj.SendMessage("addCommandToQueue", "goforward();");
            }
            if (GUI.Button(new Rect(10 + 2 * (buttonSize + buttonGap), Screen.height - buttonSize - 20, buttonSize, buttonSize), turnrighttexture))
            {
                haathiObj.SendMessage("addCommandToQueue", "turnright();");
            }
            /*if (GUI.Button(new Rect(10 + 300, Screen.height - 70, 80, 30), "eat()"))
            {
            }*/
        }
    }

	// Update is called once per frame
	void Update () 
	{
        foreach (char c in Input.inputString)
        {
            //Debug.Log(c);
            //text.text = "sdas";//+= c;
            if (c == "\b"[0]) //Backspace?
            {
                if (commTxt.text.Length > 0)
                {
                    commTxt.text = commTxt.text.Substring(0, commTxt.text.Length - 1);
                }
            }
            else //If not backsapce
            {
                if (c == "\n"[0] || c == "\r"[0]) // Enter?
                {
                    if (hmsAPIList.Contains(commTxt.text))
                    {
                        Debug.Log("Command : " + commTxt.text);
                        //commsExecList.Add(commTxt.text);
                        haathiObj.SendMessage("addCommandToQueue", commTxt.text);
                    }
                    else if (whileRegex.Match(commTxt.text).Success)
                    {
                        Debug.Log("While match!");
                    }


                    commTxt.text = "";
                }
                else
                {
                    commTxt.text += c;
                }
            }
            /*if (c == "\b"[0])
                if (gt.text.Length != 0)
                    gt.text = gt.text.Substring(0, gt.text.Length - 1);

                else
                    if (c == "\n"[0] || c == "\r"[0])
                        print("User entered his name: " + gt.text);
                    else
                        gt.text += c;*/
        }
	}
}
