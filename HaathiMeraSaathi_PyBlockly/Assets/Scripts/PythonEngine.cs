using Microsoft.Scripting.Hosting;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class PythonEngine : MonoBehaviour
{
    public GameObject haathiObj;
    ScriptSource scriptSource;
    ScriptEngine scriptEngine;
    ScriptScope scriptScope;
    string finalCode;
    StringBuilder pythonLines;


    void Start()
    {
        scriptEngine = IronPython.Hosting.Python.CreateEngine();
        scriptScope = scriptEngine.CreateScope();
        scriptEngine.Runtime.LoadAssembly(System.Reflection.Assembly.GetExecutingAssembly());
        scriptEngine.Runtime.LoadAssembly(typeof(GameObject).Assembly);
        scriptEngine.Runtime.LoadAssembly(typeof(ProgrammableGameObjectScript).Assembly); // Source : http://stackoverflow.com/questions/11766181/ironpython-in-unity3d
        scriptEngine.Runtime.LoadAssembly(typeof(PythonEngine).Assembly);
        string[] initPyCode =
        {
            "import sys",
            //"sys.path.append(\"E:\\Program Files (x86)\\IronPython 2.7\\Lib\")",
            //"import random",
            "import UnityEngine as unity",
            "import ProgrammableGameObjectScript",
            "import PythonEngine",
            "import System",
            "unity.Debug.Log(\"Python console initialized\")",
            "unity.Debug.Log(sys.version)",
            "haathiObj = unity.GameObject.Find(\"haathi\")",
            "class Color :",
            "  red = \"#FF0000\"",
            "  blue = \"#0000FF\"",
            "  green = \"#00FF00\"",
            "  cyan = \"#00FFFF\"",
            "  magenta = \"#FF00FF\"",
            "  gray = \"#888888\"",
            "  grey = \"#888888\"",
            "  white = \"#FFFFFF\"",
            "  black = \"#000000\"",
            "  def __init__(self, red, green, blue):",
            "    self._red = red",
            "    self._green = green",
            "    self._blue = blue",
            "    self.color = \"#\" + str(format(red, '02x')) + str(format(blue, '02x')) + str(format(green, '02x'))",
            "  def __str__(self):",
            "    return self.color",
            "class haathiClass:",
            "   \"This class acts as an interface with the Unity haathi object\"",
            "   def __init__(self):",
            "       unity.Debug.Log(\"haathi object initialized\")",
            "       self.haathiObjScript = haathiObj.GetComponent[ProgrammableGameObjectScript]()",
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
            "       self.haathiObjScript.  addCommandToPool(\"playSound \" + str(sound))",
            "   def penDown(self):",
            "       self.haathiObjScript.addCommandToPool(\"penDown\")",
            "   def penUp(self):",
            "       self.haathiObjScript.addCommandToPool(\"penUp\")",
            "   def setPenColor(self, color):",
            "       self.haathiObjScript.addCommandToPool(\"setPenColor \" + str(color))",
            "   def isTouching(self, obj):",
            "       self.haathiObjScript.addCommandToPool(\"isTouching \" + str(obj))",
            "   def moveForward(self):",
            "       self.haathiObjScript.addCommandToPool(\"moveForward\")",
            "   def moveUp(self):",
            "       self.haathiObjScript.addCommandToPool(\"moveUp\")",
            "   def moveDown(self):",
            "       self.haathiObjScript.addCommandToPool(\"moveDown\")",
            "   def turnRight(self):",
            "       self.haathiObjScript.addCommandToPool(\"turnRight\")",
            "   def turnLeft(self):",
            "       self.haathiObjScript.addCommandToPool(\"turnLeft\")",
            "   def placeBlock(self):",
            "       self.haathiObjScript.addCommandToPool(\"placeBlock\")",
            "   def clearBlock(self):",
            "       self.haathiObjScript.addCommandToPool(\"clearBlock\")",
            "   def setBlockColor(self, color):",
            "       self.haathiObjScript.addCommandToPool(\"setBlockColor \" + str(color))",
            "haathiObject = haathiClass()",
            "def pressLeftArrow():",
            "   unity.Debug.Log(\"Left arrow key pressed in python\")",
            "   rt = dir(haathiClass)",
            "   unity.Debug.Log(str(rt))",
            "   haathiObject.rotate(-45)",
            "",
        };
        scriptSource = scriptEngine.CreateScriptSourceFromString(string.Join("\r", initPyCode));
        try
        {
            scriptSource.Execute(scriptScope);
            //pyStatus.text = "Python initiliazed";
        }
        catch (System.Exception e)
        {
            ExceptionOperations eo = scriptEngine.GetService<ExceptionOperations>();
            string error = eo.FormatException(e);
            Debug.Log(error);
        }
    }

    public void runCode()
    {
        haathiObj.GetComponent<ProgrammableGameObjectScript>().clearCommandPool();

        pythonLines = new StringBuilder();
        //pythonLines.AppendLine(rawCodeInputField.text);
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
        finalCode = /*string.Join("\r", lines) +*/ pythonLines.ToString();
        Debug.Log("Code typed is " + finalCode);
        scriptSource = scriptEngine.CreateScriptSourceFromString(finalCode);
        scriptSource.Execute(scriptScope);
        //pyStatus.text = "Run Code";
        haathiObj.GetComponent<ProgrammableGameObjectScript>().startExecution();

    }

    // Update is called once per frame
    void Update()
    {

    }
}

