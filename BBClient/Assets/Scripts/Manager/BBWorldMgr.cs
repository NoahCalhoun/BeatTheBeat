using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class BBWorldMgr : MonoBehaviour
{
    static private BBWorldMgr mInstance;
    static public BBWorldMgr Instance { get { if (mInstance == null) mInstance = GameObject.FindGameObjectWithTag("GameManager").GetComponent<BBWorldMgr>(); return mInstance;  } }

    public Transform StarRoot;
    public Transform BuildingRoot;
    public Transform FoeRoot;

    public float Speed = 5f;

    public Dictionary<FoeType, FoePreset> FoeDic = new Dictionary<FoeType, FoePreset>(EnumComparer<FoeType>.Instance);

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

    }
	
	void Update ()
    {
    }

    static public BBBuilding SpawnBuilding()
    {
        return Instance.SpawnBuilding_Native();
    }

    private BBBuilding SpawnBuilding_Native()
    {
        var wallObj = Instantiate(Resources.Load<GameObject>("Prefabs/Building"));
        var wall = wallObj.GetComponent<BBBuilding>();
        if (wall)
        {
            wall.Tm.SetParent(BuildingRoot, false);
        }
        return wall;
    }

    static public BBStar SpawnStar()
    {
        return Instance.SpawnStar_Native();
    }

    private BBStar SpawnStar_Native()
    {
        var starObj = Instantiate(Resources.Load<GameObject>("Prefabs/Star"));
        var star = starObj.GetComponent<BBStar>();
        if (star)
        {
            star.Tm.SetParent(StarRoot, false);
        }
        return star;
    }
    static public BBFoe SpawnFoe(FoeType type)
    {
        return Instance.SpawnFoe_Native(type);
    }

    private BBFoe SpawnFoe_Native(FoeType type)
    {
        var foeObj = Instantiate(Resources.Load("Prefabs/Foe")) as GameObject;
        var foe = foeObj.GetComponent<BBFoe>();
        if (foe)
        {
            foe.Tm.SetParent(FoeRoot, false);
            foe.InitFoe(FoeDic[type]);
        }
        return foe;
    }
}
