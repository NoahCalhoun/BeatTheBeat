using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class BBStar : BBPassThrough
{
    private float DestroyX = 0f;

    public float MinScale = 3f;
    public float MaxScale = 6f;

    public float HeightMax = 6f;

    // Use this for initialization
    void Start ()
    {
        SpeedRate = 0.1f;

        float scale = Random.Range(MinScale * 10f, MaxScale * 10f) * 0.1f;
        Tm.localScale = new Vector3(scale, scale);

        float height = Random.Range(0f, HeightMax * 1000f) * 0.001f;
        Tm.localPosition = new Vector3(0f, height);

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
