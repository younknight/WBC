using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public interface IInformation
{
    public int GetId();
    public string GetName();
    public string GetExplain();
    public string GetRanking();
    public Sprite GetSprite();

}
public class InfoManager : MonoBehaviour
{
    static public string GetClassName(IInformation character)
    {
        return character.GetType().Name;
    }
    static public T GetCharacter<T>(IInformation dropItems) where T : IInformation
    {
        // 리스트에서 T 타입을 찾아 반환
            if (dropItems is T)
            {
                return (T)dropItems;
            }

        Debug.LogError("Character not found!");
        return default(T); // 또는 null을 반환할 수도 있습니다. (T가 클래스 타입일 때)
    }
    static public string CheckInterfaceType(IInformation obj)
    {
        if (obj is Item) return "item";
        else if (obj is Weapon) return "weapon";
        else if (obj is Chest) return "chest";
        else return "Unknown";
    }
}
