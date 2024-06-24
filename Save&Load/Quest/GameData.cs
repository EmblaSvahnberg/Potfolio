using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[System.Serializable]
public class GameData
{
    public long lastUpdated;

    public string currentScene;
    public bool movingFromMenu;

    public Vector3 playerPosition;

    public List<QuestSaveData> savedQuests;

    //the values defined in this constructur will be the default values the game starts with when there´s no data to load
    public GameData()
    {
        currentScene = null;
        playerPosition = new Vector3(-60, 7, -5);
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
