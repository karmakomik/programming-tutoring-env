﻿//using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using IronPython;
//using IronPython.Modules;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine.UI;
using Microsoft.Scripting.Hosting;

public class PythonTest : MonoBehaviour
{
    
    public GameObject haathiObj;
    public InputField rawCodeInputField;
    public InputField richTextInputField;
    StringBuilder init, pythonLines;
    ScriptSource scriptSource;
    ScriptEngine scriptEngine;
    ScriptScope scriptScope;
    Dictionary<string, string> syntaxHighLightDict;

    // Use this for initialization
    void Start ()
    {
        //syntaxHighLightDict = new Dictionary<string, string>();
#if UNITY_ANDROID
        TouchScreenKeyboard keyboard;
        TouchScreenKeyboard.hideInput = false;
        if (!TouchScreenKeyboard.visible)
            keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default, false, false, false, false, "xyz");
#endif
        Dictionary<string, object> options = new Dictionary<string, object>();
        options["Debug"] = true;
        scriptEngine = IronPython.Hosting.Python.CreateEngine(options);

        scriptScope = scriptEngine.CreateScope();

        // load the assemblies for unity, using types    
        // to resolve assemblies so we don't need to hard code paths    
        //ScriptEngine.Runtime.LoadAssembly(typeof(PythonFileIOModule).Assembly);
        scriptEngine.Runtime.LoadAssembly(System.Reflection.Assembly.GetExecutingAssembly());
        scriptEngine.Runtime.LoadAssembly(typeof(GameObject).Assembly);
        //scriptEngine.Runtime.LoadAssembly(typeof(Editor).Assembly);
        scriptEngine.Runtime.LoadAssembly(typeof(CubeScript).Assembly); // Source : http://stackoverflow.com/questions/11766181/ironpython-in-unity3d
        scriptEngine.Runtime.LoadAssembly(typeof(PythonTest).Assembly);
        //string dllpath = System.IO.Path.GetDirectoryName((typeof(ScriptEngine)).Assembly.Location).Replace("\\", "/");
        // load needed modules and paths    
        init = new StringBuilder();

        //init.AppendLine("cubeScriptComp.move(10)");
        //GameObject.Find("Cube").GetComponent<CubeScript>().move(10)
        //init.AppendLine("cube.transform.Translate(10, 0, 0)");
        //var ScriptSource = ScriptEngine.CreateScriptSourceFromString(init.ToString());
        //ScriptSource.Execute(ScriptScope);
        //pythonLines.AppendLine(init.ToString());
        //Debug.Log("Code typed is " + pythonLines.ToString());

