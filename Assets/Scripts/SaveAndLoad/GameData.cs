using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData
{
    [SerializeField]
    public Dictionary<string, int> ScoreTable;

    public GameData()
    {

    }
    public GameData (ScoreManager scoreMgr)
    {
        ScoreTable = scoreMgr.highScores;
    }
}
