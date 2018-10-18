using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class BBGameMgr : MonoBehaviour
{
    static private BBGameMgr mInstance;

    static public BBGameMgr Instance { get { if (mInstance == null) mInstance = GameObject.FindGameObjectWithTag("GameManager").GetComponent<BBGameMgr>(); return mInstance; } }

    public Queue<BBFoe> FoeQueue = new Queue<BBFoe>();

    public Dictionary<FoeType, FoePreset> FoeDic = new Dictionary<FoeType, FoePreset>(EnumComparer<FoeType>.Instance);

    public BBFoe HeadFoe { get { return FoeQueue.Count > 0 ? FoeQueue.Peek() : null; } }

    public BBFoe TailFoe;

    public Transform FoeRoot;

	// Use this for initialization
	void Start ()
    {
        var preset = new FoePreset();
        preset.SetInfo(FoeType.Red, 3, BeatType.Punch, BeatType.Kick, BeatType.Punch);
        FoeDic.Add(FoeType.Red, preset);

        preset = new FoePreset();
        preset.SetInfo(FoeType.Green, 3, BeatType.Kick, BeatType.Kick, BeatType.Punch);
        FoeDic.Add(FoeType.Green, preset);

        preset = new FoePreset();
        preset.SetInfo(FoeType.Blue, 3, BeatType.Kick, BeatType.Punch, BeatType.Kick);
        FoeDic.Add(FoeType.Blue, preset);

        preset = new FoePreset();
        preset.SetInfo(FoeType.Black, 5);
        FoeDic.Add(FoeType.Black, preset);

        while (FoeQueue.Count < 10)
        {
            var value = Random.Range(0, 99);
            if (value < 30)
            {
                EnqueueFoe(FoeType.Red);
            }
            else if (value > 60)
            {
                EnqueueFoe(FoeType.Green);
            }
            else if (value < 90)
            {
                EnqueueFoe(FoeType.Blue);
            }
            else
            {
                EnqueueFoe(FoeType.Black);
            }
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (HeadFoe)
            HeadFoe.Rt.localPosition = new Vector3(-1800f, 0f);

        if (Input.GetKeyDown(KeyCode.A))
        {
            OnControlled(ControlType.Left);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            OnControlled(ControlType.Right);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            OnControlled(ControlType.SlideRight);
        }
	}

    void EnqueueFoe(FoeType type)
    {
        var foeObj = Instantiate(Resources.Load("Prefabs/Foe")) as GameObject;
        var foe = foeObj.GetComponent<BBFoe>();
        foe.Tm.SetParent(FoeRoot, false);
        FoeQueue.Enqueue(foe);
        foe.InitFoe(FoeDic[type]);

        if (TailFoe)
            foe.FrontFoe = TailFoe;
        TailFoe = foe;
    }

    void OnControlled(ControlType type)
    {
        if (HeadFoe)
        {
            BeatType beat = type == ControlType.Left ? BeatType.Punch : BeatType.Kick;
            beat = type == ControlType.SlideRight ? BeatType.Straight : beat;
            beat = ((beat == BeatType.Kick || beat == BeatType.Punch) && HeadFoe.Type == FoeType.Black) ? BeatType.Gatling : beat;

            switch (HeadFoe.OnBeat(beat))
            {
                case BeatResult.Defeat:
                    {
                        Debug.Log("Defeat!!!");
                        DestroyImmediate(FoeQueue.Dequeue().Go);
                        if (FoeQueue.Count > 0)
                            FoeQueue.Peek().FrontFoe = null;
                        else
                            TailFoe = null;
                    }
                    break;

                case BeatResult.Succeed:
                    {
                        Debug.Log("Succeed!");
                    }
                    break;

                case BeatResult.Failed:
                    {
                        Debug.Log("Failed...");
                    }
                    break;
            }
        }
    }
}
