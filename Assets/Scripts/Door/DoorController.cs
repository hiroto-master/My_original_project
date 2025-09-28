using System;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private Animator animator;
    private bool isOpen = false;
    private bool isActive = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void AnimateDoor()
    {
        if (!isOpen)
        {
            animator.SetBool("isOpenDoor",true);
            isOpen = true;
        }
        else if(isOpen)
        {
            animator.SetBool("isOpenDoor",false);
            isOpen = false;
        }
    }
}
