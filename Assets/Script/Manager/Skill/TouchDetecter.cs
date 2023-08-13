using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchDetecter : MonoBehaviour, IPointerDownHandler,IPointerMoveHandler,IPointerUpHandler
{
    [SerializeField] NonTargetSkill skill;//
    [SerializeField] float coolTime;//
    [SerializeField] CircleTimer timer;//
    Camera mainCamera;
    Vector3 attackPoint;
    [SerializeField] GameObject attackCircleRangePrefab;
    [SerializeField] GameObject attackSquareRangePrefab;
    // Start is called before the first frame update
    void Awake()
    {
        mainCamera = Camera.main;
        attackCircleRangePrefab = GameObject.Find("skillCircleRange");
        attackSquareRangePrefab = GameObject.Find("skillSquareRange");
        Clear();
    }
    void Clear()
    {
        attackCircleRangePrefab.SetActive(false);
        attackSquareRangePrefab.SetActive(false);
        transform.parent.gameObject.SetActive(false);
    }
    public void Setup(NonTargetSkill skill, CircleTimer timer, float coolTime)
    {
        this.skill = skill;
        this.timer = timer;
        this.coolTime = coolTime;
        attackCircleRangePrefab.transform.localScale = new Vector3(skill.radiusX, skill.radiusY, 1);
        attackSquareRangePrefab.transform.localScale = new Vector3(skill.radiusX, skill.radiusY, 1);
    }
    void SetPosition(Transform target, Vector3 position)
    {
        target.position = mainCamera.ScreenToWorldPoint(position);//
        target.position = new Vector3(target.position.x, target.position.y, 0);
    }
    public void OnPointerDown(PointerEventData eventData)//터치 시작
    {
        if (skill.areaType == AreaType.circle) attackCircleRangePrefab.SetActive(true);
        if (skill.areaType == AreaType.square) attackSquareRangePrefab.SetActive(true);
        MoveArea(eventData);
    }
    public void OnPointerMove(PointerEventData eventData)//터치 중
    {
        MoveArea(eventData);
    }
    void MoveArea(PointerEventData eventData)
    {
        Vector3 position = new Vector3(eventData.position.x, eventData.position.y);
        if (skill.areaType == AreaType.circle) SetPosition(attackCircleRangePrefab.transform, position);
        if (skill.areaType == AreaType.square) SetPosition(attackSquareRangePrefab.transform, position);
    }
    public void OnPointerUp(PointerEventData eventData)//터치 끝 == 스킬 발동
    {
        Clear();
        timer.TimerStart(coolTime);

        if (skill.areaType == AreaType.circle) attackPoint = attackCircleRangePrefab.transform.position;
        if (skill.areaType == AreaType.square) attackPoint = attackSquareRangePrefab.transform.position;

        Vector3 position = new Vector3(eventData.position.x, eventData.position.y);
        var skillEffect = Instantiate(skill.skillEffect);
        SetPosition(skillEffect.transform, position);
        skillEffect.GetComponent<SkillEffecter>().Setup(skill, attackPoint);
    }
}
