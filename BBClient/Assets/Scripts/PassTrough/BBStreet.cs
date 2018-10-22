using UnityEngine;
using UnityEngine.UI;

public class BBStreet : BBPassThrough
{
    Material Material = null;

    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        Material = spriteRenderer.sharedMaterial;
        float speedFactor = Material.mainTextureScale.x / (Tm.localScale.x * Mathf.Abs(spriteRenderer.sprite.vertices[0].x) * 2);
        SpeedRate = speedFactor;
    }

    protected override void GoThrough()
    {
        float newX = Material.mainTextureOffset.x + SpeedRelative * Time.deltaTime;
        if (newX > 1f)
            newX -= 1f;
        Material.mainTextureOffset = new Vector2(newX, 0f);
    }
}
