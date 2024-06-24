using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuestLogUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject contentParent;

    [Header("Quest Info")]
     public TextMeshProUGUI questDisplayNameText;
     public TextMeshProUGUI questProgressText;
     public TextMeshProUGUI rewardText;
     public Image rewardImage;

    public GameObject firstSelectedButton;

    private void OnEnable()
    {
        GameEventsManager.instance.inputEvents.onQuestLogTogglePressed += QuestLogTogglePressed;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.inputEvents.onQuestLogTogglePressed -= QuestLogTogglePressed;
    }

    private void QuestLogTogglePressed()
    {
            if (contentParent.activeInHierarchy)
            {
                HideUI();
            }
            else
            {
                ShowUI();
            }
    }

    private void ShowUI()
    {
        contentParent.SetActive(true);
        //disable player from doing oter things
        GameEventsManager.instance.playerEvents.DisablePlayerMovement();
        GameEventsManager.instance.playerEvents.DisableCameraMovement();

        FirstSelectedButton();
    }

    private void HideUI()
    {
        contentParent.SetActive(false);
        //enable player to to things
        GameEventsManager.instance.playerEvents.EnablePlayerMovement();
        GameEventsManager.instance.playerEvents.EnableCameraMovement();
    }

    public void FirstSelectedButton()
    {
        //kolla vad som ligger i första buttons position
        //sätt att det är lika med firstSelectedButton om det är knapp

        if (firstSelectedButton != null)
        {

            //set Items button as first selected
            var eventSystem = EventSystem.current;
            eventSystem.SetSelectedGameObject(firstSelectedButton, new BaseEventData(eventSystem));

        }
    }
}
