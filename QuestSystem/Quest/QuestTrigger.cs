using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class QuestTrigger : MonoBehaviour
{
    public bool startQuest = false;
    public bool turnInQuest = false;

    public QuestInfoSO quest;
    public QuestLogScrollingList questscroll;

    public Sprite icon1, icon2;

    public SpriteRenderer IconLocation;

    private void Start()
    {
        Debug.Log(quest + "has " + quest.questPrerequisites.Length + " in Prerequisites");
    }



    private void Update()
    {
        switch (quest.status) 
        { 
            case (QuestState.UNDISCOVERED):
                //check if prequisite quest is done, if so change quest to can start

                for (int i = 0; i < quest.questPrerequisites.Length; i++)
                {
                    if (quest.questPrerequisites[i].status != QuestState.FINISHED)
                    {
                        break;
                    }
                    
                    if(i == quest.questPrerequisites.Length - 1)
                    {
                        quest.status = QuestState.CAN_START;
                    }
                }

                break;

            case (QuestState.CAN_START):
                //check if pickupQuest is true, if so change to in progress

                IconLocation.sprite = icon1;

                if(startQuest == true)
                {
                    quest.status = QuestState.IN_PROGRESS;
                    questscroll.addQuestToList(quest);
                }
                break;

            case (QuestState.IN_PROGRESS):
                //check if solution is sent, if so change to in progress

                IconLocation.sprite = icon2;

                //TODO - add Solution progress

                break;

            case (QuestState.CAN_FINISH):
                //check if finsiehdquest is true, if so change to finished

                IconLocation.sprite = icon2;

                if(turnInQuest == true)
                {
                    quest.status = QuestState.FINISHED;
                }
                break;

            case (QuestState.FINISHED):

                IconLocation.sprite = null;

                //TODO - remove QuestTrigger or set as unactive

                break;
        }
    }
}
