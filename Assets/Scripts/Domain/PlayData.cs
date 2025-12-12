using System;
using Math = System.Math;

namespace MyCat.Domain
{
    // 전반적인 게임 진행도 등 플레이어에 종속된 정보를 저장함
    public class PlayData
    {
        public Action<long> OnCoinAmountChanged;
        public Action<long> OnCoinAdded;   // delta
        public Action<long> OnCoinRemoved; // delta
        
        // 현재 유저가 보유한 코인 수량. 클리커 게임이 아니므로 ulong 으로 커버하고 한참 남는다
        private long _coins;
        public long CurrentCoins => _coins;
        
        public bool HasSufficientCoins(long amount)
        {
            return _coins >= amount;
        }

        public void AddCoins(long amount)
        {
            _coins += amount;
            OnCoinAmountChanged?.Invoke(_coins);
            OnCoinAdded?.Invoke(amount);
        }

        public void RemoveCoins(long amount, bool removeAll = false)
        {
            if (HasSufficientCoins(amount) || removeAll)
            {
                long realRemoveAmount = Math.Min(amount, _coins);
                _coins -= realRemoveAmount;
                OnCoinAmountChanged?.Invoke(_coins);
                OnCoinRemoved?.Invoke(realRemoveAmount);
            }
        }
    }
}