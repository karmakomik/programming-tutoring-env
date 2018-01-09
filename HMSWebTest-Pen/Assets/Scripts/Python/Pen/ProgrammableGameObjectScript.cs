using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using Cubiquity;

public class ProgrammableGameObjectScript : MonoBehaviour
{
    public GameObject codePanel;
    List<string> commList = new List<string>();
    bool moveToNextCommand = false;
    Thread timerThread;
    Vector3 haathiPos;
    //Quaternion haathiRot;
    bool isExecute = false;
    Vector3 haathiForwardFactVec = new Vector3(0, 0, 0);
    public GameObject _pen;
    public GameObject coloredCubesVolumeObj;
    ColoredCubesVolume coloredCubesVolume;
    Vector3i currActiveVoxel;
    public GameObject transparentCube;
    Color currBlockColor;

    // Use this for initialization
    void Start()
    {
        //commList = new List<string>();
        haathiPos = new Vector3(0, 0.08f, 4.58f);
        currBlockColor = Color.yellow;
        //commList.Add("wait 1");
        /*commList.Add("setPenColor #ff0000");
        commList.Add("penDown");
        commList.Add("move 300");
        commList.Add("rotate 90");
        commList.Add("penDown");
        commList.Add("move 300");*/

        if (coloredCubesVolumeObj != null)
        {
            coloredCubesVolume = coloredCubesVolumeObj.GetComponent<ColoredCubesVolume>();
        }
        //coloredCubesVolume.data.enclosingRegion.
        if (coloredCubesVolume != null)
        {
            Debug.Log("lower corner - " + coloredCubesVolume.data.enclosingRegion.lowerCorner);
            Debug.Log("upper corner - " + coloredCubesVolume.data.enclosingRegion.upperCorner);
            currActiveVoxel = getVoxelUnderneath();
            moveVertically(1); //Move elephant one position up
            transparentCube.transform.position = coloredCubesVolumeObj.transform.position + new Vector3(currActiveVoxel.x, currActiveVoxel.y + 1, currActiveVoxel.z);
            //coloredCubesVolume.data.SetVoxel(currActiveVoxel.x, currActiveVoxel.y, currActiveVoxel.z, (QuantizedColor)Color.yellow);
            //coloredCubesVolume.

            currActiveVoxel += new Vector3i(0, 1, 0);
        }

    }

    public void waitSecs(object arg)
    {
        float time = (float)arg;
        Thread.Sleep((int)time * 1000);
        //Thread.Sleep(3000);
        moveToNextCommand = true;
        //var watch = System.Diagnostics.Stopwatch.StartNew();
        // the code that you want to measure comes here
        //watch.Stop();
        //var elapsedMs = watch.ElapsedMilliseconds;
    }

    public void addCommandToPool(string command)
    {
        //Debug.Log("Command received - " + command);
        commList.Add(command);
    }

    public void clearCommandPool()
    {
        commList = new List<string>();
    }

    public void startExecution()
    {
        isExecute = true;
        codePanel.SetActive(false);
        //Debug.Log("Commands given -  ");
        moveToNextCommand = true;
        //bool areAllCommandsProcessed = false;
        /*foreach (string s in commList)
            Debug.Log(s);*/
        
        //Debug.Log("Starting execution - ");

    }

    int dir = 0; //0 = North, 1 = East, 2 = South, 3 = West

