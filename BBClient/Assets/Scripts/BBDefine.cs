using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public enum ControlType
{
    Left,
    Right,
    SlideUp,
    SlideDown,
    SlideLeft,
    SlideRight,
}

public enum BeatType
{
    Anybeat,
    Punch,
    Kick,
    Straight,
    Gatling,
}

public enum FoeType
{
    None,
    Red,
    Green,
    Blue,
    Black,
}

public enum BeatResult
{
    Failed,
    Succeed,
    Defeat,
    Error,
}

public struct FoePreset
{
    public FoeType Type;
    public BeatType[] Beats;
    public int Count { get; private set; }

    public void SetInfo(FoeType type, int count, params BeatType[] beats)
    {
        Type = type;
        Count = count;
        Beats = new BeatType[Count];
        if (Type == FoeType.Black)
        {
            for (int i = 0; i < Beats.Length; ++i)
                Beats[i] = BeatType.Gatling;
        }
        else if (beats.Length > 0)
        {
            int minCnt = Mathf.Min(Beats.Length, beats.Length);
            for (int i = 0; i < minCnt; ++i)
                Beats[i] = beats[i];
        }
        else
        {
            Type = FoeType.None;
        }
    }
}

public static class BBDefine
{

}
