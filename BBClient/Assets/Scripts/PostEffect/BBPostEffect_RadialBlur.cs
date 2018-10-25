using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class BBPostEffect_RadialBlur : MonoBehaviour
{
    private Material Material;

    private const float SampleDist = 0.5f;
    private const float SampleStrengthMax = 2.2f;
    private float CurrentStrength = 0f;

    private Vector2 Center;

    public Coroutine Working { get; private set; }

    void Start()
    {
        Material = Resources.Load<Material>("Materials/BBRadialBlur");
    }

    public void StartBlur(Vector2 center)
    {
        if (Working != null)
        {
            StopCoroutine(Working);
            Working = null;
        }

        Center = center;
        Working = StartCoroutine(RadialBlur());
    }

    private IEnumerator RadialBlur()
    {
        CurrentStrength = SampleStrengthMax;
        while (CurrentStrength > 0f)
        {
            CurrentStrength = Mathf.Max(CurrentStrength - (Time.deltaTime * SampleStrengthMax * 2f), 0f);
            yield return null;
        }

        Working = null;
        CurrentStrength = 0f;
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Material.SetTexture("_MainTex", source);
        Material.SetFloat("fSampleDist", SampleDist);
        Material.SetFloat("fSampleStrength", CurrentStrength);
        Material.SetFloat("fCenterU", Center.x);
        Material.SetFloat("fCenterV", Center.y);

        Graphics.Blit(null, destination, Material);
    }
}
