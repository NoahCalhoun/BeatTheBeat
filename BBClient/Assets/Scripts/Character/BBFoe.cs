﻿using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class BBFoe : BBCharacter
{
    public FoeType Type { get; private set; }
    private Queue<BeatType> BeatQueue;

	void Start ()
    {
		
	}
	
	void Update ()
    {

    }

    public void InitFoe(ref FoePreset preset)
    {
        Type = preset.Type;

        BeatQueue = new Queue<BeatType>(preset.Count);
        for (int i = 0; i < preset.Beats.Length; ++i)
        {
            BeatQueue.Enqueue(preset.Beats[i]);
        }
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

    public BeatResult OnFailed()
    {
        return BeatResult.Failed;
    }

    public BeatResult OnSucceed()
    {
        return BeatResult.Succeed;
    }

    public BeatResult OnDefeat()
    {
        return BeatResult.Defeat;
    }
}
