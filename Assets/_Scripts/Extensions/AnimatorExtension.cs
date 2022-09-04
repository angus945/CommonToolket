using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AnimatorExtension
{
    public static void ManualUpdate(this Animator animator, float deltaTime)
    {
        if (animator == null) return;

        animator.speed = 1;
        animator.Update(deltaTime);
        animator.speed = 0;
    }

}
