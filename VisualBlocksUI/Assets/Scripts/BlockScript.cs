using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class BlockScript : MonoBehaviour//, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler//, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public void OnEnable()
    {
        ClickController.OnClick += ObjectClicked;
        ClickController.OnMouseUp += ObjectReleased;
    }

    public void OnDisable()
    {
        ClickController.OnClick -= ObjectClicked;
        ClickController.OnMouseUp -= ObjectReleased;
    }


    private void ObjectClicked(RaycastHit hitObj)
    {
        if (hitObj.transform == transform)
        {
            Debug.Log("Object hit!");
        }
        //throw new NotImplementedException();
    }

    private void ObjectReleased(RaycastHit hitObj)
    {
        if (hitObj.transform == transform)
        {
            Debug.Log("Object released!");
        }
        //throw new NotImplementedException();
    }    

    /*public void OnPointerClick(PointerEventData eventData)
{
   Debug.Log("Click!");
   //throw new NotImplementedException();
}

public void OnPointerDown(PointerEventData eventData)
{
   Debug.Log("Click!");
   //throw new NotImplementedException();
}

public void OnPointerUp(PointerEventData eventData)
{
   //throw new NotImplementedException();
}*/



    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
