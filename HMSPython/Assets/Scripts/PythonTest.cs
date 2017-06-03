//using System.Collections;
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

        Dictionary<string, object> options = new Dictionary<string, object>();
        options["Debug"] = true;
        scriptEngine = IronPython.Hosting.Python.CreateEngine(options);

        scriptScope = scriptEngine.CreateScope();

        //richTextInputField.Select();
        //richTextInputField.ActivateInputField();

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
        scriptSource = scriptEngine.CreateScriptSourceFromFile("haathi.py");
        scriptSource.Execute(scriptScope);   
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
            Debug.Log("matched");
            replaceStr = m.Value;
        }

        return replaceStr;
    };

    public void onCodeChange()
    {
        Debug.Log("rawcode - " + rawCodeInputField.text);
        //if(codeEditor.text.Contains
        //codeEditorRichText.text = codeEditor.text;
        string richTxtCode = rawCodeInputField.text;
        //Debug.Log(richTxtCode.IndexOf("haathiObject"));

        Regex pythonSyntaxRegEx = new Regex("(def )|(if )|(return )|(class )|(for )|(import )|(as )|(=)|(while )|(in )|(haathiObject.)");

        //richTxtCode = richTxtCode.Replace("haathiObject", "<b>haathiObject</b>");
        //richTxtCode = Regex.Replace(richTxtCode, "(def )", "<color=aqua>def</color>")
        richTxtCode = pythonSyntaxRegEx.Replace(richTxtCode, evaluator);

        richTextInputField.text = richTxtCode;
        richTextInputField.caretPosition = rawCodeInputField.caretPosition;
        Debug.Log("rich text code - " + richTxtCode);
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
        string finalCode = string.Join("\r", lines) + pythonLines.ToString();
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
