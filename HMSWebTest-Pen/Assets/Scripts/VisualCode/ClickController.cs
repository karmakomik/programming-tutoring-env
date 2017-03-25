using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ClickController : MonoBehaviour
{
    public delegate void ClickAction(RaycastHit hitObject);
    public static event ClickAction OnClick;
    public static event ClickAction OnMouseUp;
    private RaycastHit hitObj;
    public static string draggedObjName = "";
    bool isObjHit = false;
    public static bool isMouseUp = true;

   // List<List<GameObject>> 

	// Use this for initialization
	void Start ()
    {
	    
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            GameObject obj = GameObject.Find("whenRun");
            GameObject child = obj.GetComponent<DragScript>().getChildBlockObj();
            Debug.Log("printing connected children");
            while (child != null)
            {
                Debug.Log(child.name);
                child = child.GetComponent<DragScript>().getChildBlockObj();
            }
            SceneManager.LoadScene(0);
        }

        //Debug.Log("Object hit!");
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitObj))
            {
                Debug.Log("Object hit!" + hitObj.transform.name);
                isMouseUp = false;
                draggedObjName = hitObj.transform.name;
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
            isMouseUp = true;
            /*if (isObjHit)
            {                
                OnMouseUp(hitObj);
            }*/
        }
    }

    public void clickPenCategory()
    {
        Debug.Log("clicked pen category!");
    }
}
