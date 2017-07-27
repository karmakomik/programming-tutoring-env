using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControllerScript : MonoBehaviour
{
    public GameObject codeCanvas;
    public GameObject UICanvas;

    // Use this for initialization
    void Start ()
    {
        codeCanvas.SetActive(false);
        UICanvas.SetActive(true);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            //Must add prompt
            Application.Quit();
        }
    }

    public void setCodeCanvasVisibility(bool status)
    {
        codeCanvas.SetActive(status);
        UICanvas.SetActive(!status);
    }
}
