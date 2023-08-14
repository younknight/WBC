using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    static EnemySpawner instance;
    public static bool isInfinity;
    [SerializeField] MapInfo mapInfo;
    [SerializeField] EnemyGroup[] wayPoints;
    [SerializeField] HpSpawner hpSpawner;
    [SerializeField] PlayerMovement player;
    [SerializeField] EnemyGauge enemyGauge;
    List<List<Unit>> enemies = new List<List<Unit>>();
    [SerializeField] int maxRound = 2;
    [SerializeField] int currentRound = 0;//
    int totalRound = 0;

    public MapInfo MapInfo { get => mapInfo; set => mapInfo = value; }
    public static EnemySpawner Instance { get => instance; }
    public List<List<Unit>> Enemies { get => enemies; set => enemies = value; }
    public int TotalRound { get => totalRound; set => totalRound = value; }
    public int CurrentRound { get => currentRound; set => currentRound = value; }

    private void OnDestroy()
    {
        instance = null;
    }
    private void Awake()
    {
        if (instance == null) instance = this;
        for(int i = 0; i< wayPoints.Length; i++)
        {
            wayPoints[i].SetRandomPoint();
        }
        if (!isInfinity)//老馆带傈 技泼
        {
            enemyGauge.gameObject.SetActive(false);
            if (MapManager.SelectedMap) SetUp(MapManager.SelectedMap);
        }
        else//公茄带傈 技泼
        {
            SetUp(mapInfo);
        }
    }
    public void Search()
    {
        foreach(Unit enemy in GetEnemyList())
        {
            enemy.gameObject.GetComponent<Attacker>().ChangeState(WeaponState.SearchTarget);
        }
    }
    public List<Unit> GetEnemyList()
    {
        if (currentRound >= maxRound) return null;
        return enemies[currentRound];
    }
    public void SetUp(MapInfo mapInfo)
    {
        this.mapInfo = mapInfo;
        if (!isInfinity)
        {
            maxRound = mapInfo.enenmies.Count;
            //for (int roundIndex = 0; roundIndex < maxRound && roundIndex < wayPoints.Length; roundIndex++)
            //{
            //    List<Unit> enemieyList = new List<Unit>();
            //    for (int j = 0; j < mapInfo.enenmies[roundIndex].enemyInfos.Count; j++)
            //    {
            //        var newEnemy = Instantiate(mapInfo.enenmies[roundIndex].enemyInfos[j], wayPoints[roundIndex].GetRandomPoint().position, Quaternion.identity).GetComponent<Unit>();
            //        newEnemy.RoundIndex = roundIndex;
            //        enemieyList.Add(newEnemy);
            //        hpSpawner.SpawnHp(newEnemy.gameObject);
            //    }
            //    enemies.Add(enemieyList);
            //}
        }
        else
        {
            maxRound = 2;
            //for (int roundIndex = 0; roundIndex < maxRound; roundIndex++)
            //{
            //    List<Unit> enemieyList = new List<Unit>();
            //    enemies.Add(enemieyList);
            //}
            //SpawnNextEnemy(0, 0);
        }
        for (int roundIndex = 0; roundIndex < 2; roundIndex++)
        {
            List<Unit> enemieyList = new List<Unit>();
            enemies.Add(enemieyList);
        }
        SpawnNextEnemy(0, 0);
    }
    public void SpawnNextEnemy(int spawnIndex, int round)
    {
        Debug.Log(round);
        wayPoints[spawnIndex].SetRandomPoint();
        for (int i = 0; i < mapInfo.enenmies[round].enemyInfos.Count; i++)
        {
            var newEnemy = Instantiate(mapInfo.enenmies[round].enemyInfos[i], wayPoints[spawnIndex].GetRandomPoint().position, Quaternion.identity).GetComponent<Unit>();
            newEnemy.RoundIndex = spawnIndex;
            enemies[spawnIndex].Add(newEnemy);
            hpSpawner.SpawnHp(newEnemy.gameObject);
        }
    }
    public void DestroyEnemy(Unit enemy, int round)
    {
        if(!isInfinity) enemies[round].Remove(enemy);
        else enemies[currentRound].Remove(enemy);
        if (enemies[round].Count == 0)
        {
            Attacker.CanAttack = false;
            totalRound++;
            currentRound = ReverseTurn(currentRound);
            if (!isInfinity)
            {
                //Debug.Log(currentRound);
                //傈何 贸府肯丰
                if (totalRound >= maxRound)
                {
                    player.Stop();
                    EndPopup.Instance.Setup(true, mapInfo.isStory >= StoryManager.Instance.StoryData.progress, mapInfo);
                    return;
                }
                SpawnNextEnemy(currentRound, totalRound);
            }
            else
            {
                enemyGauge.Setup(totalRound + 1);
                SpawnNextEnemy(currentRound, 0);
            }
            player.GO();
        }
    }
    int ReverseTurn(int index) { return index == 0 ? 1 : 0; }
}
