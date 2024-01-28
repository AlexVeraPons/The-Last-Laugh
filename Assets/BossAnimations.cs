using System;
using UnityEditor.Animations;
using UnityEngine;

public class BossAnimations : MonoBehaviour
{
    [SerializeField] private Animator _animationController;

    private void OnEnable() {
        ReactionBoss.OnBossInteraction += OnBossInteraction;
    }

    private void OnBossInteraction(float obj)
    {
        if (obj == 0)
        {
            _animationController.SetTrigger("angry");
        }
        else if (obj >= 0.75)
        {
            _animationController.SetTrigger("laugh");
        }
        else if (obj <= 0.5f)
        {
            _animationController.SetTrigger("chuckle");
        }
    }

    private void Start() {
        _animationController = GetComponent<Animator>();
    }


}
