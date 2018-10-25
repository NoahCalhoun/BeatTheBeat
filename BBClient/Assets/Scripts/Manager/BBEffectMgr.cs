using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BBEffectMgr : MonoBehaviour
{
    static private BBEffectMgr mInstance;
    static public BBEffectMgr Instance { get { if (mInstance == null) mInstance = GameObject.FindGameObjectWithTag("GameManager").GetComponent<BBEffectMgr>(); return mInstance; } }

    public BBPostEffect_RadialBlur RadialBlur;
    public BBPostEffect_RGBSeparate RGBSeparate;

    public bool Use_RadialBlur = true;
    public bool Use_RGBSeparate = true;
    
    void Start ()
    {
		
	}
	
	void Update ()
    {
		if (Input.GetMouseButton(1))
        {
            if (Use_RadialBlur)
            {
                var world = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                var screen = Camera.main.WorldToScreenPoint(world);
                Vector2 uv;
                uv.x = screen.x / BBUIMgr.Instance.ScaledWidth;
                uv.y = screen.y / BBUIMgr.Instance.ScaledHeight;
                RadialBlur.StartBlur(uv);
            }

            if (Use_RGBSeparate)
            {
                RGBSeparate.StartSeparate();
            }
        }

        RadialBlur.enabled = RadialBlur.Working != null;
        RGBSeparate.enabled = RGBSeparate.Working != null;

    }
}
