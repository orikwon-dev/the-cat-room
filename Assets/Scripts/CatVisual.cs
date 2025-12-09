using System;
using UnityEngine;
using MyCat.Domain;

namespace MyCat.Runtime
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class CatVisual : MonoBehaviour
    {
        private readonly int AnimationParam_CatState = Animator.StringToHash("CatState");
        private readonly int AnimationParam_IsMoving = Animator.StringToHash("IsMoving");
        
        private SpriteRenderer _spriteRenderer;
        private Animator _animator;
        
        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();

            Debug.Assert(_spriteRenderer != null, "_spriteRenderer != null");
            Debug.Assert(_animator != null, "_animator != null");
        }

        public void SetCatState(StateType stateType)
        {
            if (_animator == null)
            {
                return;
            }
            
            _animator.SetInteger(AnimationParam_CatState,(int)stateType);
        }
    }
}

