using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Vectrosity;

public class LineDraw : MonoBehaviour 
{
	VectorLine line1;
	List<Color> colorsList;
	// Use this for initialization
	void Start () 
	{
		colorsList = new List<Color> ();
		line1 = new VectorLine("line", new Vector3[0], null, 3f, LineType.Continuous);
		line1.points3.Add (new Vector3(0, 0, 0));
		line1.points3.Add (new Vector3(-35, 0, -70));
		line1.points3.Add (new Vector3(20, 0, -70));
		line1.points3.Add (new Vector3(50, 0, -30));
		colorsList.Add (Color.blue);
		colorsList.Add (Color.red);
		colorsList.Add (Color.yellow);
		//colorsList.Add (Color.red);
		//line1.SetColors (colorsList);
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Debug.Log (line1.points3.Count);
		line1.SetColors (colorsList);
		line1.Draw3D();
	}
}
