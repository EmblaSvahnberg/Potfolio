using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "QuestInfoSO", menuName = "Quest", order = 1)]
public class QuestInfoSO : ScriptableObject
{
    public string id;
    public string progressText;
    public QuestState status;

    public QuestInfoSO[] questPrerequisites;
    public string reward;
    public Image rewardSprite;

    public void ResetQuest()
    {
        status = QuestState.UNDISCOVERED;
        progressText = "";
    }

}
