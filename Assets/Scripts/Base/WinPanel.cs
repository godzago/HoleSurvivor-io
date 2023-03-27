using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPanel : MonoBehaviour
{
    [SerializeField] private Animator animator;
    void Start()
    {
        Invoke(nameof(ActiveAnimator), 0.5f);
    }

    void ActiveAnimator()
    {
        animator.enabled = true;
    }
}
