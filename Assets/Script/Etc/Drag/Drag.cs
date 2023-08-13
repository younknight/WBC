using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Slot slot;
    RectTransform rectTransform;
    Transform _startParent;
    CanvasGroup canvasGroup;
    Canvas canvas;
    private void Awake()
    {
        canvas = GameObject.FindWithTag("canvas").GetComponent<Canvas>();
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        slot = transform.parent.GetComponent<Slot>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        _startParent = transform.parent;
        transform.SetParent(GameObject.FindGameObjectWithTag("canvas").transform);
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // 이전 이동과 비교해서 얼마나 이동했는지를 보여줌
        // 캔버스의 스케일과 맞춰야 하기 때문에
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        transform.SetParent(_startParent);
        transform.SetAsFirstSibling();
        transform.localPosition = Vector3.zero;
    }
    public void SetUse()
    {
        _startParent.gameObject.SetActive(false);
    }
}