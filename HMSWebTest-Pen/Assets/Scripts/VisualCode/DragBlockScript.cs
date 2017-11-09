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
    float totalLoopBlockHeight;
    //float intraBlockBuffer = 15;
    RectTransform rectTrans;
    public GameObject mainBlockIm;
    public GameObject loopMid;
    public GameObject loopBottom;
    RectTransform mainBlockImTransform;
    RectTransform loopMidRectTransform;
    RectTransform loopBottomRectTransform;
    public Transform rootParentTrans;
    Transform blockCategoryPanelTrans;
    Vector3 startPos;
    GameObject childBlockObj = null;
    GameObject nestedChildBlockObj = null;
    //Canvasca
    public bool isOriginBlock = false;
    public GameObject emptyBlock;
    bool isBeingDragged = false;
    bool collideWithLoopBottom = false;
    float blockGap;
    Vector3[] fourCornersTopRectArray;
    Vector3[] fourCornersBottomRectArray;
    float defaultHeightGapRepeatBlock;

    void Start()
    {
        fourCornersTopRectArray = new Vector3[4];
        fourCornersBottomRectArray = new Vector3[4];
        startPos = transform.position;
        rectTrans = GetComponent<RectTransform>();
        rectTrans.GetWorldCorners(fourCornersTopRectArray);
        blockHeight = Mathf.Abs(fourCornersTopRectArray[0].y - fourCornersTopRectArray[2].y);
        rootParentTrans = transform.parent.parent; // Since they exist now inside different block categories, the root has to match to the grandpa element i.e "RootPanel"
        blockCategoryPanelTrans = transform.parent; //Used to assign the clone of dragged blocks back to the original block category

        if (loopBottom != null)
        {
            mainBlockImTransform = mainBlockIm.GetComponent<RectTransform>();
            //loopMidRectTransform = loopMid.GetComponent<RectTransform>();
            loopBottomRectTransform = loopBottom.GetComponent<RectTransform>();
            mainBlockImTransform.GetWorldCorners(fourCornersTopRectArray);
            loopBottomRectTransform.GetWorldCorners(fourCornersBottomRectArray);
            defaultHeightGapRepeatBlock = Mathf.Abs(fourCornersTopRectArray[0].y - fourCornersBottomRectArray[1].y);
        }

        //gameObject.GetComponentInChildren<Image>().GetComponent<Shadow>().effectDistance = new Vector2(0, 0);
        setShadow(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collision with " + other);
        if (!other.name.Equals("loopBottom"))
        {
            if (!other.GetComponent<DragBlockScript>().isOriginBlock && !isOriginBlock && isBeingDragged) // && gameObject.layer != 9)
            {
                if (other.GetType() == typeof(BoxCollider2D))
                {
                    Debug.Log("Collision with boxcollider");

                }
                else if (other.GetType() == typeof(PolygonCollider2D))
                {
                    collideWithLoopBottom = true;
                    Debug.Log("Collision with polygoncollider");
                }

                //Debug.Log("On trigger enter : " + other.transform.name);
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
    }

    void OnTriggerStay2D (Collider2D other)
    {


    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!other.name.Equals("loopBottom"))
        {
            if (!other.GetComponent<DragBlockScript>().isOriginBlock && !isOriginBlock)
            {
                collideWithLoopBottom = false;
                isCollide = false;
                collidedObj = null;
                emptyBlock.SetActive(false);
                //Debug.Log("On trigger exit : " + other.transform.name);
            }
        }
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

        if (nestedChildBlockObj != null) //branch off inside loops if there is a nestedchildobject
        {
            nestedChildBlockObj.GetComponent<DragBlockScript>().setShadowOnThisAndChildren(status);
        }


        if (childBlockObj != null)
        {
            childBlockObj.GetComponent<DragBlockScript>().setShadowOnThisAndChildren(status);
        }
        else //Leaf node condition
        {
            return;                
        }
    }

    void toggleCollisionLayerOnChildren(bool status)
    {
        //setShadow(status);
        if (status)
        {
            gameObject.layer = 9; //UI_Collision_Ignore
        }
        else
        {
            gameObject.layer = 5; //UI
        }

        if (childBlockObj != null)
        {
            //childBlockObj.GetComponent<DragBlockScript>().setShadowOnThisAndChildren(status);
            childBlockObj.GetComponent<DragBlockScript>().toggleCollisionLayerOnChildren(status);
        }
        else //Leaf node condition
        {
            return;
        }
    }

    public override void OnBeginDrag(PointerEventData data)
    {
        isBeingDragged = true;

        //Debug.Log("OnBeginDrag called.");
        //Debug.Log("transform position : " + transform.position);
        //Debug.Log("position : " + data.position);
        //Debug.Log("pressPosition : " + data.pressPosition);
        //Debug.Log("clickDisplacement : " + clickDisplacement);
        float heightOfChildBlocks = getHeightOfChildBlocks(gameObject);
        if (transform.parent != rootParentTrans)
        {
            //Debug.Log("Current parent is - " + transform.parent);
            if (transform.parent.GetComponent<DragBlockScript>() != null)
            {
                if (transform.parent.GetComponent<DragBlockScript>().loopBottom != null) //immediate parent is a loop block
                {
                    if (transform.parent.GetComponent<DragBlockScript>().getNestedChildBlockObj() == gameObject)
                    {
                        Debug.Log("Shrinking loop to default state");
                        transform.parent.GetComponent<DragBlockScript>().elongateVertically((heightOfChildBlocks / 2) + (heightOfChildBlocks / 5.75f) - transform.parent.GetComponent<DragBlockScript>().defaultHeightGapRepeatBlock);
                    }
                }
                else
                {
                    Debug.Log("Shrinking loop");
                    transform.parent.GetComponent<DragBlockScript>().elongateVertically((heightOfChildBlocks / 2) + (heightOfChildBlocks / 5.75f));
                }
            }
        }

        transform.position = data.position + clickDisplacement;// - data.pressPosition;
        try
        {
            if (transform.parent.GetComponent<DragBlockScript>().getNestedChildBlockObj() == gameObject)
            {
                transform.parent.GetComponent<DragBlockScript>().setNestedChildBlockObj(null); //this is a problem as when a loop's immediate nested child object is removed, the child beneath will also be removed.
            }
            else
            { 
                transform.parent.GetComponent<DragBlockScript>().setChildBlockObj(null); //this is a problem as when a loop's immediate nested child object is removed, the child beneath will also be removed.
            }
        }
        catch (System.Exception e)
        {
            Debug.Log("Parent is root. No DragScript component attached.");
        }
        //isBlockFinishDragging = true;
        transform.SetParent(rootParentTrans);

        if (isOriginBlock)//if (transform.parent.name.Equals("CommandSelectionPanel"))
        {
            //Debug.Log("Parent is CommandSelectionPanel");
            GameObject clone = Instantiate(gameObject, blockCategoryPanelTrans); 
            clone.name = gameObject.name; //Instead of the clone name being by default the string gameObject.name + "(clone)"
            clone.transform.position = startPos;
            isOriginBlock = false;
            clone.GetComponent<DragBlockScript>().isOriginBlock = true;
            //clone.GetComponent<RectTransform>().sizeDelta = new Vector2(0.4f, 0.4f);
            //clone.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
            clone.transform.SetParent(blockCategoryPanelTrans);
        }
        transform.SetAsLastSibling();

        //gameObject.GetComponentInChildren<Image>().GetComponent<Shadow>().effectDistance = new Vector2(2.17f, -2.17f);
        setShadowOnThisAndChildren(true);
        if (childBlockObj != null)
        {
            GetComponent<DragBlockScript>().toggleCollisionLayerOnChildren(true);
        }

        //Debug.Log("Deepest child" + getDeepestChildBlockObj(gameObject).name);
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

    public void elongateVertically(float height)
    {
        Debug.Log("elongateVertically");
        if (gameObject.name == "repeatBlock")
        {
            loopBottomRectTransform.position += new Vector3(0, height, 0);
        }
        if (transform.parent != rootParentTrans)
        {
            transform.parent.GetComponent<DragBlockScript>().elongateVertically(height);
        }
        
    }

    public override void OnEndDrag(PointerEventData data)
    {
        //Debug.Log("OnEndDrag called.");

        //gameObject.GetComponentInChildren<Image>().GetComponent<Shadow>().effectDistance = new Vector2(0,0);
        setShadowOnThisAndChildren(false);
        if (childBlockObj != null)
        {
            GetComponent<DragBlockScript>().toggleCollisionLayerOnChildren(false);
        }
        if (isCollide && (gameObject.tag != "events") && !collidedObj.GetComponent<DragBlockScript>().isOriginBlock)
        {
            //Debug.Log("Collision + EndDrag");
            //Vector3[] fourCornersArray = new Vector3[4];
            rectTrans.GetWorldCorners(fourCornersTopRectArray);
            blockHeight = Mathf.Abs(fourCornersTopRectArray[0].y - fourCornersTopRectArray[2].y);
            blockGap = blockHeight / 5.75f;
            float heightOfChildBlocks = getHeightOfChildBlocks(gameObject);

            /*if (gameObject.name == "repeatBlock") //Calculate total height of default loop block
            {
                if (getChildBlockObj() == null)
                {
                    collidedObj.GetComponent<DragBlockScript>().mainBlockImTransform.GetWorldCorners(fourCornersTopRectArray);
                    collidedObj.GetComponent<DragBlockScript>().loopBottomRectTransform.GetWorldCorners(fourCornersBottomRectArray);
                    defaultHeightGapRepeatBlock = Mathf.Abs(fourCornersTopRectArray[0].y - fourCornersBottomRectArray[1].y);

                    totalLoopBlockHeight = 2 * Mathf.Abs(fourCornersTopRectArray[0].y - fourCornersTopRectArray[2].y) + defaultHeightGapRepeatBlock;
                }
            }*/
            /*if (collidedObj.name == "repeatBlock" && collidedObj.GetComponent<DragBlockScript>().getChildBlockObj() != null)
            {
                collidedObj.GetComponent<DragBlockScript>().mainBlockImTransform.GetWorldCorners(fourCornersTopRectArray);
                collidedObj.GetComponent<DragBlockScript>().loopBottomRectTransform.GetWorldCorners(fourCornersBottomRectArray);
                defaultHeightGapRepeatBlock = Mathf.Abs(fourCornersTopRectArray[0].y - fourCornersBottomRectArray[1].y);
            }*/

            if (collideWithLoopBottom && collidedObj.name == "repeatBlock")
            {
                //Debug.Log("Set block below loopend recttransform");
                transform.position = new Vector3(collidedObj.transform.position.x, collidedObj.GetComponent<DragBlockScript>().loopBottomRectTransform.position.y - blockHeight / 2 - blockGap, 0);
                transform.SetParent(collidedObj.transform);
                collidedObj.GetComponent<DragBlockScript>().setChildBlockObj(gameObject);
            }
            else if (collidedObj.transform.position.y >= transform.position.y) //Dragged block is below the center of the collided block
            {
                Debug.Log("Inserted below");
                GameObject deepestChild = getDeepestChildBlockObj(gameObject);
                if (collidedObj.name == "repeatBlock")
                {
                    Debug.Log("Inserted inside loop");
                    transform.position = collidedObj.transform.position + new Vector3(blockHeight / 3, -blockHeight / 2 - blockGap, 0);
                    if (collidedObj.GetComponent<DragBlockScript>().getChildBlockObj() == null) //Repeat block has no children thus far
                    {
                        //collidedObj.GetComponent<DragBlockScript>().loopEndRectTransform.localPosition += new Vector3(0, -1.9f * blockHeight, 0);
                        collidedObj.GetComponent<DragBlockScript>().elongateVertically((-heightOfChildBlocks / 2) - (heightOfChildBlocks / 5.75f) + collidedObj.GetComponent<DragBlockScript>().defaultHeightGapRepeatBlock);
                        Debug.Log("Offset magnitude - " + ((-heightOfChildBlocks / 2) - (heightOfChildBlocks / 5.75f) + collidedObj.GetComponent<DragBlockScript>().defaultHeightGapRepeatBlock));
                        //collidedObj.GetComponent<PolygonCollider2D>().offset -= new Vector2(0, 45);//new Vector2(0, (-heightOfChildBlocks / 2) - (heightOfChildBlocks / 5.75f) + collidedObj.GetComponent<DragBlockScript>().defaultHeightGapRepeatBlock);
                    }
                    else
                    {
                        //collidedObj.GetComponent<DragBlockScript>().loopEndRectTransform.localPosition += new Vector3(0, -blockHeight - blockHeight, 0);
                        collidedObj.GetComponent<DragBlockScript>().elongateVertically((-heightOfChildBlocks / 2) - (heightOfChildBlocks / 5.75f));
                    }
                    //collidedObj.GetComponent<DragBlockScript>().loopMidRectTransform.localScale = new Vector3();
                    collidedObj.GetComponent<DragBlockScript>().setNestedChildBlockObj(gameObject);
                    transform.SetParent(collidedObj.transform);
                }
                else
                {
                    //transform.position = collidedObj.transform.position + new Vector3(0, -blockHeight / 2 - blockHeight / 5.75f, 0);
                    transform.position = collidedObj.transform.position + new Vector3(0, -blockHeight / 2 - blockGap, 0);
                    transform.SetParent(collidedObj.transform);
                    transform.parent.GetComponent<DragBlockScript>().elongateVertically((-heightOfChildBlocks / 2) - (heightOfChildBlocks / 5.75f));
                    GameObject collidedObjChildBlock = collidedObj.GetComponent<DragBlockScript>().getChildBlockObj();
                    collidedObj.GetComponent<DragBlockScript>().setChildBlockObj(gameObject);
                    if (collidedObjChildBlock != null)
                    {
                        collidedObjChildBlock.transform.position = transform.position + new Vector3(0, (-heightOfChildBlocks / 2) - (heightOfChildBlocks / 5.75f), 0);
                        collidedObjChildBlock.transform.SetParent(deepestChild.transform);
                    }
                    deepestChild.GetComponent<DragBlockScript>().setChildBlockObj(collidedObjChildBlock);
                }

            }
            else //Dragged block is above the center of the collided block
            {
                Debug.Log("Inserted above");
                GameObject deepestChild = getDeepestChildBlockObj(gameObject);
                //Debug.Log("Deepest child " + deepestChild.name);

                if (collidedObj.transform.parent == rootParentTrans)
                {
                    transform.position = collidedObj.transform.position - new Vector3(0, -heightOfChildBlocks / 2 - heightOfChildBlocks / 5.75f, 0);
                    collidedObj.transform.SetParent(deepestChild.transform);
                    deepestChild.GetComponent<DragBlockScript>().setChildBlockObj(collidedObj);
                }
                else
                {
                    transform.position = collidedObj.transform.position;// + new Vector3(0, -blockHeight / 2 - blockHeight / 5.75f, 0);
                    collidedObj.transform.position = transform.position + new Vector3(0, (-heightOfChildBlocks / 2) - (heightOfChildBlocks / 5.75f), 0);
                    if (collidedObj.transform.parent.name == "repeatBlock")
                    {
                        collidedObj.transform.parent.GetComponent<DragBlockScript>().setNestedChildBlockObj(gameObject);
                        collidedObj.GetComponent<DragBlockScript>().elongateVertically((-heightOfChildBlocks / 2) - (heightOfChildBlocks / 5.75f));
                    }
                    else
                    {
                        collidedObj.transform.parent.GetComponent<DragBlockScript>().setChildBlockObj(gameObject);
                    }
                    transform.SetParent(collidedObj.transform.parent);
                    collidedObj.transform.SetParent(deepestChild.transform);
                    deepestChild.GetComponent<DragBlockScript>().setChildBlockObj(collidedObj);

                    //collidedObj.transform.SetParent(transform);
                }
            }
            

        }
        emptyBlock.SetActive(false);
        isBeingDragged = false;
    }

    public GameObject getDeepestChildBlockObj(GameObject obj) //Returns the deepest child by recursing through the hierarchy, returns current obj if no children exist
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
        /*if (obj != null)
            Debug.Log("Parent block (this) : " + transform.name + ", Attached child obj : " + obj.transform.name);
        else
            Debug.Log("Parent block (this) : " + transform + ", Attached child obj : null");*/

        childBlockObj = obj;
    }

    public GameObject getNestedChildBlockObj()
    {
        return nestedChildBlockObj;
        //Debug.Log("getBlockChild");
    }

    public void setNestedChildBlockObj(GameObject obj)
    { 
        nestedChildBlockObj = obj;
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
