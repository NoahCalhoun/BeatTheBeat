using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class BBUIMgr : MonoBehaviour
{
    static private BBUIMgr mInstance;

    static public BBUIMgr Instance { get { if (mInstance == null) mInstance = GameObject.FindGameObjectWithTag("GameManager").GetComponent<BBUIMgr>(); return mInstance; } }

    public Canvas Canvas;
    public float Height { get; private set; }
    public float Width { get; private set; }
    public float Scale { get; private set; }

    public float ScaledHeight { get { return Height * Scale; } }
    public float ScaledWidth { get { return Width * Scale; } }

    // Use this for initialization
    void Start ()
    {
        if (Canvas)
        {
            Scale = (Canvas.transform as RectTransform).rect.width / 1348f;

            Canvas.GetComponent<CanvasScaler>().scaleFactor = Scale;
            Height = Canvas.pixelRect.height;
            Width = Canvas.pixelRect.width;
        }

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
