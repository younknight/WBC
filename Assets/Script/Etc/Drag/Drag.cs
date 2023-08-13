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
        // ���� �̵��� ���ؼ� �󸶳� �̵��ߴ����� ������
        // ĵ������ �����ϰ� ����� �ϱ� ������
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