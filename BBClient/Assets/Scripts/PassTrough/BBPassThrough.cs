using UnityEngine;

public abstract class BBPassThrough : BBBase
{
    protected abstract void GoThrough();

    public float SpeedRate { protected set; get; }

    public float SpeedRelative { get { return BBWorldMgr.Instance.Speed * SpeedRate; } }

    void Update()
    {
        GoThrough();
    }
}
