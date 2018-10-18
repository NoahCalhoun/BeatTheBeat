using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class BBGameMgr : MonoBehaviour
{
    static private BBGameMgr mInstance;

    static public BBGameMgr Instance { get { if (mInstance == null) mInstance = GameObject.FindGameObjectWithTag("GameManager").GetComponent<BBGameMgr>(); return mInstance; } }

    public Queue<BBFoe> FoeQueue = new Queue<BBFoe>();

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnControlled(ControlType type)
    {

    }
}
