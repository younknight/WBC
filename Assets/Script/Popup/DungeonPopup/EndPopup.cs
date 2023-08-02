using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class EndPopup : Popup
{
    [SerializeField] SceneMoveManager sceneMoveManager;
    [SerializeField] TextMeshProUGUI isWinText;
    [SerializeField] GameObject winningObj;
    [SerializeField] GameObject loseObj;
    bool isWin;
    bool isStory;
    bool isLastStage;
    MapInfo mapinfo;



    static EndPopup instance;
    public static EndPopup Instance { get => instance; }
    private void OnDestroy() { instance = null; }
    private void Awake() { if (instance == null) instance = this; }
    public void Setup(bool isWin, bool isStory, MapInfo mapInfo)
    {
        this.mapinfo = mapInfo;
        this.isStory = isStory;
        this.isWin = isWin;
        isLastStage = mapInfo.isLastStage;
        winningObj.SetActive(false);
        loseObj.SetActive(false);
        if (isWin) Win();
        else Lose();
        Open();
    }

    void Win()
    {
        isWinText.text = "�¸�!";
        winningObj.SetActive(true);
    }
    void Lose()
    {
        isWinText.text = "�й�";
        loseObj.SetActive(true);
    }
    public void Confirm()
    {
        if (isWin)
        {
            //�ܼ� �¸�
            Acquisition();
            if (mapinfo.id >= StoryManager.Instance.StoryData.mapLockProgress[mapinfo.world])//���� Ŭ����
            {
                StoryManager.Instance.AddMapProgress(mapinfo.world);
            }
            if (isStory)//���丮 ����
            {
                if (isLastStage) StoryManager.Instance.AddWolrdProgress();
                sceneMoveManager.MoveScene("Story");
                return;
            }
            sceneMoveManager.MoveScene("Main");
        }
        else { sceneMoveManager.MoveScene("Main"); }
    }
    void Acquisition()
    {

    }
}
