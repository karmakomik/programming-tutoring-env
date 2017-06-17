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

    }
}
