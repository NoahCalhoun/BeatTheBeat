using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class AppController : MonoBehaviour
{
	void Start ()
    {
        Application.targetFrameRate = 60;
	}
	
	void Update ()
    {
		
	}

    void OnApplicationQuit()
    {
        Resources.Load<Material>("Materials/BBFarback").mainTextureOffset = Vector2.zero;
        Resources.Load<Material>("Materials/BBStreet").mainTextureOffset = Vector2.zero;
    }
}
