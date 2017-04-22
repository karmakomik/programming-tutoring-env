using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

/*public class GameLevel
{
    int level;
    enum GameState {};
}*/

public class GameControl : MonoBehaviour
{
    public GameObject proj;
    public LayerMask mask;

    public GameObject exitPanel;
    public GameObject speechPanel;

    public GameObject codeCanvas;

    public Button closeButton;
    public Button volButton;

    public Sprite soundOffSprite;
    public Sprite soundOnSprite;

    public GameObject textCodeArea;
    bool showTextArea = false;

    public GameObject targetCircle;
    //public GameObject selectionCircle;
    public GameObject mainAvatar;
    
    public GameObject targetArrow;

    public static List<string> commList;

    int gameState = 0;
    //enum GameState { }

    private Ray ray;
    private RaycastHit hit;
    // Use this for initialization

    List<string> hindiInstructions = new List<string>();
    List<string> engInstructions = new List<string>();

    void Start ()
    {
        //commList = new List<string>();
        AudioListener.volume = 0; //Set BG volume to 0
        changeTargetCircleColor(Color.red);
        //setTargetCircleVisible(true);
        targetCircle.SetActive(true);
        targetArrow.SetActive(true);

        //initializeInstructions();

        /*GameObject obj = GameObject.Find("ScriptObj");
        if (obj != null)
        {
            Debug.Log("Script from editor - ");
            commList =  obj.GetComponent<ScriptAreaData>().getScript();
            foreach (string s in commList)
            {
                Debug.Log(s);                    
            }
            DestroyObject(obj); //If you dont destroy this, the object persists across future transitions between the code editor and the main scene
        }
        else
        {
            Debug.Log("script not found");
        }*/
    }

    void initializeInstructions()
    {
        //1
        hindiInstructions.Add("yky o`Ùk ls fpfàr {ks= ij tk,a");
        engInstructions.Add("Go to the area marked by the red circle");

        //1
        hindiInstructions.Add("vPNk] vc iwjs xkao dks ryk'kus dk ç;kl djsa");
        engInstructions.Add("Good, now try exploring the entire village");

    }

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

#if UNITY_STANDALONE || UNITY_EDITOR_WIN
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, mask.value) && Input.GetMouseButtonDown(0) 
            && hit.transform.name.StartsWith("ground") && !IsPointerOverUIObject())
        {
            //Debug.Log("Touch at " + hit.point);
            //Vector3 transformedHitPt = transform.TransformPoint(hit.transform.position);
            proj.transform.position = new Vector3(hit.point.x, proj.transform.position.y, hit.point.z);
            GameObject.Find("char1").SendMessage("setTarget", proj.transform);
        }
#endif
#if UNITY_ANDROID
        if (Input.touchCount == 1)
        {
            //Debug.Log("Touch!");
            ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            
            if (Physics.Raycast(ray, out hit, mask.value) && hit.transform.name.StartsWith("ground") && !IsPointerOverUIObject())// && hit.transform.name.StartsWith("Road"))
            {
                //Debug.Log("Touch!");
                proj.transform.position = new Vector3(hit.point.x, proj.transform.position.y, hit.point.z);
                GameObject.Find("char1").SendMessage("setTarget", proj.transform);
            }
        }
#endif
    }

    void LateUpdate()
    {

    }

    public void setCodeCanvasVisibility(bool state)
    {
        codeCanvas.SetActive(state);
    }

    void checkAvatarReachTarget()
    {
        if (Vector3.Distance(targetCircle.transform.position, mainAvatar.transform.position) < 1)
        {
            Debug.Log("Target reached");
            changeTargetCircleColor(Color.blue);
            targetCircle.SetActive(false);
            targetArrow.SetActive(false);
        }
        else
        {
            changeTargetCircleColor(Color.red);
            //setTargetCircleVisible(false);
        }
    }

    void setTargetCircleVisible(bool flag)
    {
        //targetCircle.SetActive(flag);
    }

    private bool IsPointerOverUIObject() // Source : https://forum.unity3d.com/threads/ispointerovereventsystemobject-always-returns-false-on-mobile.265372/
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    public void AppQuit()
    {
        Application.Quit();        
    }

    public void showExitPanel(bool status)
    {
        exitPanel.SetActive(status);
    }

    public void toggleTextCodeWindowVisibility()
    {
        showTextArea = !showTextArea;
        textCodeArea.SetActive(showTextArea);
        /*if (showTextArea)
        {
            textCodeArea.GetComponent<InputField>().ActivateInputField();
            textCodeArea.GetComponent<InputField>().Select();
        }*/
    }

    public void showSpeechPanel(bool status)
    {
        speechPanel.SetActive(status);
    }


    public void loadVisualCodeEditor()
    {
        //SceneManager.LoadScene(1);
    }
    bool volStatus = false;
    public void ChangeVolStatus()
    {
        volStatus = !volStatus;
        if (!volStatus)
        {
            AudioListener.volume = 0;
            volButton.image.sprite = soundOffSprite;
        }
        else 
        {
            AudioListener.volume = 0.4f;
            volButton.image.sprite = soundOnSprite;
        }
    }

    void changeTargetCircleColor(Color c)
    {
        Projector proj = targetCircle.GetComponent<Projector>();
        Material mat = proj.material;
        //mat.shader = Shader.Find("Projector/AdditiveTint");
        mat.SetColor("_Color", c);
    }
}