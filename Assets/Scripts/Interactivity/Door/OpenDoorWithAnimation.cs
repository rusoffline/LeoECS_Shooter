using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class OpenDoorWithAnimation : BaseDoorAction
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public override void OpenForward()
    {
        animator.SetTrigger("OpenForward");
    }

    public override void OpenBackward()
    {
        animator.SetTrigger("OpenBackward");
    }

    public override void Close()
    {
        animator.SetTrigger("Close");
    }
}
