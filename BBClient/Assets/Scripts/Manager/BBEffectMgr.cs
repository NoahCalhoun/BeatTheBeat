using UnityEngine;

public class BBEffectMgr : MonoBehaviour
{
    static private BBEffectMgr mInstance;
    static public BBEffectMgr Instance { get { if (mInstance == null) mInstance = GameObject.FindGameObjectWithTag("GameManager").GetComponent<BBEffectMgr>(); return mInstance; } }

    private Camera EffectCamera;

    public BBPostEffect_RadialBlur RadialBlur;
    public BBPostEffect_RGBSeparate RGBSeparate;

    public bool Use_RadialBlur = true;
    public bool Use_RGBSeparate = true;
    
    void Start ()
    {
        EffectCamera = Camera.main;
    }
	
	void Update ()
    {
		if (Input.GetMouseButton(1))
        {
            Begin_RadialBlur_Implement(Vector2.zero);
            Begin_RGBSeparate_Implement();
        }

        SetEffectsActive();
    }

    void SetEffectsActive()
    {
        RadialBlur.enabled = RadialBlur.IsWorking;
        RGBSeparate.enabled = RGBSeparate.IsWorking;
    }

    static public void Begin_RadialBlur(Vector2 center)
    {
        Instance.Begin_RadialBlur_Implement(center);
    }

    static public void Begin_RGBSeparate()
    {
        Instance.Begin_RGBSeparate_Implement();
    }

    void Begin_RadialBlur_Implement(Vector2 center)
    {
        if (Use_RadialBlur == false)
            return;

        var world = EffectCamera.ScreenToWorldPoint(Input.mousePosition);
        var screen = EffectCamera.WorldToScreenPoint(world);
        Vector2 uv;
        uv.x = screen.x / BBUIMgr.Instance.Width;
        uv.y = screen.y / BBUIMgr.Instance.Height;
        RadialBlur.BeginEffect(new BBPostEffect_RadialBlur.RadialBlurData() { Center = uv });
    }

    void Begin_RGBSeparate_Implement()
    {
        if (Use_RGBSeparate == false)
            return;

        RGBSeparate.BeginEffect();
    }
}
