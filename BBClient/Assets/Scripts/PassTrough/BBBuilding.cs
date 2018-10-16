using UnityEngine;

public class BBBuilding : BBPassThrough
{
    private float DestroyX = 0f;

    void Start()
    {
        SpeedRate = 0.8f;

        float startX = (Tm.parent as RectTransform).offsetMax.x;
        DestroyX = -BBWorldMgr.Instance.Width - startX * 2f;
    }

    protected override void GoThrough()
    {
        var newPos = Rt.localPosition;
        newPos.x -= (BBWorldMgr.Instance.Speed * Time.deltaTime);
        Rt.localPosition = newPos;

        if (newPos.x <= DestroyX)
            DestroyImmediate(Go);
    }
}
