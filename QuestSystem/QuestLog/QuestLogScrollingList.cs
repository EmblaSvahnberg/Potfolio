using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

public class QuestLogScrollingList : MonoBehaviour
{
    public QuestList everyQuest;

    [Header("Components")]
    [SerializeField] private GameObject content;

    [Header("Rect Transforms")]
    [SerializeField] private RectTransform scrollRectTransform;
    [SerializeField] private RectTransform contentRectTransform;

    [Header("Quest Log Button")]
    [SerializeField] private GameObject questLogButtonPrefab;

    public Dictionary<string, QuestInfoSO> questLogs = new Dictionary<string, QuestInfoSO>();

    private QuestLogButton questLogButton;

    public QuestLogUI questLog;

    public void addQuestToList(QuestInfoSO currentQuestInList)
    {
        if(currentQuestInList.status != QuestState.UNDISCOVERED && currentQuestInList.status != QuestState.CAN_START)
        {
            if (!questLogs.ContainsValue(currentQuestInList))
            {
                //ad the quest info to the dictionary
                questLogs.Add(currentQuestInList.id, currentQuestInList);

                //Create the button
                questLogButton = Instantiate(questLogButtonPrefab, content.transform).GetComponent<QuestLogButton>();

                if (questLog.firstSelectedButton == null)
                {
                    questLog.firstSelectedButton = questLogButton.gameObject;
                }

                //game object name in the scene
                questLogButton.gameObject.name = currentQuestInList.id + "_button";
                questLogButton.questInfo = currentQuestInList;

                RectTransform buttonRectTransform = questLogButton.GetComponent<RectTransform>();
                UpdateScrolling(buttonRectTransform);
            }

        }
    }

    public void UpdateList()
    {
        //foreach (KeyValuePair<string, QuestInfoSO> loggedQuests in questLogs)
        //{
        //    if (loggedQuests.Key.Equals(currentQuestInList.id))
        //    {
        //        loggedQuests.Value.status = currentQuestInList.status;
        //        loggedQuests.Value.progressText = currentQuestInList.progressText;
        //    }
        //}
    }

    private void UpdateScrolling(RectTransform buttonRectTransform)
    {
        //calculate the min and max for the selected button
        float buttonYMin = Mathf.Abs(buttonRectTransform.anchoredPosition.y);
        float buttonYMax = buttonYMin + buttonRectTransform.rect.height;

        //calculate the min and max for the content area
        float contentYMin = contentRectTransform.anchoredPosition.y;
        float contentYMax = contentYMin + scrollRectTransform.rect.height;

        //Handle scrolling down
        if (buttonYMax > contentYMax)
        {
            contentRectTransform.anchoredPosition = new Vector2(contentRectTransform.anchoredPosition.x, buttonYMax - scrollRectTransform.rect.height);
        }
        //handle scrolling up
        else if (buttonYMin < contentYMin)
        {
            contentRectTransform.anchoredPosition = new Vector2(contentRectTransform.anchoredPosition.x, buttonYMin);
        }
    }



}
