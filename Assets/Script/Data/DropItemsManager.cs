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
        // ����Ʈ���� T Ÿ���� ã�� ��ȯ
            if (dropItems is T)
            {
                return (T)dropItems;
            }

        Debug.LogError("Character not found!");
        return default(T); // �Ǵ� null�� ��ȯ�� ���� �ֽ��ϴ�. (T�� Ŭ���� Ÿ���� ��)
    }
}
