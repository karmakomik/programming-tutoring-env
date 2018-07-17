//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenManagerScript : MonoBehaviour
{
    public GameObject haathiObj;
    public GameObject rotor;
    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        haathiObj.transform.Rotate(Vector3.up, Time.deltaTime * 20);
        rotor.transform.Rotate(0, Time.deltaTime * 800, 0);
    }

    public void startGame(int level)
    {
        SceneManager.LoadScene(level);
    }
}
