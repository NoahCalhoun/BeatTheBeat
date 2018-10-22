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

    // Use this for initialization
    void Start ()
    {
        if (Canvas)
        {
            float scaleFactor = (Canvas.transform as RectTransform).rect.width / 1348f;

            Canvas.GetComponent<CanvasScaler>().scaleFactor = scaleFactor;
            Height = Canvas.pixelRect.height / scaleFactor;
            Width = Canvas.pixelRect.width / scaleFactor;
        }

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