    public void executeCommands()
    {
        if (commList.Count > 0)
        {
            string currComm = commList[0];

            //Debug.Log("Currently processing command - " + currComm);
            //Debug.Log("moveToNextCommand - " + moveToNextCommand);
            if (moveToNextCommand)
            {
                if (currComm.StartsWith("moveForward"))
                {
                    switch (dir)
                    {
                        case 0: //NORTH
                            currActiveVoxel += new Vector3i(0, 0, 1);
                            break;
                        case 1: //EAST
                            currActiveVoxel += new Vector3i(1, 0, 0);
                            break;
                        case 2: //SOUTH
                            currActiveVoxel += new Vector3i(0, 0, -1);
                            break;
                        case 3: //WEST
                            currActiveVoxel += new Vector3i(-1, 0, 0);
                            break;
                    }
                    //currActiveVoxel += new Vector3i(xFactor, yFactor, zFactor);
                   
                    moveForward(1);
                    //transparentCube.transform.position = transform.position + new Vector3(0, -1, 0);
                    commList.RemoveAt(0);
                }
                else if (currComm.StartsWith("moveUp"))
                {
                    currActiveVoxel += new Vector3i(0, 1, 0);
                    moveVertically(1);
                    //transparentCube.transform.position = transform.position + new Vector3(0, -1, 0);
                    commList.RemoveAt(0);
                }
                else if (currComm.StartsWith("moveDown"))
                {
                    currActiveVoxel += new Vector3i(0, -1, 0);
                    moveVertically(-1);
                    //transparentCube.transform.position = coloredCubesVolumeObj.transform.position + new Vector3(currActiveVoxel.x, currActiveVoxel.y + 1, currActiveVoxel.z);
                    commList.RemoveAt(0);
                }
                else if (currComm.StartsWith("move"))
                {
                    float dist = 0;
                    if (float.TryParse(currComm.Split(' ')[1], out dist)) { }
                    if (_pen != null)
                    {
                        _pen.SendMessage("markControlObjPoint");
                    }
                    //currActiveVoxel = getVoxelUnderneath();
                    //coloredCubesVolume.data.SetVoxel(currActiveVoxel.x, currActiveVoxel.y, currActiveVoxel.z, (QuantizedColor)Color.yellow);
                    move(dist);
                    commList.RemoveAt(0);
                }
                else if (currComm.StartsWith("rotate"))
                {
                    float ang = 0;
                    if (float.TryParse(currComm.Split(' ')[1], out ang)) { }
                    rotate(ang);
                    commList.RemoveAt(0);
                }
                else if (currComm.StartsWith("turnLeft"))
                {
                    --dir;
                    if (dir < 0)
                    {
                        dir = 3;
                    }
                    turnLeft();
                    commList.RemoveAt(0);
                }
                else if (currComm.StartsWith("turnRight"))
                {
                    ++dir;
                    if (dir > 3)
                    {
                        dir = 0;
                    }
                    turnRight();
                    commList.RemoveAt(0);
                }
                else if (currComm.StartsWith("placeBlock"))
                {
                    placeBlock();
                    commList.RemoveAt(0);
                    //currActiveVoxel = new Vector3i(currActiveVoxel.x, currActiveVoxel.y, currActiveVoxel.z);
                }
                else if (currComm.StartsWith("wait"))
                {
                    moveToNextCommand = false;
                    float time = 0;
                    if (float.TryParse(currComm.Split(' ')[1], out time)) { }
                    //Debug.Log("Start wait");
                    //timerThread = new Thread(new ParameterizedThreadStart(waitSecs));
                    //timerThread.Start(time);
                    wait(time);
                    //Debug.Log("Call to wait done");
                    commList.RemoveAt(0);
                }
                else if (currComm.Equals("penDown"))
                {
                    if (_pen != null)
                    {
                        _pen.SendMessage("setPenDownStatus", true);
                    }
                    commList.RemoveAt(0);
                }
                else if (currComm.Equals("penUp"))
                {
                    if (_pen != null)
                    {
                        _pen.SendMessage("setPenDownStatus", false);
                    }
                    commList.RemoveAt(0);
                }
                else if (currComm.StartsWith("setBlockColor"))
                {
                    string param = currComm.Split(' ')[1];                    
                    ColorUtility.TryParseHtmlString(param, out currBlockColor);
                    commList.RemoveAt(0);
                }
                else if (currComm.StartsWith("setPenColor"))
                {
                    string param = currComm.Split(' ')[1];
                    Color penColor;
                    ColorUtility.TryParseHtmlString(param, out penColor);
                    if (_pen != null)
                    {
                        _pen.SendMessage("setPenColor", penColor);
                    }
                    commList.RemoveAt(0);
                }
                else
                {
                    Debug.Log("Unprocesssed - " + currComm);
                    commList.RemoveAt(0);
                }
                /*else if ()
                {

                }
                else if ()
                {

                }
                else if ()
                {

                }
                else if ()
                {

                }
                else if ()
                {

                }*/
            }
        }
        else
        {
            isExecute = false;
            codePanel.SetActive(true);
        }
    }

    public Vector3i getVoxelUnderneath()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        PickVoxelResult pickResult;
        bool hit = Picking.PickFirstSolidVoxel(coloredCubesVolume, ray, 100.0f, out pickResult);
        Vector3i hitVoxelAddress = new Vector3i();
        // If we hit a solid voxel then create an explosion at this point.
        if (hit)
        {
            hitVoxelAddress = new Vector3i(pickResult.volumeSpacePos.x, pickResult.volumeSpacePos.y, pickResult.volumeSpacePos.z);
            Debug.Log("hit voxel = " + hitVoxelAddress);
        }
        return hitVoxelAddress;
    }

    public void move(float units)
    {
        //Debug.Log("Move");
        //StartCoroutine(_move(units));
        //transform.Translate(0, 0, units / 100);
        haathiForwardFactVec.Set(0, 0, units / 100);
        haathiPos = transform.localPosition + transform.TransformDirection(haathiForwardFactVec);
    }

    public void moveForward(int units)
    {
        haathiForwardFactVec.Set(0, 0, units);
        haathiPos = transform.localPosition + transform.TransformDirection(haathiForwardFactVec);
    }

    public void moveVertically(int units)
    {
        haathiForwardFactVec.Set(0, units, 0);
        haathiPos = transform.localPosition + transform.TransformDirection(haathiForwardFactVec);
    }

    public void turnRight()
    {
        rotate(90);
    }

    public void turnLeft()
    {
        rotate(-90);
    }

    public void placeBlock()
    {
        //currActiveVoxel = getVoxelUnderneath();
        coloredCubesVolume.data.SetVoxel(currActiveVoxel.x, currActiveVoxel.y, currActiveVoxel.z, (QuantizedColor)currBlockColor);
        //moveForward(1);
    }

    private IEnumerator _wait(float units)
    {
        //objectPaused = true;
        //Debug.Log("Start wait");
        yield return new WaitForSeconds(units);
        //Debug.Log("End wait");
        moveToNextCommand = true;
    }

    public void rotate(float units)
    {
        transform.Rotate(0, units, 0);
    }

    public void wait(float time)
    {
        StartCoroutine(_wait(time));
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = haathiPos;

        if (isExecute)
        {
            executeCommands();
        }

       /* if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            Debug.Log("Up pressed");
            for (int i = 0; i < 30; i++)
            {
                coloredCubesVolume.data.SetVoxel(0, i, 0, (QuantizedColor)Color.green);
            }

            //coloredCubesVolume.ForceUpdate();
        }*/
    }
}

