using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DanielLochner.Assets.SimpleScrollSnap;
public class TutorialManager : MonoBehaviour
{
    [SerializeField] TutorialPannel TutorialPannel;
    [SerializeField] DialogueTextManager dialogue;
    [SerializeField] SimpleScrollSnap scrollSnap;
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
        Debug.Log(dialogue.index);
    }
    public void Talk()
    {
        dialogue.Talk();
        Debug.Log(dialogue.index);
        TryEvent(dialogue.index);
        if(!dialogue.isAction) TutorialPannel.gameObject.SetActive(false);
    }
    void TryEvent(int index)
    {
        switch (index)
        {
            case 2:
                scrollSnap.GoToPanel(0);
                break;
        }
    }
}
