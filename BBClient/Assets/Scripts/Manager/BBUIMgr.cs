using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BBUIMgr : MonoBehaviour
{
    static private BBUIMgr mInstance;

    static public BBUIMgr Instance { get { if (mInstance == null) mInstance = GameObject.FindGameObjectWithTag("GameManager").GetComponent<BBUIMgr>(); return mInstance; } }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
