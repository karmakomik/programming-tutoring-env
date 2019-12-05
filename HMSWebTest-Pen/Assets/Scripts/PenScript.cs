using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PenScript : MonoBehaviour 
{
    public Texture2D sandBoxTexture;
	// Use this for initialization
    bool isPenDown = false;
    RaycastHit hit;
    //Ray ray;
    public GameObject controlObj; //Refers to the haathi object or any other object that controls the pen across the paint surface
    Vector2 prevPenLoc, currPenLoc; //Paint texture UV
    bool rayCastToggle = true;
    bool isFirstPtMarked = true;
    Color defaultColor = Color.black;
    Color defaultBGColor = Color.gray;
    Color currColor;
    public GameObject targetPaintingObject;

	void Start () 
    {
        currColor = defaultColor;
        clearPenSandBox();
	}

    void setPenDownStatus(bool flag)
    {
        isPenDown = flag;
    }

    void clearPenSandBox()
    {
        // Create a new 2x2 texture ARGB32 (32 bit with alpha) and no mipmaps
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
            colors[i] = defaultBGColor;
        }
        texture.SetPixels(colors);

        // Apply all SetPixel calls
        texture.Apply(false);

        // connect texture to material of GameObject this script is attached to
        GetComponent<Renderer>().material.mainTexture = texture;
        //targetPaintingObject.GetComponent<Renderer>().material.mainTexture = texture;
    }

    void setPenColor(Color c)
    {
        currColor = c;
    }
       
    void markControlObjPoint()
    {
        //prevPenLoc = currPenLoc;
        rayCastToggle = true;
        //currPenLoc = 
    }

	// Update is called once per frame
	void Update() 
    {
        if (isPenDown)
        {
            //Debug.Log("rayCastToggle = true && isPenDown = true");
            if (Physics.Raycast(controlObj.transform.position, Vector3.down, out hit, 10))
            {
                if (hit.collider.tag.Equals("drawsurface"))
                {
                    // Source : https://docs.unity3d.com/ScriptReference/RaycastHit-textureCoord2.html
                    //Make sure the collider is mesh instead of sphere or box to get texture coord                    
                                      
                    Renderer rnd = hit.collider.transform.GetComponent<Renderer>();
                    Texture2D tex = (Texture2D)rnd.material.mainTexture;
                    Vector2 pixelUV = hit.textureCoord2;
                    Vector2 finalPixelUV = pixelUV;
                    pixelUV.x *= tex.width;
                    pixelUV.y *= tex.height;

                    currPenLoc = new Vector2(Mathf.FloorToInt(pixelUV.x), Mathf.FloorToInt(pixelUV.y));
                    //Debug.Log("hit : " + currPenLoc);
                    if (isFirstPtMarked)
                    {
                        prevPenLoc = currPenLoc;
                        isFirstPtMarked = false;
                    }
                    rayCastToggle = false;
                    //Debug.Log("line from " + currPenLoc + " to " + prevPenLoc);
                    //drawBressLine(currPenLoc, prevPenLoc, currColor, tex);
                    DrawLine(currPenLoc, prevPenLoc, 1, currColor, tex, false, currColor, 1);
                    prevPenLoc = currPenLoc;
                    //isFirstPtMarked = true;
                } 
            }            
        }
	}

    //Adapted from : http://www.codeproject.com/Articles/13360/Antialiasing-Wu-Algorithm
    //Algorithm    : https://en.wikipedia.org/wiki/Xiaolin_Wu%27s_line_algorithm  
    void drawWuLine()
    { 
        
    }

    public enum Samples
    {
        None,
        Samples2,
        Samples4,
        Samples8,
        Samples16,
        Samples32,
        RotatedDisc
    }

    public static Samples NumSamples = Samples.Samples4;

    public static void AddP(List<Vector2> tmpList, Vector2 p, float ix, float iy)
    {
        var x = p.x + ix;
        var y = p.y + iy;
        tmpList.Add(new Vector2(x, y));
    }

    public static Vector2[] Sample(Vector2 p)
    {
        List<Vector2> tmpList = new List<Vector2>(32);

        switch (NumSamples)
        {
            case Samples.None:
                AddP(tmpList, p, 0.5f, 0.5f);
                break;

            case Samples.Samples2:
                AddP(tmpList, p, 0.25f, 0.5f);
                AddP(tmpList, p, 0.75f, 0.5f);
                break;

            case Samples.Samples4:
                AddP(tmpList, p, 0.25f, 0.5f);
                AddP(tmpList, p, 0.75f, 0.5f);
                AddP(tmpList, p, 0.5f, 0.25f);
                AddP(tmpList, p, 0.5f, 0.75f);
                break;

            case Samples.Samples8:
                AddP(tmpList, p, 0.25f, 0.5f);
                AddP(tmpList, p, 0.75f, 0.5f);
                AddP(tmpList, p, 0.5f, 0.25f);
                AddP(tmpList, p, 0.5f, 0.75f);

                AddP(tmpList, p, 0.25f, 0.25f);
                AddP(tmpList, p, 0.75f, 0.25f);
                AddP(tmpList, p, 0.25f, 0.75f);
                AddP(tmpList, p, 0.75f, 0.75f);
                break;
            case Samples.Samples16:
                AddP(tmpList, p, 0, 0);
                AddP(tmpList, p, 0.3f, 0);
                AddP(tmpList, p, 0.7f, 0);
                AddP(tmpList, p, 1, 0);

                AddP(tmpList, p, 0, 0.3f);
                AddP(tmpList, p, 0.3f, 0.3f);
                AddP(tmpList, p, 0.7f, 0.3f);
                AddP(tmpList, p, 1, 0.3f);

                AddP(tmpList, p, 0, 0.7f);
                AddP(tmpList, p, 0.3f, 0.7f);
                AddP(tmpList, p, 0.7f, 0.7f);
                AddP(tmpList, p, 1, 0.7f);

                AddP(tmpList, p, 0, 1);
                AddP(tmpList, p, 0.3f, 1);
                AddP(tmpList, p, 0.7f, 1);
                AddP(tmpList, p, 1, 1);
                break;

            case Samples.Samples32:
                AddP(tmpList, p, 0, 0);
                AddP(tmpList, p, 1, 0);
                AddP(tmpList, p, 0, 1);
                AddP(tmpList, p, 1, 1);

                AddP(tmpList, p, 0.2f, 0.2f);
                AddP(tmpList, p, 0.4f, 0.2f);
                AddP(tmpList, p, 0.6f, 0.2f);
                AddP(tmpList, p, 0.8f, 0.2f);

                AddP(tmpList, p, 0.2f, 0.4f);
                AddP(tmpList, p, 0.4f, 0.4f);
                AddP(tmpList, p, 0.6f, 0.4f);
                AddP(tmpList, p, 0.8f, 0.4f);

                AddP(tmpList, p, 0.2f, 0.6f);
                AddP(tmpList, p, 0.4f, 0.6f);
                AddP(tmpList, p, 0.6f, 0.6f);
                AddP(tmpList, p, 0.8f, 0.6f);

                AddP(tmpList, p, 0.2f, 0.8f);
                AddP(tmpList, p, 0.4f, 0.8f);
                AddP(tmpList, p, 0.6f, 0.8f);
                AddP(tmpList, p, 0.8f, 0.8f);

                AddP(tmpList, p, 0.5f, 0);
                AddP(tmpList, p, 0.5f, 1);
                AddP(tmpList, p, 0, 0.5f);
                AddP(tmpList, p, 1, 0.5f);

                AddP(tmpList, p, 0.5f, 0.5f);
                break;
            case Samples.RotatedDisc:
                AddP(tmpList, p, 0, 0);
                AddP(tmpList, p, 1, 0);
                AddP(tmpList, p, 0, 1);
                AddP(tmpList, p, 1, 1);

                Vector2 pq = new Vector2(p.x + 0.5f, p.y + 0.5f);
                AddP(tmpList, pq, 0.258f, 0.965f);//Sin (75°) && Cos (75°)
                AddP(tmpList, pq, -0.965f, -0.258f);
                AddP(tmpList, pq, 0.965f, 0.258f);
                AddP(tmpList, pq, 0.258f, -0.965f);
                break;
        }

        return tmpList.ToArray();
    }

    void DrawLine(Vector2 from, Vector2 to, float w, Color col, Texture2D tex, bool stroke, Color strokeCol, float strokeWidth)
    {
        w = Mathf.Round(w);//It is important to round the numbers otherwise it will mess up with the texture width
        strokeWidth = Mathf.Round(strokeWidth);

        var extent = w + strokeWidth;
        var stY = Mathf.Clamp(Mathf.Min(from.y, to.y) - extent, 0, tex.height);//This is the topmost Y value
        var stX = Mathf.Clamp(Mathf.Min(from.x, to.x) - extent, 0, tex.width);
        var endY = Mathf.Clamp(Mathf.Max(from.y, to.y) + extent, 0, tex.height);
        var endX = Mathf.Clamp(Mathf.Max(from.x, to.x) + extent, 0, tex.width);//This is the rightmost Y value

        strokeWidth = strokeWidth / 2;
        var strokeInner = (w - strokeWidth) * (w - strokeWidth);
        var strokeOuter = (w + strokeWidth) * (w + strokeWidth);
        var strokeOuter2 = (w + strokeWidth + 1) * (w + strokeWidth + 1);
        var sqrW = w * w;//It is much faster to calculate with squared values

        var lengthX = endX - stX;
        var lengthY = endY - stY;
        var start = new Vector2(stX, stY);
        Color[] pixels = tex.GetPixels((int)stX, (int)stY, (int)lengthX, (int)lengthY, 0);//Get all pixels

        for (int y = 0; y < lengthY; y++)
        {
            for (int x = 0; x < lengthX; x++)
            {//Loop through the pixels
                var p = new Vector2(x, y) + start;
                var center = p + new Vector2(0.5f, 0.5f);
                float dist = (center - MathFX.NearestPointStrict(from, to, center)).sqrMagnitude;//The squared distance from the center of the pixels to the nearest point on the line
                if (dist <= strokeOuter2)
                {
                    var samples = Sample(p);
                    var c = Color.black;
                    var pc = pixels[y * (int)lengthX + x];
                    for (int i = 0; i < samples.Length; i++)
                    {//Loop through the samples
                        dist = (samples[i] - MathFX.NearestPointStrict(from, to, samples[i])).sqrMagnitude;//The squared distance from the sample to the line
                        if (stroke)
                        {
                            if (dist <= strokeOuter && dist >= strokeInner)
                            {
                                c += strokeCol;
                            }
                            else if (dist < sqrW)
                            {
                                c += col;
                            }
                            else
                            {
                                c += pc;
                            }
                        }
                        else
                        {
                            if (dist < sqrW)
                            {//Is the distance smaller than the width of the line
                                c += col;
                            }
                            else
                            {
                                c += pc;//No it wasn't, set it to be the original colour
                            }
                        }
                    }
                    c /= samples.Length;//Get the avarage colour
                    pixels[y * (int)lengthX + x] = c;
                }
            }
        }
        tex.SetPixels((int)stX, (int)stY, (int)lengthX, (int)lengthY, pixels, 0);
        tex.Apply();
        //return tex;
    }

    void drawBressLine(Vector2 currPenLoc, Vector2 prevPenLoc, Color col, Texture2D tex)
    { 
        int x = (int)currPenLoc.x;
        int y = (int)currPenLoc.y;
        int x2 = (int)prevPenLoc.x;
        int y2 = (int)prevPenLoc.y;
        int w = x2 - x ;
        int h = y2 - y ;
        int dx1 = 0, dy1 = 0, dx2 = 0, dy2 = 0 ;
        if (w<0) dx1 = -1 ; else if (w>0) dx1 = 1 ;
        if (h<0) dy1 = -1 ; else if (h>0) dy1 = 1 ;
        if (w<0) dx2 = -1 ; else if (w>0) dx2 = 1 ;
        int longest = Mathf.Abs(w) ;
        int shortest = Mathf.Abs(h) ;
        if (!(longest>shortest)) 
        {
            longest = Mathf.Abs(h) ;
            shortest = Mathf.Abs(w) ;
            if (h<0) dy2 = -1 ; else if (h>0) dy2 = 1 ;
            dx2 = 0 ;            
        }
        int numerator = longest >> 1 ;
        for (int i=0;i<=longest;i++) 
        {
            //putpixel(x,y,color) ;
            tex.SetPixel(x, y, col);
            numerator += shortest ;
            if (!(numerator < longest)) 
            {
                numerator -= longest ;
                x += dx1 ;
                y += dy1 ;
            } else 
            {
                x += dx2 ;
                y += dy2 ;
            }
        }
        tex.Apply();
    }

}
