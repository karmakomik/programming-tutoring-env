using UnityEngine;
using System.Collections;

public class BananaScript : MonoBehaviour 
{
    public static int objCount = 0; //shared by all objects
    GameObject _haathi;
    public int bananaID;
	// Use this for initialization
	void Start () 
    {
        bananaID = ++objCount;
        //Debug.Log("banana count : " + bananaID);
        //transform.gameObject.AddComponent()
        _haathi = GameObject.Find("haathi");
	}
	
	// Update is called once per frame
	void Update () 
    {
        transform.Rotate(0,1,0);
	}

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision!");
        _haathi.SendMessage("bananaHit", transform.gameObject);
    }

    public void setVisibility(bool status)
    {
        transform.gameObject.SetActive(status);
    }

}
