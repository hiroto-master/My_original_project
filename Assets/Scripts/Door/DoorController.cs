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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isActive)
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("eye"))
        {
            isActive = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("eye"))
        {
            isActive = false;
        }
    }
    
}
