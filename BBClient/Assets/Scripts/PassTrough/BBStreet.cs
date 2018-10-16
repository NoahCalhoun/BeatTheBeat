using UnityEngine;
using UnityEngine.UI;

public class BBStreet : BBPassThrough
{
    Material Material = null;

    void Start()
    {
        Material = GetComponent<Image>().material;
        SpeedRate = 1f / Rt.rect.width * Material.mainTextureScale.x / Rt.localScale.x;
    }

    protected override void GoThrough()
    {
        float newX = Material.mainTextureOffset.x + SpeedRelative * Time.deltaTime;
        if (newX > 1f)
            newX -= 1f;
        Material.mainTextureOffset = new Vector2(newX, 0f);
    }
}
