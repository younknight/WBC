using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchDetecter : MonoBehaviour, IPointerDownHandler,IPointerMoveHandler,IPointerUpHandler
{
    float attackRadius = 1f;
    float damage = 0;
    float coolTime;
    CircleTimer timer;
    Collider2D[] colliders;
    Camera mainCamera;
    Vector3 attackPoint;
    [SerializeField] GameObject attackRangePrefab;
    // Start is called before the first frame update
    void Awake()
    {
        mainCamera = Camera.main;
        attackRangePrefab.SetActive(false);
        transform.parent.gameObject.SetActive(false);
    }
    public void Setup(NonTargetSkill skill, CircleTimer timer, float coolTime)
    {
        this.damage = skill.damage;
        this.timer = timer;
        this.coolTime = coolTime;
        attackRadius = skill.radius;
        attackRangePrefab.transform.localScale = new Vector3(attackRadius, attackRadius, 1);
    }
    void TryOverlap()
    {
        colliders = Physics2D.OverlapCircleAll(attackPoint, attackRadius / 2);
        Attack();
    }
    void Attack()
    {
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.tag == "Enemy")
            {
                collider.GetComponent<Unit>().Damaged(damage);
            }
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        attackRangePrefab.SetActive(true);
        Vector3 position = new Vector3(eventData.position.x, eventData.position.y);
        attackRangePrefab.transform.position = mainCamera.ScreenToWorldPoint(position);//
        attackRangePrefab.transform.position = new Vector3(attackRangePrefab.transform.position.x, attackRangePrefab.transform.position.y, 0);
    }
    public void OnPointerMove(PointerEventData eventData)
    {
        Vector3 position = new Vector3(eventData.position.x, eventData.position.y);
        attackRangePrefab.transform.position = mainCamera.ScreenToWorldPoint(position);//
        attackRangePrefab.transform.position = new Vector3(attackRangePrefab.transform.position.x, attackRangePrefab.transform.position.y, 0);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        attackRangePrefab.SetActive(false);
        transform.parent.gameObject.SetActive(false);
        timer.TimerStart(coolTime); 
        attackPoint = attackRangePrefab.transform.position;
        TryOverlap();
    }
}
