using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CodePanelScript : MonoBehaviour, IDropHandler
{
    public bool isPointerInsidePanel = true;
    public void OnDrop(PointerEventData data)
    {
        Debug.Log("Dropped object in CodePanel was: " + data.pointerDrag);
        /*if (data.pointerDrag != null && !data.pointerDrag.tag.Equals("Untagged"))
        {
            Debug.Log("Dropped object in CodePanel was: " + data.pointerDrag);
            data.pointerDrag.transform.SetParent(transform);
            data.pointerDrag.GetComponent<DragBlockScript>().rootParentTrans = transform; //When objects are undocked from their parent object they need to be reattached to the root (the codepanel transform)
        }*/

    }

    public Transform getTransform()
    {
        return transform;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("The cursor exited the selectable UI element.");
        //isPointerInsidePanel = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("The cursor exited the selectable UI element.");
       // isPointerInsidePanel = true;
    }
}
