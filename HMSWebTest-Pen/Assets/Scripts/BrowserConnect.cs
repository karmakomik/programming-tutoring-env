using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BrowserConnect : MonoBehaviour 
{
    public static List<string> commandsList = new List<string>();
    public GameObject _haathi;
	// Use this for initialization
	void Start ()
    {
        #if !UNITY_EDITOR && UNITY_WEBGL
            //WebGLInput.captureAllKeyboardInput = false;
        #endif
        //_haathi = GameObject.Find("haathi");
    }
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    void addCommand(string command)
    {
        commandsList.Add(command);
        Debug.Log("Unity : " + command);
    }

    void setCodeEditorVisibility(bool val)
    {
        //Application.ExternalCall("doneEval"); //Message to blocklyInject.js
        Application.ExternalCall("setVisibility", ((val==true)?1:2)); //Message to blocklyInject.js
    }

    void evaluateBlocklyCode()
    {
        Application.ExternalCall("evalCode"); //Message to blocklyInject.js
    }

    void startExecution() //Message from blocklyInject.js
    {
        Debug.Log("start execution");
        _haathi.SendMessage("startExecution"); //Message for haathi object
        //_haathi.SendMessage("setHaathiProgrammingFlag", false); //Message for haathi object
    }
}
