using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    [SerializeField] private MovementController movementScript;
    private Animator animator;

    public event Action OnHit;

    private void Start()
    {
        animator = GetComponent<Animator>();
        movementScript.OnReachingToThePlayer += Attack;
    }

    private void Attack()
    {
        animator.SetBool("IsAttacking", true);
    }

    public void HitAnimationCompletion()
    {
        animator.SetBool("IsAttacking", false);
    }

    public void GolemHitFrame()
    {
        OnHit?.Invoke();
    }
}
