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
        List<Dialogue> dialogueList = new List<Dialogue>();//��� ����Ʈ 

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
        return dialogueList.ToArray();//�� ĳ������ ���� �迭�� ����
    }







    [ContextMenu("To Dialogue Json Data")]
    public void SaveNewJson()
    {
        string path = Path.Combine(Application.dataPath, "Dialogue.json");
        List<DialogueJson> dialogueJson = new List<DialogueJson>();
        line[] test = new line[3];
        DialogueJson dialogue;
        List<line> lines;
        #region 0. ���� ���丮
        dialogue = new DialogueJson(0, 0, "startStory");
        lines = new List<line>();
        lines.Add(new line("�����̼�", "hut", "��� ���� ���� ���θ�,\n�������� �ִ� ������ ���� ������ �̾߱��."));
        lines.Add(new line("�Ҿƹ���", "home", "�����, ���ھ� �ű� �ִ³�?"));
        lines.Add(new line("�˷�", "home", "�Ҿƹ���...\n�� ���� �־��"));
        lines.Add(new line("�Ҿƹ���", "home", "���� ���������� �װ� ���� ���� �ִܴ�."));
        lines.Add(new line("�Ҿƹ���", "GetWBC", "�̰� ���� ���� �����ϰ� ����� ���ڶ���."));
        lines.Add(new line("�Ҿƹ���", "home", "�̰� �޾��ַ�"));
        lines.Add(new line("�˷�", "home", "�̰� ����?\n�׳� ����ó�� ���̴µ���"));
        lines.Add(new line("�Ҿƹ���", "home", "����! �� ����� ���ڸ� �׳� ���ڶ��, �װͺ��� �� ���ڿ��� ū ������ �ִܴ�."));
        lines.Add(new line("�˷�", "home", "���� ������?"));
        lines.Add(new line("�Ҿƹ���", "home", "�� ���ڸ� ���� ���ڴ�?"));
        lines.Add(new line("�˷�", "home", "..........\n����ִµ� ����¿�?"));
        lines.Add(new line("�Ҿƹ���", "home", ".........."));
        lines.Add(new line("�˷�", "home", ".........."));
        lines.Add(new line("�Ҿƹ���", "home", "���谡 ��������."));
        lines.Add(new line("�˷�", "home", "..........\n�Ҿ������ �ƴϰ��?"));
        lines.Add(new line("�Ҿƹ���", "home", "�ݷ��ݷ�\n���� �ְ��� ���ڸ� ã�Ŷ� �� �ȿ� �� ���ڸ� ���� �ִ� ���� ���谡 ������� �Ŷ���. ��"));
        lines.Add(new line("�˷�", "home", "�Ҿƹ���!\n"));
        dialogue.lines = lines;
        dialogueJson.Add(dialogue);
        #endregion
        #region 1. Ʃ�丮��
        dialogue = new DialogueJson(1, 1, "tutorial");
        lines = new List<line>();
        lines.Add(new line("�����̼�", "hut", "��� ���� ���� ���θ�,\n�������� �ִ� ������ ���� ������ �̾߱��."));
        lines.Add(new line("�Ҿƹ���", "home", "0������ �̵�"));
        dialogue.lines = lines;
        dialogueJson.Add(dialogue);
        #endregion
        #region 2. �Ҵ뵹��
        dialogue = new DialogueJson(2, 2, "���� ���� ����");
        lines = new List<line>();
        lines.Add(new line("�˷�", "hut", "���� �Ϸ�!"));
        dialogue.lines = lines;
        dialogueJson.Add(dialogue);
        #endregion

        jsonParser.SaveJson<List<DialogueJson>>(dialogueJson, path);
    }

}