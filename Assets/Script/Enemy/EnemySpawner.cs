using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    static EnemySpawner instance;
    [SerializeField] MapInfo mapInfo;
    [SerializeField] Transform[] wayPoints;
    [SerializeField] PlayerMovement player;
    List<List<Unit>> enemies = new List<List<Unit>>();
    int maxRound;

    public MapInfo MapInfo { get => mapInfo; set => mapInfo = value; }
    public static EnemySpawner Instance { get => instance; }
    public List<List<Unit>> Enemies { get => enemies; set => enemies = value; }
    private void OnDestroy()
    {
        instance = null;
    }
    private void Awake()
    {
        if (instance == null) instance = this;
    }
    private void Start()
    {
        if(MapManager.selectedMap) SetUp(MapManager.selectedMap);
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
            }
            enemies.Add(enemieyList);
        }
    }
    public void DestroyEnemy(Unit enemy, int round)
    {
        enemies[round].Remove(enemy);
        if(enemies.Count == 0)
        {
            player.GO();
            //전부 처리완료
        }
    }
}
