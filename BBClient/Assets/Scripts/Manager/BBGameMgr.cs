using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class BBGameMgr : MonoBehaviour
{
    static private BBGameMgr mInstance;

    static public BBGameMgr Instance { get { if (mInstance == null) mInstance = GameObject.FindGameObjectWithTag("GameManager").GetComponent<BBGameMgr>(); return mInstance; } }

    public Queue<BBFoe> FoeQueue = new Queue<BBFoe>();

    public BBFoe HeadFoe { get { return FoeQueue.Count > 0 ? FoeQueue.Peek() : null; } }
    private BBFoe TailFoe;
    public Transform HeadPosition;

	// Use this for initialization
	void Start ()
    {
        BBWorldMgr.SpawnStar();
        BBWorldMgr.SpawnBuilding();

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
        UpdateHeadFoePosition();

        HandleControl();

    }

    void EnqueueFoe(FoeType type)
    {
        var foe = BBWorldMgr.SpawnFoe(type);
        FoeQueue.Enqueue(foe);

        if (TailFoe)
            foe.FrontFoe = TailFoe;
        TailFoe = foe;
    }

    void UpdateHeadFoePosition()
    {
        if (HeadFoe == null)
            return;

        HeadFoe.PositionUpdated = false;

        if (Mathf.Abs(HeadFoe.Tm.localPosition.x - HeadPosition.localPosition.x) < 0.0001f)
            return;

        float nextHeadPos = Mathf.Max(HeadFoe.Tm.localPosition.x - BBWorldMgr.Instance.Speed * Time.deltaTime, HeadPosition.localPosition.x);
        HeadFoe.Tm.localPosition = new Vector3(nextHeadPos, 0f);
        HeadFoe.PositionUpdated = true;
    }

    void HandleControl()
    {
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

    static public void OnControl(ControlType type)
    {
        Instance.OnControlled(type);
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
