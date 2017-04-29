using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class DeleteBlocksScript : MonoBehaviour, IDropHandler, IPointerExitHandler, IPointerEnterHandler
{
    Image currImage;
    public void OnDrop(PointerEventData data)
    {
        if (data.pointerDrag != null)
        {
            Debug.Log("Dropped object in CodePanel was: " + data.pointerDrag);
            Destroy(data.pointerDrag);
        }
    }
        // Use this for initialization
    void Start()
    {
        currImage = gameObject.GetComponent<Image>();

    }
	
	// Update is called once per frame
	void Update()
    {
		
	}

    public void OnPointerExit(PointerEventData eventData)
    {
        currImage.color = Color.white;
        //Debug.Log("The cursor exited the selectable UI element.");
        //isPointerInsidePanel = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        currImage.color = new Color(255/255f,186/255f,186/255f);
        //Debug.Log("The cursor exited the selectable UI element.");
        //isPointerInsidePanel = true;
    }
}
