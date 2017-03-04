using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ArtSandboxEntranceScript : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag.Equals("Player"))
        {
            Debug.Log("Hit door!");
            SceneManager.LoadScene("artsandbox");
        }
        //
    }
}
