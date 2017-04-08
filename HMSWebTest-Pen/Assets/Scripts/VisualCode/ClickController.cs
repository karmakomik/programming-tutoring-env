﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClickController : MonoBehaviour
{
    public delegate void ClickAction(RaycastHit hitObject);
    public static event ClickAction OnClick;
    public static event ClickAction OnMouseUp;
    private RaycastHit hitObj;
    public static string draggedObjName = "";
    public static GameObject currDraggedObj = null;
    bool isObjHit = false;
    public static bool isMouseUp = true;
    public List<GameObject> listOfAllBlocks = new List<GameObject>(); //Make sure to add any new block type into this list in the editor
    public GameObject scriptAreaObject;
    
   // List<List<GameObject>> 

	// Use this for initialization
	void Start ()
    {
        //Make pen default category, make all other block objects invisible
        foreach (GameObject obj in listOfAllBlocks)
        {
            if (obj.tag.Equals("pen"))
            {
                obj.SetActive(true);
            }
            else
            {
                obj.SetActive(false);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            //List<GameObject> list = GameObject.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "Name");
            GameObject[] list = GameObject.FindGameObjectsWithTag("instantiatedEvents");
            //GameObject obj = GameObject.Find("whenRun");
            List<string> commLst = new List<string>();
            //Debug.Log("Command list length : " + list.Length);
            for (int i = 0; i < list.Length; i++)
            {
                GameObject child = list[i].GetComponent<DragScript>().getChildBlockObj();
                Debug.Log("printing connected children");
                while (child != null)
                {
                    Debug.Log(child.name);
                    if (child.name.Equals("sayBlock"))
                    {
                        string cm = "say ";
                        cm += child.GetComponent<DragScript>().inputField1.text;
                        Debug.Log("say param : " + cm);
                        commLst.Add(cm);
                    }
                    else if (child.name.Equals("sayForSecsBlock"))
                    {
                        string cm = "sayForTime ";
                        cm += child.GetComponent<DragScript>().inputField1.text + " " + child.GetComponent<DragScript>().inputField2.text;
                        Debug.Log("sayForTime param : " + cm);
                        commLst.Add(cm);
                    }
                    else if (child.name.Equals("moveBlock"))
                    {
                        string cm = "moveForward ";
                        cm += child.GetComponent<DragScript>().inputField1.text;
                        Debug.Log("moveForward param : " + cm);
                        commLst.Add(cm);
                    }
                    else if (child.name.Equals("rotateBlock"))
                    {
                        string cm = "turnClockwise ";
                        cm += child.GetComponent<DragScript>().inputField1.text;
                        Debug.Log("turnClockwise param : " + cm);
                        commLst.Add(cm);
                    }
                    else
                    {
                        commLst.Add(child.name);
                    }
                    
                    child = child.GetComponent<DragScript>().getChildBlockObj();
                }
            }
            scriptAreaObject.SendMessage("addScript", commLst);
            SceneManager.LoadScene(0);
        }

        //Debug.Log("Object hit!");
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitObj))
            {
                //Debug.Log("Object hit!" + hitObj.transform.name);
                isMouseUp = false;
                draggedObjName = hitObj.transform.name;
                currDraggedObj = hitObj.transform.gameObject;
                /*if (OnClick != null)
                {
                    isObjHit = true;                    
                    OnClick(hitObj);
                }*/
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            draggedObjName = "";
            currDraggedObj = null;
            isMouseUp = true;
            /*if (isObjHit)
            {                
                OnMouseUp(hitObj);
            }*/
        }
    }

    public void clickPenCategory(string category)
    {
        //Debug.Log("clicked category " + category);
        foreach (GameObject obj in listOfAllBlocks)
        {
            if (category == obj.tag)
            {
                obj.SetActive(true);
            }
            else
            {
                obj.SetActive(false);
            }
        }

    }
}
