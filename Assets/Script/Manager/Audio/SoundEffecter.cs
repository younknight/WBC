using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum soundEffectType
{
    drop,
    chestOpen,
    getPositive,
    getNegative
}
public class SoundEffecter : MonoBehaviour
{
    static SoundEffecter instance;

    // Inspector 에표시할 배경음악 목록
    [SerializeField] AudioClip[] drop;
    [SerializeField] AudioClip[] chestOpen;
    [SerializeField] AudioClip[] getPositive;
    [SerializeField] AudioClip[] getNegative;
    Dictionary<soundEffectType, AudioClip[]> BGMList = new Dictionary<soundEffectType, AudioClip[]>(); 
    public AudioSource BGM;

    public static SoundEffecter Instance { get => instance; set => instance = value; }

    private void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else Destroy(this.gameObject);
    }
    void Start()
    {
        BGM = GetComponent<AudioSource>();
        SetDic();
    }
    void SetDic()
    {
        BGMList.Add(soundEffectType.drop, drop);
        BGMList.Add(soundEffectType.chestOpen, chestOpen);
        BGMList.Add(soundEffectType.getPositive, getPositive);
        BGMList.Add(soundEffectType.getNegative, getNegative);
    }
    public void PlayEffect(soundEffectType effectType)
    {
        BGM.clip = BGMList[effectType][Random.Range(0, BGMList[effectType].Length)];
        BGM.Play();
    }
}
