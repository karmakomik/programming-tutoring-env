using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HaathiScript : MonoBehaviour 
{
    List<string> commsExecList = new List<string>(); //Commands to be executed
    public float smoothFactor = 2; //smoothing factor for the elephant's movement
	// Use this for initialization

	void Start () 
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
	}
    
    void turnRightOps()
    {
        transform.Rotate(new Vector3(0, 1, 0), 90);
    }

    void turnLeftOps()
    {
        transform.Rotate(new Vector3(0, 1, 0), -90);
    }

    void goForwardOps()
    {
        transform.Translate(0, 0.0f, 3.0f);
    }

    void eatOps()
    {
        
    }
	// Update is called once per frame
	void Update () 
    {
        if(commsExecList.Count > 0)
        {
            if (commsExecList[0].Equals("goforward();"))
            {
                goForwardOps();
                //Vector3 targetPosition = ;
                //transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothFactor);
            }
            else if (commsExecList[0].Equals("turnleft();"))
            {
                turnLeftOps();
            }
            else if (commsExecList[0].Equals("turnright();"))
            {
                turnRightOps();
            }
            else if (commsExecList[0].Equals("eat();"))
            {
                eatOps();
            }
            commsExecList.RemoveAt(0);
        }
	}

    void addCommandToQueue(string comm)
    {
        commsExecList.Add(comm);
    }
}
