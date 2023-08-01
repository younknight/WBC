using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSlot : MonoBehaviour
{
    [SerializeField] MapWorld world;//
    [SerializeField] int id;//
    public MapWorld World { get => world; set => world = value; }
    public int Id { get => id; set => id = value; }
    public void SetMap()
    {
        MapManager.Instance.SelectMap(world,id);
        StagePopup.Instance.Setup(MapManager.Instance.GetStageName(world,id));
    }
}
