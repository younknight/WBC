using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IDropItems { }
public class DropItemsManager : MonoBehaviour
{
    static public Type GetClassName(IDropItems character)
    {
        return character.GetType();
    }
    static public T GetCharacter<T>(IDropItems dropItems) where T : IDropItems
    {
        // 리스트에서 T 타입을 찾아 반환
            if (dropItems is T)
            {
                return (T)dropItems;
            }

        Debug.LogError("Character not found!");
        return default(T); // 또는 null을 반환할 수도 있습니다. (T가 클래스 타입일 때)
    }
}
