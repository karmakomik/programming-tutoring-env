using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class PyProgrammableObjectScript : MonoBehaviour
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

    // Use this for initialization
    void Start()
    {
        //commList = new List<string>();
        haathiPos = new Vector3(0, 0.08f, 4.58f);

        //commList.Add("wait 1");
        commList.Add("setPenColor #ff0000");
        commList.Add("penDown");
        commList.Add("move 300");
        commList.Add("rotate 90");
        commList.Add("penDown");
        commList.Add("move 300");

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
        Debug.Log("Command received - " + command);
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

    public void executeCommands()
    {
        if (commList.Count > 0)
        {
            string currComm = commList[0];

            //Debug.Log("Currently processing command - " + currComm);
            //Debug.Log("moveToNextCommand - " + moveToNextCommand);
            if (moveToNextCommand)
            {
                if (currComm.StartsWith("move"))
                {
                    float dist = 0;
                    if (float.TryParse(currComm.Split(' ')[1], out dist)) { }
                    //_pen.SendMessage("markControlObjPoint");
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
                    //_pen.SendMessage("setPenDownStatus", true);
                    commList.RemoveAt(0);
                }
                else if (currComm.Equals("penUp"))
                {
                    //_pen.SendMessage("setPenDownStatus", false);
                    commList.RemoveAt(0);
                }
                else if (currComm.StartsWith("setPenColor"))
                {
                    string param = currComm.Split(' ')[1];
                    Color penColor;
                    ColorUtility.TryParseHtmlString(param, out penColor);
                    //_pen.SendMessage("setPenColor", penColor);
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

    public void move(float units)
    {
        //Debug.Log("Move");
        //StartCoroutine(_move(units));
        //transform.Translate(0, 0, units / 100);
        haathiForwardFactVec.Set(0, 0, units / 100);
        haathiPos = transform.localPosition + transform.TransformDirection(haathiForwardFactVec);
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
    }
}

