﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DragScript : MonoBehaviour
{
    float x, y, z;
    Vector3 point;
    float distance = 5f;
    public float blockHeight;
    bool isCollide = false;
    bool isObjUndocked = true;
    Transform collidedObjTransform;
    AudioSource clickAudioSrc;
    //int childBlockNum = 0;
    GameObject childBlockObj = null;
    public bool flag1 = false;
    Mesh currMesh;
    Vector3[] currMeshVerts;
    bool isContainElongPt = false;
    Vector3 elongPt;
    private float intraRepeatBlockDistance = 0.015f; // Hardcoded based on 3D design 

    float mouseClickOffsetX, mouseClickOffsetY;

    // Use this for initialization
    void Start()
    {
        z = 4.27f;// (Camera.main.transform.position - gameObject.transform.position).magnitude;
        Renderer currMeshRend = GetComponent<Renderer>();
        blockHeight = 0.83f * currMeshRend.bounds.size.y;
        Debug.Log("Height of " + transform.name + " is " + blockHeight);
        //clickAudioSrc = GameObject.FindObjectOfType<AudioSource>();
        //clickAudioSrc.playOnAwake = false;

        Transform[] childObjs = GetComponentsInChildren<Transform>();
        foreach (Transform t in childObjs)
        {
            if (t.name.Equals("elongationRefPt"))
            {
                //Debug.Log("" + transform.name + " contains elongationRefPt");
                isContainElongPt = true;
                elongPt = t.position;
                Debug.Log("elongPt - " + elongPt);
                currMesh = GetComponent<MeshFilter>().mesh;
                currMeshVerts = currMesh.vertices;               
            }
        }
    }

    public void elongateVertically(float height, float gap)
    {
        if (transform.GetComponent<DragScript>().isContainElongPt)
        {
            Debug.Log("Height of incoming blocks - " + height);
            int i = 0;
            int count = 0;
            //foreach (Vector3 v in currMeshVerts)
            while (i < currMeshVerts.Length)
            {
                //Debug.Log(transform.TransformPoint(v));
                if (transform.TransformPoint(currMeshVerts[i]).y < elongPt.y)
                {
                    ++count;
                    currMeshVerts[i].y = currMeshVerts[i].y - height / 3f + gap; //Divided by the scaling factor plus the distance between the top and bottom halves of repeat block
                }
                ++i;
            }
            Debug.Log("Total vertices - " + currMesh.vertices.Length);
            Debug.Log("Vertices below elongation pt - " + count);
            currMesh.vertices = currMeshVerts;
            currMesh.RecalculateBounds();
        }

        //exit condition
        if (transform.parent == null)
        {
            Debug.Log("parent null");
            return;
        }
        else
        {
            Debug.Log("parent exists");
            //trans = trans.parent;
            transform.parent.GetComponent<DragScript>().elongateVertically(height, gap);
        }

    }

    public void OnMouseDrag()
    {
        if (isObjUndocked)//if (!isCollide)
        {
            x = Input.mousePosition.x;
            y = Input.mousePosition.y;

            //Plane plane = new Plane(Vector3.forward, new Vector3(x, y, distance));

            distance = Camera.main.WorldToScreenPoint(gameObject.transform.position).z; //distance from camera to gameobject


            point = Camera.main.ScreenToWorldPoint(new Vector3(x + mouseClickOffsetX, y + mouseClickOffsetY, distance));
            gameObject.transform.position = point;

            //Debug.Log("x: " + point.x + "   y: " + point.y + "   z: " + point.z);
        }
        else
        {
            //if (transform.parent.GetComponent<DragScript>().isContainElongPt) //Is parent block a forever type block
            //{
                transform.parent.GetComponent<DragScript>().elongateVertically(-getHeightOfBlocks(gameObject), -intraRepeatBlockDistance); 
                //Next step : Cascade this up the chain until root node is reached
            //}
            transform.parent.GetComponent<DragScript>().setChildBlockObj(null);
            transform.SetParent(null);

            
            isObjUndocked = true;
            //isObjUndocked
        }
    }

    public void OnMouseDown() 
    {
        //Move object using offset which is diff b/w mouse click pos and transform pos
        mouseClickOffsetX = Camera.main.WorldToScreenPoint(gameObject.transform.position).x - Input.mousePosition.x;
        mouseClickOffsetY = Camera.main.WorldToScreenPoint(gameObject.transform.position).y - Input.mousePosition.y;
    }

    public GameObject getChildBlockObj()
    {
        return childBlockObj;
        //Debug.Log("getBlockChild");
    }

    public void setChildBlockObj(GameObject obj)
    {
        childBlockObj = obj;
    }

    public void insertBlocks()
    {
        if (collidedObjTransform.GetComponent<DragScript>().isContainElongPt) //checks if block is horizontally expandable, must also check if the collided obj is forever or if-else
        {
            collidedObjTransform.GetComponent<DragScript>().elongateVertically(getHeightOfBlocks(gameObject), intraRepeatBlockDistance);
  
            transform.position = new Vector3(collidedObjTransform.position.x + 0.22f, collidedObjTransform.GetComponent<Renderer>().bounds.max.y - blockHeight - 0.11f, collidedObjTransform.position.z);

        }
        else
        {
            transform.position = new Vector3(collidedObjTransform.position.x, collidedObjTransform.position.y - blockHeight, collidedObjTransform.position.z);

        }    
    }



    public void OnMouseUp()
    {
        if (isCollide) //Is this block colliding with another?
        {
            //Debug.Log("Current obj - " + transform.name);
            //clickAudioSrc.Play(); //play click sound
            isObjUndocked = false; // Mouse up while blocks are in collision => object has snapped

            //Debug.Log("Number of children - " + collidedObjTransform.childCount);
            GameObject collidedObjChildBlock = collidedObjTransform.GetComponent<DragScript>().getChildBlockObj();

            insertBlocks();            

            //Debug.Log("block height - " + blockHeight);


            if (collidedObjChildBlock != null)  //Check if parent collider has children. If so cascade child objects below the current one
            {
                Debug.Log("Inside!");
                //Transform childObj = collidedObjTransform.GetChild(0);
                collidedObjChildBlock.transform.SetParent(transform);//childObj.SetParent(transform); //Set this transform as parent to the previous child of the block this block is being docked into



                //childObj.position = new Vector3(collidedObjTransform.position.x, collidedObjTransform.position.y - blockHeight * 2, collidedObjTransform.position.z);
                //childObj.localPosition = Vector3.zero;
                collidedObjChildBlock.transform.position = new Vector3(collidedObjTransform.position.x, collidedObjTransform.position.y - blockHeight * 2, collidedObjTransform.position.z);

            }
            transform.SetParent(collidedObjTransform);
            transform.parent.GetComponent<DragScript>().setChildBlockObj(gameObject);
            transform.SetAsFirstSibling();

        }
        isCollide = false;        
    }

    //To calculate height of current block + all child blocks stacked underneath, we make recursive calls to getHeightOfBlocks() 
    private float getHeightOfBlocks(GameObject obj)
    {
        //Debug.Log("hit");
        float height = 0;
        GameObject currObjChild = obj.GetComponent<DragScript>().getChildBlockObj();
        if (currObjChild != null)
        {
            height = obj.GetComponent<DragScript>().blockHeight + getHeightOfBlocks(currObjChild);
        }
        else //leaf node
        {
            return obj.GetComponent<DragScript>().blockHeight;
        }
        //Debug.Log("Returned height" + height);
        return height;
    }



    void OnTriggerEnter(Collider other)
    {
        
        //Debug.Log(ClickController.draggedObjName);
        if (isThisObjBeingDragged()) //To filter out collision trigger event in parent block onto which this child block is being snapped into
        {
            collidedObjTransform = other.transform;
            isCollide = true;
            Debug.Log("Drag collision!" + transform.name);

            Debug.Log("Collision with " + other.transform.name);
        }
    }

    void OnTriggerExit(Collider other)
    {
        //isObjUndocked = true;
        isCollide = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            if (flag1)
            {
                float heightOfInsertedBlocks = getHeightOfBlocks(gameObject);
                Debug.Log("Height of inserted blocks" + heightOfInsertedBlocks);
            }
        }

        // Debug.Log("z : " + z );
        //transform.rotation = Camera.main.transform.localRotation;
    }

    private bool isThisObjBeingDragged()
    {
        if (transform.name.Equals(ClickController.draggedObjName))
        {
            return true;
        }
        return false;
    }
}
