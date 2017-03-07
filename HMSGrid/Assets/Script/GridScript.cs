using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Vectrosity;

[ExecuteInEditMode]

public class GridScript : MonoBehaviour 
{
    VectorLine line1, line2;
    List<Vector3> pointsList1, pointsList2;
    float startX = -120;
    float startZ = -100;
    float gridYHeight = 0.2f;
    Material lineMat;
    public Material lineMaterial1;

	// Use this for initialization
	void Start () 
    {
        pointsList1 = new List<Vector3>();
        pointsList2 = new List<Vector3>();
        for(int i = 0; i < 100; i++)
        {
            pointsList1.Add(new Vector3(startX + i*3, gridYHeight, startZ));
            pointsList1.Add(new Vector3(startX + i * 3, gridYHeight, startZ + 300));
            pointsList2.Add(new Vector3(startX, gridYHeight, startZ + i * 3));
            pointsList2.Add(new Vector3(startX + 300, gridYHeight, startZ + i * 3));
        }

        line1 = new VectorLine("grid1", pointsList1, null, 0.3f, LineType.Discrete);
        line1.AddNormals();
        line2 = new VectorLine("grid2", pointsList2, null, 0.9f, LineType.Discrete);
        line2.AddNormals();
	}
	
	// Update is called once per frame
	void Update () 
    {
        line1.Draw3D();
        line2.Draw3D();
	}
}
