using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CommandSelectionPanelScript : MonoBehaviour, IDropHandler, IPointerExitHandler, IPointerEnterHandler
{
    public bool isPointerInsidePanel = true;
    public void OnDrop(PointerEventData data)
    {
        if (data.pointerDrag != null && !data.pointerDrag.tag.Equals("Untagged"))
        {
            Debug.Log("Dropped object in CommandSelectionPanel was: " + data.pointerDrag);
            //data.pointerDrag.transform.SetParent(transform);
            Destroy(data.pointerDrag); //When objects are dropped inside the commandselectionpanel from the codepanel, they need to be deleted
        }
    }

    public Transform getTransform()
    {
        return transform;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("The cursor exited the selectable UI element.");
        isPointerInsidePanel = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("The cursor exited the selectable UI element.");
        isPointerInsidePanel = true;
    }

}