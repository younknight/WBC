using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    static EnemySpawner instance;
    [SerializeField] GameObject enemyHp;
    [SerializeField] Transform canvasTransform;
    [SerializeField] MapInfo mapInfo;
    [SerializeField] Transform[] wayPoints;
    [SerializeField] PlayerMovement player;
    List<List<Unit>> enemies = new List<List<Unit>>();
    int maxRound;
    int currentRound = 0;

    public MapInfo MapInfo { get => mapInfo; set => mapInfo = value; }
    public static EnemySpawner Instance { get => instance; }
    public List<List<Unit>> Enemies { get => enemies; set => enemies = value; }
    private void OnDestroy() { instance = null; }
    private void Awake() { if (instance == null) instance = this; }
    private void Start()
    {
        if (MapManager.SelectedMap) SetUp(MapManager.SelectedMap);
    }
    public List<Unit> GetEnemyList()
    {
        return enemies[currentRound];
    }
    public void SetUp(MapInfo mapInfo)
    {
        this.mapInfo = mapInfo;
        maxRound = mapInfo.enenmies.Count;
        for (int roundIndex = 0;roundIndex < maxRound && roundIndex < wayPoints.Length; roundIndex++)
        {
            List<Unit> enemieyList = new List<Unit>();
            for(int j = 0;j < mapInfo.enenmies[roundIndex].enemyInfos.Count; j++)
            {
                var newEnemy = Instantiate(mapInfo.enenmies[roundIndex].enemyInfos[j], wayPoints[roundIndex].position, Quaternion.identity).GetComponent<Unit>();
                newEnemy.RoundIndex = roundIndex;
                enemieyList.Add(newEnemy);
                SpawnHp(newEnemy.gameObject);
            }
            enemies.Add(enemieyList);
        }
    }
    void SpawnHp(GameObject enemy)
    {
        //Debug.Log("ASdasd");
        GameObject sliderClone = Instantiate(enemyHp);
        sliderClone.transform.SetParent(canvasTransform);
        sliderClone.transform.localScale = Vector3.one;
        sliderClone.GetComponent<HpPositionSetter>().Setup(enemy.transform);
        sliderClone.GetComponent<HpManager>().Setup(enemy.GetComponent<Unit>());
        sliderClone.transform.SetAsFirstSibling();
    }
    public void DestroyEnemy(Unit enemy, int round)
    {
        enemies[round].Remove(enemy);
        if(enemies[round].Count == 0)
        {
            currentRound++;
            //전부 처리완료
            if(currentRound >= maxRound)
            {
                player.Stop();
                EndPopup.Instance.Setup(true, mapInfo.isStory >= StoryManager.Instance.StoryData.progress, mapInfo);
                return;
            }
            player.GO();
        }
    }
}
