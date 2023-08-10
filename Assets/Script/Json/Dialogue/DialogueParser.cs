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
    public List<line> lines;

    public DialogueJson(int scene, int index, string name)
    {
        this.scene = scene;
        this.index = index;
        this.name = name;
    }
}
[System.Serializable]
public struct line
{
    public string name;
    public string background;
    public string context;

    public line(string name, string background, string context)
    {
        this.name = name;
        this.background = background;
        this.context = context;
    }
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
        if (!File.Exists(path))
        {
            SaveNewJson();

        }
        string loadJson = File.ReadAllText(path);
        List<DialogueJson> dialogueData = jsonParser.JsonToOject<List<DialogueJson>>(loadJson);

        for (int i = 0; i < dialogueData.Count; i++)
        {
            Dialogue dialogue = new Dialogue();
            dialogue.name = dialogueData[i].name;
            List<line> contextList = new List<line>();
            for (int j = 0; j < dialogueData[i].lines.Count; j++)
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
        List<DialogueJson> dialogueJson = new List<DialogueJson>();
        line[] test = new line[3];
        DialogueJson dialogue;
        List<line> lines;
        #region 0. 메인 스토리
        dialogue = new DialogueJson(0, 0, "startStory");
        lines = new List<line>();
        lines.Add(new line("나레이션", "hut", "어느 숲속 작은 오두막,\n누군가가 주님 곁으로 가기 직전의 이야기다."));
        lines.Add(new line("할아버지", "home", "으어어, 손자야 거기 있는냐?"));
        lines.Add(new line("알렌", "home", "할아버지...\n저 여기 있어요"));
        lines.Add(new line("할아버지", "home", "내가 떠나기전에 네게 남길 것이 있단다."));
        lines.Add(new line("할아버지", "GetWBC", "이건 내가 가장 소중하게 여기던 상자란다."));
        lines.Add(new line("할아버지", "home", "이걸 받아주렴"));
        lines.Add(new line("알렌", "home", "이게 뭐죠?\n그냥 상자처럼 보이는데요"));
        lines.Add(new line("할아버지", "home", "어허! 이 대단한 상자를 그냥 상자라니, 그것보다 이 상자에는 큰 문제가 있단다."));
        lines.Add(new line("알렌", "home", "무슨 문제요?"));
        lines.Add(new line("할아버지", "home", "그 상자를 열어 보겠니?"));
        lines.Add(new line("알렌", "home", "..........\n잠겨있는데 열쇠는요?"));
        lines.Add(new line("할아버지", "home", ".........."));
        lines.Add(new line("알렌", "home", ".........."));
        lines.Add(new line("할아버지", "home", "열쇠가 도망갔다."));
        lines.Add(new line("알렌", "home", "..........\n잃어버린게 아니고요?"));
        lines.Add(new line("할아버지", "home", "콜록콜록\n세계 최고의 상자를 찾거라 그 안에 그 상자를 열수 있는 만능 열쇠가 들어있을 거란다. 꽥"));
        lines.Add(new line("알렌", "home", "할아버지!\n"));
        dialogue.lines = lines;
        dialogueJson.Add(dialogue);
        #endregion
        #region 1. 튜토리얼
        dialogue = new DialogueJson(1, 1, "tutorial");
        lines = new List<line>();
        lines.Add(new line("나레이션", "hut", "어느 숲속 작은 오두막,\n누군가가 주님 곁으로 가기 직전의 이야기다."));
        lines.Add(new line("할아버지", "home", "0번쨰로 이동"));
        dialogue.lines = lines;
        dialogueJson.Add(dialogue);
        #endregion
        #region 2. 불대돌파
        dialogue = new DialogueJson(2, 2, "불의 대지 돌파");
        lines = new List<line>();
        lines.Add(new line("알렌", "hut", "돌파 완료!"));
        dialogue.lines = lines;
        dialogueJson.Add(dialogue);
        #endregion

        jsonParser.SaveJson<List<DialogueJson>>(dialogueJson, path);
    }

}