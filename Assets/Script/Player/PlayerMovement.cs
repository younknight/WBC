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
    // Start is called before the first frame update
    //private void OnValidate()
    //{
    //    wayPoints = parent.GetComponentsInChildren<Transform>();
    //}
    void Start()
    {
        movement2D = GetComponent<Movement2D>();
        animationContoler = GetComponent<AnimationContol>();
        wayPointCount = wayPoints.Length;
        transform.position = wayPoints[currentIndex].position;
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
                //animationContoler.SetAnimation("Attack", true);
                //µµÂø °ø°Ý°³½Ã
            }
            yield return null;
        }
    }
    void Arrive()
    {
        movement2D.MoveTo(Vector3.zero);
    }
    void NextMoveTo()
    {
        if(currentIndex < wayPointCount - 1)
        {
            transform.position = wayPoints[currentIndex].position;
            currentIndex++;
            Vector3 direction = (wayPoints[currentIndex].position - transform.position).normalized;
            movement2D.MoveTo(direction);
        }
        else
        {
            //µµÂø
        }
    }
}
