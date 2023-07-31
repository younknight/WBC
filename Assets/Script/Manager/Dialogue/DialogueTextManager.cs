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
    public int id = 0;//--------------------------------¿Œµ¶Ω∫ 
    public int index = 0;//-------------
    public bool isAction = true;//
    private void Start()
    {
        dialogueData = GetComponent<DialogueDatabase>();
        id = StoryManager.Instance.StoryData.progress;
        Talk();
    }
    public void Talk()
    {
        string talkDataContext = dialogueData.GetDialogueContetxt(id, index);
        string talkDataName = dialogueData.GetDialogueName(id, index);

        if(talkDataContext == null)
        {
            isAction = false;
            StoryManager.Instance.AddProgress();
            if(id == 0) sceneMoveManager.MoveScene("Main");

            return;
        }
        nameText.text = talkDataName;
        characterPortrait.sprite = Resources.Load<Sprite>("NPC/" + talkDataName);
        dialogueText.Setup(talkDataContext);

        isAction = true;
        index++;
    }
}
