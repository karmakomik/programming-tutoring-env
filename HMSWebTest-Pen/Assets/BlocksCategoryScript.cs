using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocksCategoryScript : MonoBehaviour
{
    public List<GameObject> motionBlocks;
    public List<GameObject> eventBlocks;
    public List<GameObject> penBlocks;
    public List<GameObject> controlsBlocks;

    public GameObject motionPanel;
    public GameObject eventsPanel;
    public GameObject penPanel;
    public GameObject controlsPanel;

    // Use this for initialization
    void Start ()
    {
        

    }

    // Update is called once per frame
    /*void Update ()
    {
		
	}*/

    public void changeCategory(string text)
    {
        Debug.Log("category " + text + " selected");
        if (text.Equals("move"))
        {
            motionPanel.SetActive(true);
            eventsPanel.SetActive(false);
            penPanel.SetActive(false);
            controlsPanel.SetActive(false);
        }
        else if (text.Equals("controls"))
        {
            motionPanel.SetActive(false);
            eventsPanel.SetActive(false);
            penPanel.SetActive(false);
            controlsPanel.SetActive(true);
        }
        else if (text.Equals("pen"))
        {
            motionPanel.SetActive(false);
            eventsPanel.SetActive(false);
            penPanel.SetActive(true);
            controlsPanel.SetActive(false);
        }
        else if (text.Equals("events"))
        {
            motionPanel.SetActive(false);
            eventsPanel.SetActive(true);
            penPanel.SetActive(false);
            controlsPanel.SetActive(false);
        }
    }

}
