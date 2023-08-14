using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenteredGridObjectSorter : MonoBehaviour
{
    public Transform[] objectsToSort;//
    private void OnValidate()
    {
        objectsToSort = new Transform[transform.childCount];
        for(int i= 0;i < transform.childCount; i++)
        {
            objectsToSort[i] = transform.GetChild(i).transform;
        }
        ArrangeObjectsInGrid();

    }
    public int rows = 5; // 행 수
    public int cols = 5; // 열 수
    public float spacing = 2.0f; // 오브젝트 사이의 간격

    void Start()
    {

    }

    void ArrangeObjectsInGrid()
    {
        int index = 0;
        ResetTransform();
        for (int y = -(rows - 1) / 2; y <= (rows - 1) / 2; y++)
        {
            for (int x = -(cols - 1) / 2; x <= (cols - 1) / 2; x++)
            {
                if (index < objectsToSort.Length)
                {
                    Vector3 offset = new Vector3(x * spacing, y * spacing, 0);
                    objectsToSort[index].transform.position += offset;
                    index++;
                }
            }
        }
    }
    void ResetTransform()
    {
        foreach(Transform transform in objectsToSort)
        {
            transform.position = this.transform.position + Vector3.zero;
        }

    }
}
