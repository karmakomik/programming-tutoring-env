import sys
import UnityEngine as unity
import CubeScript
import PythonTest
import System

unity.Debug.Log("Python console initialized")

#obj1 = unity.GameObject.Find("Cube")
#cube = obj1.GetComponent[CubeScript]()
#obj2 = unity.GameObject.Find("Sphere")
#sphere = obj2.GetComponent[CubeScript]()
haathiObj = unity.GameObject.Find("haathi")
haathiObjScript = haathiObj.GetComponent[CubeScript]()

class haathiClass:
	"This class acts as an interface with the Unity haathi object"
	def __init__(self):
		unity.Debug.Log("haathi object initialized")
		pyConnObj = unity.GameObject.Find("PythonConn")
		pyConnScript = 	pyConnObj.GetComponent[PythonTest]()			
		#self.haathiObj = obj1.GetComponent[CubeScript]()
		#self.haathiObj = unity.GameObject.Instantiate(haathiObj)
		#self.haathiObj = unity.GameObject.Instantiate(unity.Resources.Load("haathi"))
		self.haathiObjScript = haathiObj.GetComponent[CubeScript]()

	def move(self, units):
		self.haathiObjScript.addCommandToPool("move " + str(units))
	
	def wait(self, units):
		self.haathiObjScript.addCommandToPool("wait " + str(units))	

	def rotate(self, units):
		self.haathiObjScript.addCommandToPool("rotate " + str(units))		

	def goto(self, x, y):
		self.haathiObjScript.addCommandToPool("goto " + str(x) + " " + str(y))

	def say(self, text):
		self.haathiObjScript.addCommandToPool("say " + str(text))

	def think(self, text):
		self.haathiObjScript.addCommandToPool("think " + str(text))	

	def changeColor(self, color):
		self.haathiObjScript.addCommandToPool("changeColor " + str(color))

	def playSound(self, sound):
		self.haathiObjScript.addCommandToPool("playSound " + str(sound))

	def penDown(self):
		self.haathiObjScript.addCommandToPool("penDown")	

	def penUp(self):
		self.haathiObjScript.addCommandToPool("penUp")	

	def setPenColor(self, color):
		self.haathiObjScript.addCommandToPool("setPenColor " + str(color))	

	def isTouching(self, obj):	
		self.haathiObjScript.addCommandToPool("isTouching " + str(obj))	

#Trace command callback 
def traceit(frame, event, arg):
   if event == "line": 
       lineno = frame.f_lineno       
       unity.Debug.Log("line " + str(lineno))
   return traceit
#sys.settrace(traceit)

#haathiObjScript.addCommandToPool("move 200")
#haathiObjScript.addCommandToPool("wait 2")
#haathiObjScript.addCommandToPool("wait 2")


haathiObject = haathiClass()
#haathiObject.move(50)
#haathiObject.wait(1)
#haathiObject.move(50)
#haathiObject.wait(1)
#System.Diagnostics.Debugger.Break()
#haathiObject.rotate(60)
#haathiObject.wait(1)
#haathiObject.goto(100,200)