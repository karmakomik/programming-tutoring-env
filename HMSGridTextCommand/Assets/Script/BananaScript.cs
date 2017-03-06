using UnityEngine;
using System.Collections;

public class BananaScript : MonoBehaviour 
{
    public static int objCount = 0; //shared by all objects    
	// Use this for initialization
	void Start () 
    {
        ++objCount;
        Debug.Log("banana count : " + objCount);
	}
	
	// Update is called once per frame
	void Update () 
    {
        transform.Rotate(0,1,0);
	}
}
