using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BBWorldMgr : MonoBehaviour
{
    static private BBWorldMgr mInstance;
    static public BBWorldMgr Instance { get { if (mInstance == null) mInstance = GameObject.FindGameObjectWithTag("WorldRoot").GetComponent<BBWorldMgr>(); return mInstance;  } }

    public Canvas Canvas;

    public float Height { get { return Canvas ? Canvas.pixelRect.height : 0f; } }
    public float Width { get { return Canvas ? Canvas.pixelRect.width : 0f; } }

    public Transform BackgroundRoot;

    public float Speed = 1000f;

    // Use this for initialization
    void Start ()
    {
        var wallPref = Resources.Load<GameObject>("Prefabs/Wall");
        var wallObj = Instantiate(wallPref);
        wallObj.GetComponent<BBBase>().Tm.SetParent(BackgroundRoot, false);

    }
	
	// Update is called once per frame
	void Update ()
    {
    }
}
