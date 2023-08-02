using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] TutorialPannel TutorialPannel;
    [SerializeField] DialogueTextManager dialogue;
    void Start()
    {
        TutorialPannel.gameObject.SetActive(false);
        if (StoryManager.Instance.StoryData.progress == 1) { StartTutorial(); }
    }
    void StartTutorial()
    {
        TutorialPannel.gameObject.SetActive(true);
        dialogue.id = StoryManager.Instance.StoryData.progress;
        dialogue.index = 0;
        dialogue.Talk();
    }
    public void Talk()
    {
        dialogue.Talk();
        if(!dialogue.isAction) TutorialPannel.gameObject.SetActive(false);
    }
}
