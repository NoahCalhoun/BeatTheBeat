using UnityEngine;
using UnityEngine.UI;

using System;

public class BBFarback : BBPassThrough
{
    Material Material = null;

    void Start ()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        Material = spriteRenderer.sharedMaterial;
        float speedFactor = Material.mainTextureScale.x / (Tm.localScale.x * Mathf.Abs(spriteRenderer.sprite.vertices[0].x) * 2);
        SpeedRate = 0.2f * speedFactor;

        //SpeedRate = spriteRenderer.sprite.rect.width * Material.mainTextureScale.x / (Tm.localScale.x * spriteRenderer.size.x);
        //Material = GetComponent<Image>().material;
        //SpeedRate = 1f;// / Rt.rect.width * Material.mainTextureScale.x / Rt.localScale.x;
    }

    protected override void GoThrough()
    {
        float newX = Material.mainTextureOffset.x + SpeedRelative * Time.deltaTime;
        if (newX > 1f)
            newX -= 1f;
        Material.mainTextureOffset = new Vector2(newX, 0f);
    }
}
