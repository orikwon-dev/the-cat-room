using System;
using UnityEngine;

namespace MyCat
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class CatVisual : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        private Animator _animator;
        
        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();

            Debug.Assert(_spriteRenderer != null, "_spriteRenderer != null");
            Debug.Assert(_animator != null, "_animator != null");
        }
    }
}

