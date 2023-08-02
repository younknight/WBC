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
        // ����Ʈ���� T Ÿ���� ã�� ��ȯ
            if (dropItems is T)
            {
                return (T)dropItems;
            }

        Debug.LogError("Character not found!");
        return default(T); // �Ǵ� null�� ��ȯ�� ���� �ֽ��ϴ�. (T�� Ŭ���� Ÿ���� ��)
    }
    static public string CheckInterfaceType(IInformation obj)
    {
        if (obj is Item) return "item";
        else if (obj is Weapon) return "weapon";
        else if (obj is Chest) return "chest";
        else return "Unknown";
    }
}
