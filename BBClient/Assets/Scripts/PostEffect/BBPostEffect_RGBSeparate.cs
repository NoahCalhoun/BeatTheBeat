using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class BBPostEffect_RGBSeparate : MonoBehaviour
{
    private Material Material;

    private const float SampleDist = 0.005f;
    private float CurrentDist = 0f;

    public Coroutine Working { get; private set; }

    void Start()
    {
        Material = Resources.Load<Material>("Materials/BBRGBSeparate");
    }

    public void StartSeparate()
    {
        if (Working != null)
        {
            StopCoroutine(Working);
            Working = null;
        }
        
        Working = StartCoroutine(RGBSeparate());
    }

    private IEnumerator RGBSeparate()
    {
        CurrentDist = SampleDist;
        while (CurrentDist > 0f)
        {
            CurrentDist = Mathf.Max(CurrentDist - (Time.deltaTime * SampleDist * 2f), 0f);
            yield return null;
        }

        Working = null;
        CurrentDist = 0f;
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Material.SetTexture("_MainTex", source);
        Material.SetFloat("fSampleDist", CurrentDist);

        Graphics.Blit(null, destination, Material);
    }
}
