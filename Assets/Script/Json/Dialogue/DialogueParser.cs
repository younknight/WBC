using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class DialogueJson
{
    public int scene;
    public int index;
    public string name;
    public line[] lines;
}
[System.Serializable]
public struct line
{
    public string name;
    public string background;
    public string context;
}
[System.Serializable]
public class Dialogue
{
    public string name;
    public line[] contexts;
}
public class DialogueParser : MonoBehaviour
{
    JsonParser jsonParser = new JsonParser();


    public Dialogue[] Parse(string path)
    {
        List<Dialogue> dialogueList = new List<Dialogue>();//대사 리스트 

        path = Path.Combine(Application.dataPath, path);
        string loadJson = File.ReadAllText(path);
        List<DialogueJson> dialogueData = jsonParser.JsonToOject<List<DialogueJson>>(loadJson);

        for (int i = 0; i < dialogueData.Count; i++)
        {
            Dialogue dialogue = new Dialogue();
            dialogue.name = dialogueData[i].name;
            List<line> contextList = new List<line>();
            for (int j = 0; j < dialogueData[i].lines.Length; j++)
            {
                line newLine = new line();
                newLine.name = dialogueData[i].lines[j].name;
                newLine.background = dialogueData[i].lines[j].background;
                newLine.context = dialogueData[i].lines[j].context;
                contextList.Add(newLine);
            }
            dialogue.contexts = contextList.ToArray();
            dialogueList.Add(dialogue);
        }
        return dialogueList.ToArray();//각 캐릭터의 대사들 배열로 리턴
    }







    [ContextMenu("To Dialogue Json Data")]
    public void SaveNewJson()
    {
        string path = Path.Combine(Application.dataPath, "Dialogue.json");
        DialogueJson dialogueJson = new DialogueJson();
        line[] test = new line[3];
        test[0].context = "test0";
        test[0].name = "name0";
        test[1].context = "test1";
        test[1].name = "name1";
        test[2].context = "test2";
        test[2].name = "name2";
        dialogueJson.index = 0;
        dialogueJson.scene = 0;
        dialogueJson.name = "testScene";
        dialogueJson.lines = test;

        jsonParser.SaveJson<DialogueJson>(dialogueJson, path);
    }
}