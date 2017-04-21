using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickMonitorScript : MonoBehaviour
{
    public static GameObject currDraggedObj = null;
    private RaycastHit hitObj;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitObj))
            {
                Debug.Log("Object hit!" + hitObj.transform.name);
                //isMouseUp = false;
                
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
           
            currDraggedObj = null;
            //isMouseUp = true;
            /*if (isObjHit)
            {                
                OnMouseUp(hitObj);
            }*/
        }

    }
}
