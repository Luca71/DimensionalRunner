using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    public Dictionary<string, int> ScoreTable;

    public GameData (ScoreManager scoreMgr)
    {
        ScoreTable = scoreMgr.highScores;
    }
}
