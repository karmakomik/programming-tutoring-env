using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragBlockScript : EventTrigger
{
    Vector2 clickDisplacement;
    Vector2 lastClickPos;
    bool isCollide = false;
    GameObject collidedObj = null;
    float blockHeight;
    float intraBlockBuffer = 15;
    Transform rootParentTrans;

    void Start()
    {
        //Renderer currMeshRend = GetComponent<Renderer>();
        RectTransform rectTrans = GetComponent<RectTransform>();
        blockHeight = rectTrans.rect.height;
        rootParentTrans = transform.parent;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("On trigger enter : " + other.transform.name);
        isCollide = true;
        collidedObj = other.transform.gameObject;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        isCollide = false;
        collidedObj = null;
    }

    public override void OnBeginDrag(PointerEventData data)
    {
        //Debug.Log("OnBeginDrag called.");
        //Debug.Log("transform position : " + transform.position);
        //Debug.Log("position : " + data.position);
        //Debug.Log("pressPosition : " + data.pressPosition);
        //clickDisplacement = new Vector2(transform.position.x - data.position.x, transform.position.y - data.position.y);
        //Debug.Log("clickDisplacement : " + clickDisplacement);
        //clickDisplacement = new Vector3(data.pressPosition.x - data.position.x, data.pressPosition.y - data.position.y);
        //clickDisplacement = data.pressPosition;
        transform.position = data.position + clickDisplacement;// - data.pressPosition;
        //isBlockFinishDragging = true;
        transform.parent = rootParentTrans;
    }

    /*public override void OnCancel(BaseEventData data)
    {
        Debug.Log("OnCancel called.");
    }

    public override void OnDeselect(BaseEventData data)
    {
        Debug.Log("OnDeselect called.");
    }*/

    public override void OnDrag(PointerEventData data)
    {
        //Debug.Log("OnDrag called.");
        transform.position = data.position + clickDisplacement;
        //isBlockBeingDragged = true;
    }
    
    public override void OnDrop(PointerEventData data)
    {
       //Debug.Log("OnDrop called.");
    }
    
    public override void OnEndDrag(PointerEventData data)
    {
        //Debug.Log("OnEndDrag called.");

        if (isCollide)
        {
            transform.SetParent(collidedObj.transform);
            transform.position = collidedObj.transform.position + new Vector3(0,-blockHeight/2 - intraBlockBuffer, 0);
        }

    }

    /*public override void OnInitializePotentialDrag(PointerEventData data)
    {
        Debug.Log("OnInitializePotentialDrag called.");
    }

    public override void OnMove(AxisEventData data)
    {
        Debug.Log("OnMove called.");
    }

    public override void OnPointerClick(PointerEventData data)
    {
        Debug.Log("OnPointerClick called.");
    }
    */
    public override void OnPointerDown(PointerEventData data)
    {
        //Debug.Log("OnPointerDown called.");
        lastClickPos = data.position;
        clickDisplacement = new Vector2(transform.position.x - data.position.x, transform.position.y - data.position.y);
        // Debug.Log("clickDisplacement : " + clickDisplacement);
    }

    public override void OnPointerEnter(PointerEventData data)
    {
       // Debug.Log("OnPointerEnter called.");
    }

    public override void OnPointerExit(PointerEventData data)
    {
      //  Debug.Log("OnPointerExit called.");
    }

    public override void OnPointerUp(PointerEventData data)
    {
        //Debug.Log("OnPointerUp called.");
        //isBlockBeingDragged = false;
    }

    /*
    public override void OnScroll(PointerEventData data)
    {
        Debug.Log("OnScroll called.");
    }

    public override void OnSelect(BaseEventData data)
    {
        Debug.Log("OnSelect called.");
    }

    public override void OnSubmit(BaseEventData data)
    {
        Debug.Log("OnSubmit called.");
    }

    public override void OnUpdateSelected(BaseEventData data)
    {
        Debug.Log("OnUpdateSelected called.");
    }*/
}
