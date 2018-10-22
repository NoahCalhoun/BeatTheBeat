using UnityEngine;

public class BBBuilding : BBPassThrough
{
    private float DestroyX = 0f;

    private Material Material;

    public Sprite[] Sprites;

    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = Sprites[Random.Range(0, Sprites.Length - 1)];

        SpeedRate = 0.8f;

        float startX = Tm.parent.localPosition.x;
        DestroyX = -startX * 2f;
    }

    protected override void GoThrough()
    {
        var newPos = Tm.localPosition;
        newPos.x -= (SpeedRelative * Time.deltaTime);
        Tm.localPosition = newPos;

        if (newPos.x <= DestroyX)
            DestroyImmediate(Go);
    }
}
