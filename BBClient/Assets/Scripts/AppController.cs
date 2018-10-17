using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class AppController : MonoBehaviour
{
	// Use this for initialization
	void Start ()
    {
        Application.targetFrameRate = 60;

        var canvasObj = GameObject.FindGameObjectWithTag("Canvas");
        
        canvasObj.GetComponent<CanvasScaler>().scaleFactor = (canvasObj.transform as RectTransform).rect.height / 1080f;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
