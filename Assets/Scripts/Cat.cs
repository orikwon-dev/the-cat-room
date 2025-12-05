using System;
using UnityEngine;

namespace MyCat
{
    public class Cat : MonoBehaviour
    {
        private CatVisual _catVisual;
        
        private void Awake()
        {
            _catVisual = GetComponentInChildren<CatVisual>();
        }
    }
}