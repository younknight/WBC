using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueDatabase : MonoBehaviour
{
    public Dictionary<int, Dialogue> dialogueDic = new Dictionary<int, Dialogue>();

    [SerializeField] Dialogue[] dialogues;//
    private void Awake()
    {
        
        DialogueParser parser = GetComponent<DialogueParser>();
        dialogues = parser.Parse("Dialogue.json");
        for (int i = 0; i < dialogues.Length; i++)
        {
            dialogueDic.Add(i, dialogues[i]);
        }
    }
    public string GetDialogueName(int id, int index)//id = 씬 넘버, index = 대화 넘버
    {
        if (dialogueDic.ContainsKey(id))
        {
            if (index == dialogueDic[id].contexts.Length) return null;
            return dialogueDic[id].contexts[index].name;
        }
        return null;
    }
    public string GetDialogueContetxt(int id, int index)//id = 씬 넘버, index = 대화 넘버
    {
        if (dialogueDic.ContainsKey(id))
        {
            if (index == dialogueDic[id].contexts.Length) return null;
            return dialogueDic[id].contexts[index].context;
        }
        return null;
    }
}
