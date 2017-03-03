using UnityEngine;
using System.Collections;

public class TurtleArtDisplayScript : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        var texture = new Texture2D(512, 512, TextureFormat.ARGB32, false);
        //var texture = sandBoxTexture;
        // set the pixel values
        /*for (int i = 0; i < 512; ++i)
        {
            texture.SetPixel(i, i, Color.black);
        }*/

        Color[] colors = new Color[texture.GetPixels().Length];
        for (int i = 0; i < colors.Length; ++i)
        {
            colors[i] = Color.gray;// defaultBGColor;
        }
        texture.SetPixels(colors);

        // Apply all SetPixel calls
        texture.Apply(false);

        // connect texture to material of GameObject this script is attached to
        GetComponent<Renderer>().material.mainTexture = texture;

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
