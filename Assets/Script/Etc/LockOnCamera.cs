using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOnCamera : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Vector3 offset;
    [SerializeField] float followSpeed = 2f;
    private void FixedUpdate()
    {
        // transform.position = Vector3.Lerp(transform.position, player.position, followSpeed * Time.deltaTime);
        Vector3 camera_pos = player.position + offset;
        Vector3 lerp_pos = Vector3.Lerp(transform.position, camera_pos, followSpeed);
        transform.position = lerp_pos;
       // transform.LookAt(player);
        transform.Translate(0, 0, -10);
    }
}
