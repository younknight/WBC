using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpPositionSetter : MonoBehaviour
{
    [SerializeField] Vector3 distance = Vector3.down * 20.0f;
    [SerializeField] Vector2 size = new Vector2(20,10);
    [SerializeField]Transform targetTransform;
    [SerializeField] RectTransform rectTransform;
    
    public void Setup(Transform target)
    {
        targetTransform = target;
        rectTransform = GetComponent<RectTransform>();
        rectTransform.sizeDelta = size;
    }
    private void LateUpdate()
    {
        if(targetTransform == null)
        {
            Destroy(gameObject);
            return;
        }
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(targetTransform.position);
        if(screenPosition != null) rectTransform.position = screenPosition + distance;
    }
}
