using UnityEngine;
using System.Collections;

public class CamFollowScript : MonoBehaviour 
{
    Vector3 camPosDisplacement;
    public GameObject haathi;
	// Use this for initialization
	void Start () 
    {
        camPosDisplacement = new Vector3(0,5,-7);
        //camPosDisplacement = new Vector3(-147, , -7);    
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = haathi.transform.position + camPosDisplacement;
	}
}
