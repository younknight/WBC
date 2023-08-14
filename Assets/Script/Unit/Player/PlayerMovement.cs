using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    int wayPointCount;
    //[SerializeField] Transform parent;
    [SerializeField] Transform[] wayPoints;
    int currentIndex = 0;
    Movement2D movement2D;
    [SerializeField] AnimationContol animationContoler;
    [SerializeField] Attacker attacker;
    [SerializeField] bool isInfinity;

    // Start is called before the first frame update
    //private void OnValidate()
    //{
    //    wayPoints = parent.GetComponentsInChildren<Transform>();
    //}
    void Start()
    {
        attacker = GetComponent<Attacker>();
        movement2D = GetComponent<Movement2D>();
        animationContoler = GetComponent<AnimationContol>();
        wayPointCount = wayPoints.Length;
        transform.position = wayPoints[currentIndex].position;
    }
    public void Stop()
    {
        animationContoler.SetAnimation("attack", false);
        animationContoler.SetAnimation("move", false);
    }
    public void GO()
    {
        animationContoler.SetAnimation("attack", false);
        animationContoler.SetAnimation("move", true);
        StartCoroutine("OnMove");
    }
    IEnumerator OnMove()
    {
        NextMoveTo();
        bool isArrive = false;
        while (!isArrive)
        {
            if(Vector3.Distance(transform.position, wayPoints[currentIndex].position) < 0.02f * movement2D.MoveSpeed)
            {
                isArrive = true;
                Arrive();
                animationContoler.SetAnimation("move", false);
                //도착 공격개시
            }
            yield return null;
        }
    }
    void Arrive()
    {
        Attacker.CanAttack = true;
        movement2D.MoveTo(Vector3.zero);
        attacker.ChangeState(WeaponState.SearchTarget);
        EnemySpawner.Instance.Search();
    }
    void NextMoveTo()
    {
        Attacker.CanAttack = false;
        if (currentIndex < wayPointCount - 1)
        {
            transform.position = wayPoints[currentIndex].position;
            currentIndex++;

            //Vector3 direction = (wayPoints[currentIndex].position - transform.position).normalized;
            movement2D.MoveTo(Vector3.up);
        }
        else
        {
            if (isInfinity)
            {
                currentIndex = 0;
                movement2D.MoveTo(Vector3.up);
            }
            else EndPopup.Instance.Setup(false, false, null);
        }
    }
}
