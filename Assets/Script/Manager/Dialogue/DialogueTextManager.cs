using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DialogueTextManager : MonoBehaviour
{
    DialogueDatabase dialogueData;
    [SerializeField] SceneMoveManager sceneMoveManager;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TypeEffect dialogueText;
    [SerializeField] Image characterPortrait;
    [SerializeField] Image background;
    public int id = 0;//--------------------------------ÀÎµ¦½º 
    public int index = 0;//-------------
    public bool isAction = true;//
    private void Awake()
    {
        dialogueData = GetComponent<DialogueDatabase>();
        id = StoryManager.Instance.StoryData.progress;
    }
    public void Talk()
    {
        string talkDataContext = dialogueData.GetDialogueContetxt(id, index);
        string talkDataName = dialogueData.GetDialogueName(id, index);
        if(talkDataContext == null)
        {
            isAction = false;
            StoryManager.Instance.AddProgress();
            if(id != 1) sceneMoveManager.MoveScene("Main");//Æ©Åä¸®¾ó

            return;
        }
        nameText.text = talkDataName;
        characterPortrait.sprite = Resources.Load<Sprite>("NPC/" + talkDataName);
        dialogueText.Setup(talkDataContext);

        isAction = true;
        index++;
    }
}
