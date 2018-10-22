using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class BBFoe : BBCharacter
{
    public FoeType Type { get; private set; }
    private Queue<BeatType> BeatQueue;

    public BeatType CurrentBeat { get { return BeatQueue != null && BeatQueue.Count > 0 ? BeatQueue.Peek() : BeatType.Anybeat; } }

    public float FrontDist = 3f;
    public BBFoe FrontFoe;
    public bool PositionUpdated = false;

	void Start ()
    {
		
	}
	
	void Update ()
    {
        UpdatePositionByFrontFoe();
    }

    public void InitFoe(FoePreset preset)
    {
        Type = preset.Type;

        BeatQueue = new Queue<BeatType>(preset.Count);
        for (int i = 0; i < preset.Beats.Length; ++i)
        {
            BeatQueue.Enqueue(preset.Beats[i]);
        }
    }

    void UpdatePositionByFrontFoe()
    {
        if (FrontFoe == null)
            return;

        if (FrontFoe.PositionUpdated == false)
        {
            PositionUpdated = false;
            return;
        }
        
        Tm.localPosition = FrontFoe.Tm.localPosition + new Vector3(FrontDist, 0f);
        PositionUpdated = true;
    }

    public BeatResult OnBeat(BeatType type)
    {
        if (BeatQueue.Count <= 0)
        {
            return BeatResult.Error;
        }

        if (Type != FoeType.Black && type == BeatType.Straight)
            return OnDefeat();

        if (CheckBeat(type))
        {
            BeatQueue.Dequeue();

            return BeatQueue.Count > 0 ? OnSucceed() : OnDefeat();
        }
        else
        {
            return OnFailed();
        }
    }

    public bool CheckBeat(BeatType type)
    {
        if (Type != FoeType.Black && type == BeatType.Straight)
            return true;

        return type == BeatType.Anybeat || type == BeatQueue.Peek();
    }

    private BeatResult OnFailed()
    {
        return BeatResult.Failed;
    }

    private BeatResult OnSucceed()
    {
        return BeatResult.Succeed;
    }

    private BeatResult OnDefeat()
    {
        return BeatResult.Defeat;
    }
}
