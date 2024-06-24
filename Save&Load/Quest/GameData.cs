using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[System.Serializable]
public class GameData
{
    public long lastUpdated;

    public List<QuestSaveData> savedQuests;

    //the values defined in this constructur will be the default values the game starts with when there´s no data to load
    public GameData()
    {
        savedQuests = new List<QuestSaveData>();
    }
}

[System.Serializable]
public class QuestSaveData
{
    public string questId;
    public QuestState currentState;
    public string currentProgress;
}
