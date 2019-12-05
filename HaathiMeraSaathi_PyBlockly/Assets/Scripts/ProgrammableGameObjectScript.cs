using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgrammableGameObjectScript : MonoBehaviour
{
    List<string> commList = new List<string>();
    bool moveToNextCommand = false;
    public GameObject rotor;
    bool isExecute = false;
    Vector3 haathiPos;

    // Start is called before the first frame update
    void Start()
    {
        haathiPos = new Vector3();

    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = haathiPos;

        if (isExecute)
        {
            executeCommands();
        }

        rotor.transform.Rotate(0, Time.deltaTime * 1000, 0);
    }

    public void startExecution()
    {
        isExecute = true;
        Debug.Log("Commands given -  ");
        moveToNextCommand = true;
        //bool areAllCommandsProcessed = false;
        foreach (string s in commList)
            Debug.Log(s);

        //Debug.Log("Starting execution - ");

    }

    public void executeCommands()
    {
        if (commList.Count > 0)
        {
            string currComm = commList[0];

            //Debug.Log("Currently processing command - " + currComm);
            //Debug.Log("moveToNextCommand - " + moveToNextCommand);
            if (moveToNextCommand)
            {
            }
        }
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

    public void moveForward(int units)
    {
        //haathiPos = transform.localPosition + transform.TransformDirection(haathiForwardFactVec);
    }

    public void moveVertically(int units)
    {
        //haathiForwardFactVec.Set(0, units, 0);
        //haathiPos = transform.localPosition + transform.TransformDirection(haathiForwardFactVec);
    }

    public void turnRight()
    {
        rotate(90);
    }

    public void turnLeft()
    {
        rotate(-90);
    }

    public void rotate(float units)
    {
        transform.Rotate(0, units, 0);
    }

}
