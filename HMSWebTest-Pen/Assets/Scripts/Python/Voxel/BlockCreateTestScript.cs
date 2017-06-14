using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cubiquity;

public class BlockCreateTestScript : MonoBehaviour
{
    private ColoredCubesVolume coloredCubesVolume;

    // Use this for initialization
    void Start ()
    {
        coloredCubesVolume = gameObject.GetComponent<ColoredCubesVolume>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            Debug.Log("Up pressed");
            for (int i = 0; i < 30; i++)
            {
                coloredCubesVolume.data.SetVoxel(50, i, 50, (QuantizedColor)Color.green);
            }
            
            coloredCubesVolume.ForceUpdate();
        }
    }
}