        //scriptSource = scriptEngine.CreateScriptSourceFromString(init.ToString());
        string[] initPyCode =
        {
            "import sys",
            "import UnityEngine as unity",
            "import CubeScript",
            "import PythonTest",
            "import System",
            "unity.Debug.Log(\"Python console initialized\")",
            "haathiObj = unity.GameObject.Find(\"haathi\")",
            "class haathiClass:",
            "   \"This class acts as an interface with the Unity haathi object\"",
            "   def __init__(self):",
            "       unity.Debug.Log(\"haathi object initialized\")",
            "       self.haathiObjScript = haathiObj.GetComponent[CubeScript]()",
            "   def move(self, units):",
            "       self.haathiObjScript.addCommandToPool(\"move \" + str(units))",
            "   def wait(self, units):",
            "       self.haathiObjScript.addCommandToPool(\"wait \" + str(units))",
            "   def rotate(self, units):",
            "       self.haathiObjScript.addCommandToPool(\"rotate \" + str(units))",
            "   def goto(self, x, y):",
            "       self.haathiObjScript.addCommandToPool(\"goto \" + str(x) + \" \" + str(y))",
            "   def say(self, text):",
            "       self.haathiObjScript.addCommandToPool(\"say \" + str(text))",
            "   def think(self, text):",
            "       self.haathiObjScript.addCommandToPool(\"think \" + str(text))",
            "   def changeColor(self, color):",
            "       self.haathiObjScript.addCommandToPool(\"changeColor \" + str(color))",
            "   def playSound(self, sound):",
            "       self.haathiObjScript.addCommandToPool(\"playSound \" + str(sound))",
            "   def penDown(self):",
            "       self.haathiObjScript.addCommandToPool(\"penDown\")",
            "   def penUp(self):",
            "       self.haathiObjScript.addCommandToPool(\"penUp\")",
            "   def setPenColor(self, color):",
            "       self.haathiObjScript.addCommandToPool(\"setPenColor \" + str(color))",
            "   def isTouching(self, obj):",
            "       self.haathiObjScript.addCommandToPool(\"isTouching \" + str(obj))",
            "haathiObject = haathiClass()",
            "",
        };
        scriptSource = scriptEngine.CreateScriptSourceFromString(string.Join("\r", initPyCode));
        try
        {
            scriptSource.Execute(scriptScope);
        }
        catch (System.Exception e)
        {
            ExceptionOperations eo = scriptEngine.GetService<ExceptionOperations>();
            string error = eo.FormatException(e);
            Debug.Log(error);
        }
    }



    MatchEvaluator evaluator = delegate (Match m)
    {
        string replaceStr ="";
        //Debug.Log("Matched word = " + m.Value);
        if (m.Value.Contains("def") || m.Value.Contains("class"))
        {
            replaceStr = "<color=aqua>" + m.Value + "</color>";
        }
        else if (m.Value.Contains("import") || m.Value.Contains("if") || m.Value.Contains("for") || m.Value.Contains("in") || m.Value.Contains("as") || m.Value.Contains("in") || m.Value.Contains("while"))
        {
            replaceStr = "<color=orange>" + m.Value + "</color>";
        }
        else 
        {
            //Debug.Log("matched");
            replaceStr = m.Value;
        }

        return replaceStr;
    };

    public void onCodeChange()
    {
        //Debug.Log("rawcode - " + rawCodeInputField.text);
        //if(codeEditor.text.Contains
        //codeEditorRichText.text = codeEditor.text;
        string richTxtCode = rawCodeInputField.text;
        //Debug.Log(richTxtCode.IndexOf("haathiObject"));

        Regex pythonSyntaxRegEx = new Regex("(def )|(if )|(return )|(class )|(for )|(import )|(as )|(=)|(while )|(in )|(haathiObject)");
        //([^a-zA-Z]def )|([^a-zA-Z]if )|([^a-zA-Z]return )|([^a-zA-Z]class )|([^a-zA-Z]import )|([^a-zA-Z]as )|(=)

        //richTxtCode = richTxtCode.Replace("haathiObject", "<b>haathiObject</b>");
        //richTxtCode = Regex.Replace(richTxtCode, "(def )", "<color=aqua>def</color>")
        richTxtCode = pythonSyntaxRegEx.Replace(richTxtCode, evaluator);

        richTextInputField.text = richTxtCode;
        richTextInputField.caretPosition = rawCodeInputField.caretPosition;
        //Debug.Log("rich text code - " + richTxtCode);
        //richTextInputField.onValueChanged.AddListener(delegate { test(); });
    }

    public void test()
    {
        Debug.Log("test");
    }

    public void runCode()
    {
        haathiObj.GetComponent<CubeScript>().clearCommandPool();
        
        pythonLines = new StringBuilder();
        pythonLines.AppendLine(rawCodeInputField.text);
        string[] lines =
        {
            "def traceit(frame, event, arg):",
            "   if event == \"line\":",
            "       lineno = frame.f_lineno",
            "       unity.Debug.Log(\"line\" + str(lineno))",
            "       unity.Debug.Log(\"f_code\" + str(frame.f_code))",
            "       #unity.Debug.Log(\"f_back\" + str(frame.f_back))",
            "   return traceit",
            "sys.settrace(traceit)",
            "",
        };
        //string.Join("\r", lines);
        string finalCode = /*string.Join("\r", lines) +*/ pythonLines.ToString();
        Debug.Log("Code typed is " + finalCode);

        scriptSource = scriptEngine.CreateScriptSourceFromString(finalCode);
        scriptSource.Execute(scriptScope);
        haathiObj.GetComponent<CubeScript>().startExecution();

    }

    // Update is called once per frame
    void Update ()
    {
		
	}
}
