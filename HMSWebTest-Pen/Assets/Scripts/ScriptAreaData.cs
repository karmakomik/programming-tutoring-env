using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptAreaData : MonoBehaviour
{
    List<string> commandsList = new List<string>();
    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    // Use this for initialization
    void Start ()
    {
        //commandsList.Add("test");

    }

    void addScript(List<string> lst)
    {
        commandsList = lst;
    }

    public List<string> getScript()
    {
        return commandsList;
    }

    // Update is called once per frame
    void Update ()
    {
		
	}
}
