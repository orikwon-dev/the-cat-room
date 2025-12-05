using UnityEngine;
using System;
using MyCat.Domain;

namespace MyCat
{
    public class GameManager : MonoSingletonBase<GameManager>
    {
        private Status _status;
        public Status CurrentStatus => _status;
        
        public void Initialize()
        {
            _status = new Status();
        }
    }
}