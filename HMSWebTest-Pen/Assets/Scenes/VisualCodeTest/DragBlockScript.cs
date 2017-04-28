using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragBlockScript : EventTrigger
{
    Vector2 clickDisplacement;
    Vector2 lastClickPos;
    bool isCollide = false;
    GameObject collidedObj = null;
    float blockHeight;
    float intraBlockBuffer = 15;
    RectTransform rectTrans;
    public Transform rootParentTrans;
    Vector3 startPos;
    GameObject childBlockObj = null;
    //Canvasca
    public bool isOriginBlock = false;
    public GameObject emptyBlock;

    void Start()
    {
        startPos = transform.position;
        //Renderer currMeshRend = GetComponent<Renderer>();
        rectTrans = GetComponent<RectTransform>();
        //blockHeight = rectTrans.rect.height;
        Vector3[] fourCornersArray = new Vector3[4];
        rectTrans.GetWorldCorners(fourCornersArray);
        blockHeight = Mathf.Abs(fourCornersArray[0].y - fourCornersArray[2].y);
        rootParentTrans = transform.parent;

        //gameObject.GetComponentInChildren<Image>().GetComponent<Shadow>().effectDistance = new Vector2(0, 0);
        setShadow(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.GetComponent<DragBlockScript>().isOriginBlock && !isOriginBlock)
        {
            Debug.Log("On trigger enter : " + other.transform.name);
            isCollide = true;
            collidedObj = other.transform.gameObject;
            emptyBlock.SetActive(true);
            emptyBlock.transform.SetSiblingIndex(transform.parent.childCount - 2);
            /*if (other.transform.position.y >= transform.position.y)
            {
                emptyBlock.transform.position = other.transform.position + new Vector3(0, -blockHeight / 2.5f, 0);
            }
            else
            {

                emptyBlock.transform.position = other.transform.position - new Vector3(0, -blockHeight / 3f, 0);
            }*/
        }
    }

    void OnTriggerStay2D (Collider2D other)
    {


    }

    void OnTriggerExit2D(Collider2D other)
    {
        isCollide = false;
        collidedObj = null;
        emptyBlock.SetActive(false);
    }

    void setShadow(bool status)
    {
        if (status)
        {
            gameObject.GetComponentInChildren<Image>().GetComponent<Shadow>().effectDistance = new Vector2(2.17f, -2.17f);
        }
        else
        {
            gameObject.GetComponentInChildren<Image>().GetComponent<Shadow>().effectDistance = new Vector2(0, 0);
        }
    }

    void setShadowOnThisAndChildren(bool status)
    {
        setShadow(status);
        if (childBlockObj != null)
        {
            childBlockObj.GetComponent<DragBlockScript>().setShadowOnThisAndChildren(status);
        }
        else //Leaf node condition
        {
            return;                
        }
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
        try
        {
           transform.parent.GetComponent<DragBlockScript>().setChildBlockObj(null);
        }
        catch (System.Exception e)
        {
            Debug.Log("Root parent. No DragScript component attached.");
        }
        //isBlockFinishDragging = true;
        transform.SetParent(rootParentTrans);
        /*if (transform.parent.name.Equals("CommandSelectionPanel"))
        {
            //Debug.Log("CommandSelectionPanel");
            //transform.parent.parent.SetAsLastSibling();
            if (!transform.parent.GetComponent<CommandSelectionPanelScript>().isPointerInsidePanel)
            {
                transform.parent.parent.GetComponentInChildren<CodePanelScript>().getTransform().parent.SetAsLastSibling(); // The Drop functionality for the panel works only if it is the last sibling
            }
        }
        else if (transform.parent.name.Equals("CodePanel"))
        {
            //Debug.Log("CodePanel");
            //transform.parent.SetAsFirstSibling();
            if (!transform.parent.GetComponent<CodePanelScript>().isPointerInsidePanel)
            {
                transform.parent.parent.parent.GetComponentInChildren<CommandSelectionPanelScript>().getTransform().SetAsLastSibling(); // The Drop functionality for the panel works only if it is the last sibling
            }
        }*/
        if (isOriginBlock)//if (transform.parent.name.Equals("CommandSelectionPanel"))
        {
            //Debug.Log("Parent is CommandSelectionPanel");
            GameObject clone = Instantiate(gameObject, transform.parent); 
            clone.name = gameObject.name; //Instead of the clone name being by default the string gameObject.name + "(clone)"
            clone.transform.position = startPos;
            isOriginBlock = false;
            clone.GetComponent<DragBlockScript>().isOriginBlock = true;
            //clone.GetComponent<RectTransform>().sizeDelta = new Vector2(0.4f, 0.4f);
            //clone.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
            clone.transform.SetParent(transform.parent);
        }
        transform.SetAsLastSibling();

        //gameObject.GetComponentInChildren<Image>().GetComponent<Shadow>().effectDistance = new Vector2(2.17f, -2.17f);
        setShadowOnThisAndChildren(true);
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

        if (isCollide && !collidedObj.GetComponent<DragBlockScript>().isOriginBlock && !isOriginBlock)
        {
            if (collidedObj.transform.position.y >= transform.position.y)
            {
                emptyBlock.transform.position = collidedObj.transform.position + new Vector3(0, -blockHeight / 2.5f, 0);
            }
            else
            {

                emptyBlock.transform.position = collidedObj.transform.position - new Vector3(0, -blockHeight / 3f, 0);
            }
        }
    }
    
    public override void OnDrop(PointerEventData data)
    {
       //Debug.Log("OnDrop called.");
    }
    
    

    public override void OnEndDrag(PointerEventData data)
    {
        //Debug.Log("OnEndDrag called.");
        /*if (transform.parent.name.Equals("CommandSelectionPanel"))
        {
            if (transform.parent.GetComponent<CommandSelectionPanelScript>().isPointerInsidePanel) //If pointer is inside the command selection panel and the object is release inside the panel then it needs to be deleted
            {
                Debug.Log("Object destroyed");
                Destroy(gameObject);
                return; //To avoid execution of collision check below which would be triggered if there is collision with other objects inside the commandselectionpanel
            }
            //else
        }*/

        //gameObject.GetComponentInChildren<Image>().GetComponent<Shadow>().effectDistance = new Vector2(0,0);
        setShadowOnThisAndChildren(false);
        if (isCollide && (gameObject.tag != "events") && !collidedObj.GetComponent<DragBlockScript>().isOriginBlock)
        {
            //Debug.Log("Collision + EndDrag");
            Vector3[] fourCornersArray = new Vector3[4];
            rectTrans.GetWorldCorners(fourCornersArray);
            blockHeight = Mathf.Abs(fourCornersArray[0].y - fourCornersArray[2].y);
            if (collidedObj.transform.position.y >= transform.position.y) //Dragged block is below the center of the collided block
            {
                transform.position = collidedObj.transform.position + new Vector3(0, -blockHeight / 2 - blockHeight / 5.75f, 0);
                transform.SetParent(collidedObj.transform);
                GameObject collidedObjChildBlock = collidedObj.GetComponent<DragBlockScript>().getChildBlockObj();
                collidedObj.GetComponent<DragBlockScript>().setChildBlockObj(gameObject);
                if (collidedObjChildBlock != null)
                {
                    collidedObjChildBlock.transform.position = transform.position + new Vector3(0, (-getHeightOfChildBlocks(gameObject) / 2) - (getHeightOfChildBlocks(gameObject) / 5.75f), 0);
                    collidedObjChildBlock.transform.SetParent(transform);
                }
            }
            else //Dragged block is above the center of the collided block
            {
                GameObject deepestChild = getDeepestChildBlockObj(gameObject);
                Debug.Log("Deepest child " + deepestChild.name);

                if (collidedObj.transform.parent == rootParentTrans)
                {
                    transform.position = collidedObj.transform.position - new Vector3(0, -getHeightOfChildBlocks(gameObject) / 2 - getHeightOfChildBlocks(gameObject) / 5.75f, 0);
                    collidedObj.transform.SetParent(deepestChild.transform);
                    getDeepestChildBlockObj(gameObject).GetComponent<DragBlockScript>().setChildBlockObj(collidedObj);
                }
                else
                {
                    transform.position = collidedObj.transform.position;// + new Vector3(0, -blockHeight / 2 - blockHeight / 5.75f, 0);
                    collidedObj.transform.position = transform.position + new Vector3(0, (-getHeightOfChildBlocks(gameObject) / 2) - (getHeightOfChildBlocks(gameObject) / 5.75f), 0);
                    collidedObj.transform.parent.GetComponent<DragBlockScript>().setChildBlockObj(gameObject);
                    transform.SetParent(collidedObj.transform.parent);
                    collidedObj.transform.SetParent(deepestChild.transform);
                    getDeepestChildBlockObj(gameObject).GetComponent<DragBlockScript>().setChildBlockObj(collidedObj);

                    //collidedObj.transform.SetParent(transform);
                }
            }        

        }

    }

    public GameObject getDeepestChildBlockObj(GameObject obj)
    {
        GameObject deepestChild;
        if (getChildBlockObj() != null)
        {
            deepestChild = obj.GetComponent<DragBlockScript>().getDeepestChildBlockObj(getChildBlockObj());
        }
        else
        {
            return gameObject;
        }
        return deepestChild;
    }

    public GameObject getChildBlockObj()
    {
        return childBlockObj;
        //Debug.Log("getBlockChild");
    }

    public void setChildBlockObj(GameObject obj)
    {
        if (obj != null)
            Debug.Log("Parent block : " + transform.name + ", Attached child obj : " + obj.transform.name);
        else
            Debug.Log("Parent block : " + transform + ", Attached child obj : null");

        childBlockObj = obj;
    }

    //To calculate height of current block + all child blocks stacked underneath, we make recursive calls to getHeightOfBlocks() 
    private float getHeightOfChildBlocks(GameObject obj)
    {
        //Debug.Log("hit");
        float height = 0;
        GameObject currObjChild = obj.GetComponent<DragBlockScript>().getChildBlockObj();
        if (currObjChild != null)
        {
            height = obj.GetComponent<DragBlockScript>().blockHeight + getHeightOfChildBlocks(currObjChild);
        }
        else //leaf node
        {
            return obj.GetComponent<DragBlockScript>().blockHeight;
        }
        Debug.Log("Returned height" + height);
        return height;
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
