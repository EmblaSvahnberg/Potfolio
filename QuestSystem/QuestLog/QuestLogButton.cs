using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEngine.EventSystems;

public class QuestLogButton : MonoBehaviour
{
    //public Button button;

    public QuestInfoSO questInfo;
    [SerializeField] private QuestLogUI questLogUI;

    private TextMeshProUGUI idButton;


    private void Start()
    {
        idButton = GetComponentInChildren<TextMeshProUGUI>();
        idButton.text = questInfo.id;
        
        questLogUI = GetComponentInParent<QuestLogUI>();

        //button = GetComponent<Button>();
    }

    public void clickedButton()
    {
        questLogUI.questDisplayNameText.text = questInfo.id;
        questLogUI.questProgressText.text = questInfo.progressText;
        questLogUI.rewardText.text = questInfo.reward;
        questLogUI.rewardImage = questInfo.rewardSprite;

    }

}
