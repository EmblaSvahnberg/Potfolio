using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class QuestList : MonoBehaviour, IDataPersistence
{
    public QuestLogScrollingList questScroll;

    public QuestInfoSO[] questAll;
    private Dictionary<string, QuestInfoSO> questDictionary = new Dictionary<string, QuestInfoSO>();

    private void Awake()
    {
        questAll = Resources.LoadAll<QuestInfoSO>("Quest");

        foreach (QuestInfoSO quest in questAll)
        {
            quest.ResetQuest();
            questDictionary.Add(quest.id, quest);
        }

    }

    public void LoadData(GameData data)
    {
        
        foreach(QuestSaveData save in data.savedQuests)
        {
            QuestInfoSO quest = questDictionary[save.questId];

            quest.status = save.currentState;
            quest.progressText = save.currentProgress;
            questScroll.addQuestToList(quest);
        }
        data.savedQuests.Clear();
    }

    public void SaveData(GameData data) 
    {
        foreach(KeyValuePair<string, QuestInfoSO> quest in questDictionary)
        {
            QuestSaveData dataToSave = new QuestSaveData();

            dataToSave.questId = quest.Value.id;
            dataToSave.currentState = quest.Value.status;
            dataToSave.currentProgress = quest.Value.progressText;

            data.savedQuests.Add(dataToSave);
        }
    }
}
