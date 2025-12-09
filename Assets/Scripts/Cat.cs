using System;
using UnityEngine;

namespace MyCat.Runtime
{
    public class Cat : MonoBehaviour
    {
        private CatVisual _catVisual;
        
        private void Awake()
        {
            _catVisual = GetComponentInChildren<CatVisual>();
        }

        private void Update()
        {
            if (GameManager.Instance != null)
            {
                _catVisual.SetCatState(GameManager.Instance.CurrentStatus.GetState());
            }
        }
    }
}