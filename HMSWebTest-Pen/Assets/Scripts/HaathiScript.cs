using UnityEngine;
using System.Collections;
using UnityEngine.UI; 

public class HaathiScript : MonoBehaviour 
{
   // public enum haathiState {haathiForward};

    bool isExecute = false;
    float lerpStepFactor = 0.0f;
    bool initCalcToggle = false;
    Vector3 targetPos;
    Quaternion targOrient;
    Vector3 haathiForwardFactVec = new Vector3(0, 0, 2.5f);
    bool isBananaHit = false;
    GameObject hitBananaObj;
    int gameScore = 0;
    //public GameObject _canvas;
    public GameObject _browserConnect;
    public GameObject _pen;
    public GameObject _player;
    public GameObject _thinkBox;
    public GameObject _speechBox;
    public Canvas _haathiCanvas;
    bool isSpeechBubbleVisible = true;
	// Use this for initialization
	void Start()
    {
        isExecute = false;
	}
	
	// Update is called once per frame
	void Update() 
    {
        //Debug UI
        #if UNITY_EDITOR

        #endif

        if (isSpeechBubbleVisible)
        {
           // _haathiCanvas.transform.rotation = _player.transform.rotation;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            setHaathiProgrammingFlag(false);
            _browserConnect.SendMessage("setCodeEditorVisibility", false);
        }
        
        if (isExecute)
        {
            if (BrowserConnect.commandsList.Count > 0)
            {
                //Debug.Log("Current command  -  " + BrowserConnect.commandsList[0]);
                string currCommand = BrowserConnect.commandsList[0];
                if (currCommand.StartsWith("moveForward"))
                {
                    string param = currCommand.Split(' ')[1];
                    //Debug.Log("moveforward param - " + param);

                    if (!initCalcToggle)
                    {
                        float dist = 0;
                        if (float.TryParse(param, out dist))
                        {

                        }
                        //Debug.Log("angle - " + Vector3.Angle(new Vector3(-2.5f, 0, 0), transform.forward));
                        haathiForwardFactVec.Set(0, 0, dist/50);
                        targetPos = transform.localPosition + transform.TransformDirection(haathiForwardFactVec);//haathiForwardFactVec_adjusted;
                       // if (flag)
                        //{
                         //   _pen.SendMessage("markControlObjPoint");
                        //}
                        initCalcToggle = true;
                    }

                    //Debug.Log("position - " + transform.position);

                    transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, lerpStepFactor);

                    if (lerpStepFactor <= 1.0f)
                    {
                        lerpStepFactor += 0.01f;
                    }
                    else //Movement animation is done
                    {
                        _pen.SendMessage("markControlObjPoint");
                        lerpStepFactor = 0.0f;
                        BrowserConnect.commandsList.RemoveAt(0);
                        initCalcToggle = false;
                    }
                }
                else if (currCommand.Equals("penDown"))
                {
                    _pen.SendMessage("setPenDownStatus", true);
                    BrowserConnect.commandsList.RemoveAt(0);
                }
                else if (currCommand.Equals("penUp"))
                {
                    _pen.SendMessage("setPenDownStatus", false);
                    BrowserConnect.commandsList.RemoveAt(0);
                }
                else if (currCommand.Equals("penClear"))
                {
                    _pen.SendMessage("clearPenSandBox");
                    BrowserConnect.commandsList.RemoveAt(0);
                }
                else if (currCommand.StartsWith("penSetColor"))
                {
                    string param = currCommand.Split(' ')[1];
                    Color penColor;
                    ColorUtility.TryParseHtmlString(param, out penColor);
                    _pen.SendMessage("setPenColor",penColor);
                    BrowserConnect.commandsList.RemoveAt(0);
                }
                else if (currCommand.Equals("turnRight"))
                {
                    doTurnOpsEveryFrame(90);
                }
                else if (currCommand.Equals("turnLeft"))
                {
                    doTurnOpsEveryFrame(-90);
                }
                else if (currCommand.Equals("eat"))
                {
                    Debug.Log("Eat command received");
                    if (isBananaHit)
                    {
                        Debug.Log("Banana eat!");
                        //Make banana invisible 
                        hitBananaObj.SendMessage("setVisibility", false);
                        //Score animation

                        //Score update
                        gameScore += 50;
                        //_canvas.SendMessage("updateScore", gameScore);

                        //Cleanup
                        isBananaHit = false;
                        hitBananaObj = null;
                        BrowserConnect.commandsList.RemoveAt(0);
                    }
                }
                else if (currCommand.StartsWith("turnClockwise"))
                {
                    string param = currCommand.Split(' ')[1];
                    int angle = 0;
                    if (int.TryParse(param, out angle))
                    {
                        doTurnOpsEveryFrame(angle);
                    }
                }
                else if (currCommand.StartsWith("turnAntiClockwise"))
                {
                    string param = currCommand.Split(' ')[1];
                    int angle = 0;
                    if (int.TryParse(param, out angle))
                    {
                        doTurnOpsEveryFrame(-angle);
                    }
                }
                else if (currCommand.StartsWith("sayForTime")) //Process this before "think" because "thinkForTime" starts with "think" ... just genius \(-_-)'/
                {
                    string param = currCommand.Split(' ')[1]; //text
                    string param2 = currCommand.Split(' ')[2]; //time
                    _speechBox.SetActive(true);

                    Text txt = GameObject.Find("speechBoxText").GetComponent<Text>();
                    txt.text = param;

                    float time = 0; //default

                    //Debug.Log("sayForTime");
                    if (float.TryParse(param2, out time))
                    {
                        StartCoroutine(showElementForSecs(time, _speechBox));
                    }
                    BrowserConnect.commandsList.RemoveAt(0);
                }
                else if (currCommand.StartsWith("say"))
                {
                    //_speechBox.text = "sdsd";
                    string param = currCommand.Split(' ')[1];//text
                    //Debug.Log("Saying...");
                    _speechBox.SetActive(true);
                    Text txt = GameObject.Find("speechBoxText").GetComponent<Text>();
                    txt.text = param;

                    BrowserConnect.commandsList.RemoveAt(0);
                }
                else if (currCommand.StartsWith("thinkForTime")) //Process this before "think" because "thinkForTime" starts with "think" ... just genius \(-_-)'/
                {
                    string param = currCommand.Split(' ')[1]; //text
                    string param2 = currCommand.Split(' ')[2]; //time
                    //Debug.Log("Thinking...");
                    _thinkBox.SetActive(true);
                    Text txt = GameObject.Find("thinkBoxText").GetComponent<Text>();
                    txt.text = param;

                    float time = 0;//default

                    if (float.TryParse(param2, out time))
                    {
                        StartCoroutine(showElementForSecs(time, _thinkBox));
                    }

                    BrowserConnect.commandsList.RemoveAt(0);
                }
                else if (currCommand.StartsWith("think"))
                {
                    string param = currCommand.Split(' ')[1];
                    //Debug.Log("Thinking...");
                    _thinkBox.SetActive(true);
                    Text txt = GameObject.Find("thinkBoxText").GetComponent<Text>();
                    txt.text = param;
                    BrowserConnect.commandsList.RemoveAt(0);
                }
                else if (currCommand.StartsWith("changeCostume"))
                {
                    string param1 = currCommand.Split(' ')[1]; //Costume ID
                    BrowserConnect.commandsList.RemoveAt(0);
                }
                else if (currCommand.StartsWith("gotoxy"))
                {
                    string param1 = currCommand.Split(' ')[1]; //x
                    string param2 = currCommand.Split(' ')[2]; //y

                    BrowserConnect.commandsList.RemoveAt(0);
                }
                else if (currCommand.StartsWith("playSound"))
                {
                    string param1 = currCommand.Split(' ')[1]; //Sound ID
                    BrowserConnect.commandsList.RemoveAt(0);
                }
            }
            else // No more commands left to execute
            {
                isExecute = false;
                _browserConnect.SendMessage("setCodeEditorVisibility",false);                
            }
        }
	}

    IEnumerator showElementForSecs(float time, GameObject obj)
    {
        yield return new WaitForSeconds(time);
        obj.SetActive(false);

    }

    void doTurnOpsEveryFrame(int ang)
    {
        if (!initCalcToggle)
        {
            Vector3 currRotEuler = transform.rotation.eulerAngles;
            targOrient = Quaternion.Euler(currRotEuler.x, currRotEuler.y + ang, currRotEuler.z);
            initCalcToggle = true;
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, targOrient, lerpStepFactor);

        if (lerpStepFactor <= 1.0f)
        {
            lerpStepFactor += 0.01f;
        }
        else
        {
            lerpStepFactor = 0.0f;
            BrowserConnect.commandsList.RemoveAt(0);
            initCalcToggle = false;
        }
    }

    void startExecution()
    {
        isExecute = true;
    }

    public void evalBlocklyCode()
    {
        #if UNITY_EDITOR
            BrowserConnect.commandsList.Add("penDown");
            BrowserConnect.commandsList.Add("penSetColor #0000ff");
            BrowserConnect.commandsList.Add("moveForward 300");
            BrowserConnect.commandsList.Add("turnAntiClockwise 90");
            BrowserConnect.commandsList.Add("moveForward 300");
            BrowserConnect.commandsList.Add("turnAntiClockwise 90");
            BrowserConnect.commandsList.Add("moveForward 300");
            BrowserConnect.commandsList.Add("turnAntiClockwise 90");
            BrowserConnect.commandsList.Add("moveForward 300");
            //BrowserConnect.commandsList.Add("turnAntiClockwise 45");
            //BrowserConnect.commandsList.Add("moveForward 400");
            //BrowserConnect.commandsList.Add("penClear");
            //BrowserConnect.commandsList.Add("thinkForTime hellosay 2");
            BrowserConnect.commandsList.Add("think hellothink");
            /*BrowserConnect.commandsList.Add("penSetColor #ff0000");
            BrowserConnect.commandsList.Add("turnClockwise 45");
            BrowserConnect.commandsList.Add("moveForward 550");                       
            BrowserConnect.commandsList.Add("penUp");
            BrowserConnect.commandsList.Add("moveForward 100");*/
            
            /*BrowserConnect.commandsList.Add("eat");
            BrowserConnect.commandsList.Add("turnRight");    
            BrowserConnect.commandsList.Add("moveForward");
            BrowserConnect.commandsList.Add("moveForward");
            BrowserConnect.commandsList.Add("moveForward");
            BrowserConnect.commandsList.Add("eat");*/
            
           // BrowserConnect.commandsList.Add("moveForward");
            //BrowserConnect.commandsList.Add("turnLeft");
            startExecution(); //isExecute = true;   
        #endif
        #if UNITY_WEBGL
            _browserConnect.SendMessage("evaluateBlocklyCode");            
        #endif            
    }

    void bananaHit(GameObject obj)
    {
        //Debug.Log("banana + " + obj.ban + "hit!");
        isBananaHit = true;
        hitBananaObj = obj;
    }

    bool clickToggle = false;
    public static bool isHaathiBeingProgrammed = false;

    void setHaathiProgrammingFlag(bool state)
    {
        isHaathiBeingProgrammed = state;
    }
    void toggleHaathiProgrammingFlag()
    {
        isHaathiBeingProgrammed = !isHaathiBeingProgrammed;
    }

    void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0))
        {
            clickToggle = !clickToggle;
            Debug.Log("Mouse clicked");
            _browserConnect.SendMessage("setCodeEditorVisibility", clickToggle);
            #if !UNITY_EDITOR && UNITY_WEBGL
                toggleHaathiProgrammingFlag();                
            #endif
        }
    }

}
