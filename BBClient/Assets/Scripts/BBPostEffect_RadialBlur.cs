using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class BBPostEffect_RadialBlur : MonoBehaviour
{
    public Material Material;

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Material.SetTexture("_MainTex", source);
        Material.SetFloat("fSampleDist", 1f);
        Material.SetFloat("fSampleStrength", 2.2f);
        Material.SetFloat("fCenterU", 0.25f);
        Material.SetFloat("fCenterV", 0.25f);

        Graphics.Blit(null, destination, Material);
    }
}
