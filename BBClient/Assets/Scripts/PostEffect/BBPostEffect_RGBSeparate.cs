using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class BBPostEffect_RGBSeparate : BBPostEffectBase
{
    protected override string MaterialName { get { return "BBRGBSeparate"; } }

    private float CurrentDist = 0f;

    public float SampleDist = 0.005f;
    public float EffectTime = 0.5f;

    protected override IEnumerator PostEffect()
    {
        CurrentDist = SampleDist;
        while (CurrentDist > 0f)
        {
            CurrentDist = Mathf.Max(CurrentDist - (Time.deltaTime * SampleDist / EffectTime), 0f);
            yield return null;
        }

        Working = null;
        CurrentDist = 0f;
    }

    protected override void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Material.SetTexture("_MainTex", source);
        Material.SetFloat("fSampleDist", CurrentDist);

        Graphics.Blit(null, destination, Material);
    }
}
