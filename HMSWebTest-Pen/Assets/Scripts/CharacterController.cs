using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour 
{
    public float speed = 10.0f;
	// Use this for initialization
	void Start () 
    {
        Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update() 
    {
        if (!HaathiScript.isHaathiBeingProgrammed)
        {
            transform.Translate(Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0, Input.GetAxis("Vertical") * speed * Time.deltaTime);
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }
	}
}
