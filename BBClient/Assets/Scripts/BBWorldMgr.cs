﻿using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class BBWorldMgr : MonoBehaviour
{
    static private BBWorldMgr mInstance;
    static public BBWorldMgr Instance { get { if (mInstance == null) mInstance = GameObject.FindGameObjectWithTag("WorldRoot").GetComponent<BBWorldMgr>(); return mInstance;  } }

    public Canvas Canvas;

    public float Height { get; private set; }
    public float Width { get; private set; }

    public Transform BackgroundRoot;

    public float Speed = 1000f;

    // Use this for initialization
    void Start ()
    {
        if (Canvas)
        {
            float scaleFactor = (Canvas.transform as RectTransform).rect.height / 720f;

            Canvas.GetComponent<CanvasScaler>().scaleFactor = scaleFactor;
            Height = Canvas.pixelRect.height / scaleFactor;
            Width = Canvas.pixelRect.width / scaleFactor;
        }

        var wallPref = Resources.Load<GameObject>("Prefabs/Wall");
        var wallObj = Instantiate(wallPref);
        wallObj.GetComponent<BBBase>().Tm.SetParent(BackgroundRoot, false);

    }
	
	// Update is called once per frame
	void Update ()
    {
    }
}
