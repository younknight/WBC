using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationContol : MonoBehaviour
{
    [SerializeField] Animator animator;//
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();   
    }
    public void SetAnimation(string trigger, bool isOn)
    {
        animator.SetBool(trigger, isOn);
    }
}
