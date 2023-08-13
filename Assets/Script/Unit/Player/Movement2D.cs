using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2D : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2.0f;
    [SerializeField] Vector3 moveDirection = Vector3.zero;

    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    private void Update()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
    public void MoveTo(Vector3 direction)
    {
        moveDirection = direction;
    }
}
