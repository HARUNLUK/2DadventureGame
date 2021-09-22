using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager
{
    Animator animator;
    bool running = false;
    bool dead = false;

    public AnimationManager(Animator animator)
    {
        this.animator = animator;
    }
    public void Idle()
    {
        notRun();
    }
    public void attack()
    {
        if (dead)
        {
            return;
        }
        animator.SetTrigger("attack");
        notRun();
    }
    public void Run()
    {
        if (dead)
        {
            return;
        }
        running = true;
        refresh();
    }
    public void notRun()
    {
        running = false;
        refresh();
    }
    public void Jump()
    {
        if (dead)
        {
            return;
        }
        animator.SetTrigger("jump");
        notRun();
    }
    public void Dead()
    {
        dead = true;
        animator.SetTrigger("dead");
        notRun();
    }
    public void Damage()
    {
        if (dead)
        {
            return;
        }
        animator.SetTrigger("damage");
        notRun();
    }
    public void refresh()
    {
        animator.SetBool("isRunning", running);
    }
}
