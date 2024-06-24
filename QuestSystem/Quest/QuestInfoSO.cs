using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "QuestInfoSO", menuName = "Quest", order = 1)]
public class QuestInfoSO : ScriptableObject
{
    /*[field: SerializeField]*/
    public string id; /*{ get; private set; }*/
    public string displayName;
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
