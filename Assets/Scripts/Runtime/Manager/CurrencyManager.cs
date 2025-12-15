using System;
using MyCat.Domain;
using UnityEngine;
using System.Collections;

namespace MyCat.Runtime
{
    public class CurrencyManager: MonoSingletonBase<CurrencyManager>
    {
        public Action<BigNumber> OnBalanceChanged;
        private BigNumber _balance = new BigNumber(0, 0);
        private BigNumber _productionRate = new BigNumber(1,0);

        public BigNumber ProductionRate
        {
            get
            {
                return _productionRate;
            }
        }

        public void Initialize()
        {
            StartCoroutine(CO_Production());
        }
        
        IEnumerator CO_Production()
        {
            var interval = new WaitForSecondsRealtime(1.0f);
            while (true)
            {
                _balance = _balance.Add(_productionRate);
                OnBalanceChanged?.Invoke(_balance);
                yield return interval;
            }
        }
    }    
}

