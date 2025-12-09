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

        public void SetLookDirection(bool isLookRight, bool isMovingSpriteShowing = false)
        {
            // 이동 모션은 스프라이트가 반대 방향이라서 플래그를 뒤집어서 사용한다.
            // isLookRight 만 true 이거나, isMovingSpriteShowing 만 true 이거나 (XOR)
            _spriteRenderer.flipX = isLookRight ^ isMovingSpriteShowing;;
        }

        public void SetCatState(StateType stateType)
        {
            if (_animator == null)
            {
                return;
            }
            
            _animator.SetInteger(AnimationParam_CatState,(int)stateType);
        }

        public void SetIsMoving(bool isMoving)
        {
            if (_animator == null)
            {
                return;
            }
            
            _animator.SetBool(AnimationParam_IsMoving,isMoving);
        }
    }
}

