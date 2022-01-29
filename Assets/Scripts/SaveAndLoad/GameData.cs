using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData
{
    [SerializeField]
    public Dictionary<string, int> ScoreTable;
    [SerializeField]
    public float musicVolume;
    [SerializeField]
    public float sfxVolume;

    public GameData()
    {

    }
    public GameData (ScoreManager scoreMgr)
    {
        ScoreTable = scoreMgr.highScores;
        musicVolume = scoreMgr.musicLevel;
        sfxVolume = scoreMgr.sfxLevel;
    }
}
