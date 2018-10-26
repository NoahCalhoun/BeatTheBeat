using System.Collections;

using UnityEngine;

public class BBPostEffect_RadialBlur : BBPostEffectBase
{
    public class RadialBlurData : PostEffectData
    {
        public Vector2 Center;
    }

    private float CurrentStrength = 0f;
    private Vector2 Center;

    public float SampleDist = 0.5f;
    public float SampleStrengthMax = 2.2f;
    public float EffectTime = 0.5f;

    protected override string MaterialName { get { return "BBRadialBlur"; } }

    override public void BeginEffect(PostEffectData database = null)
    {
        base.BeginEffect();

        var data = database as RadialBlurData;
        if (data != null)
            Center = data.Center;
    }

    protected override IEnumerator PostEffect()
    {
        CurrentStrength = SampleStrengthMax;
        while (CurrentStrength > 0f)
        {
            CurrentStrength = Mathf.Max(CurrentStrength - (Time.deltaTime * SampleStrengthMax / EffectTime), 0f);
            yield return null;
        }

        Working = null;
        CurrentStrength = 0f;
    }

    protected override void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Material.SetTexture("_MainTex", source);
        Material.SetFloat("fSampleDist", SampleDist);
        Material.SetFloat("fSampleStrength", CurrentStrength);
        Material.SetFloat("fCenterU", Center.x);
        Material.SetFloat("fCenterV", Center.y);

        Graphics.Blit(null, destination, Material);
    }
}
