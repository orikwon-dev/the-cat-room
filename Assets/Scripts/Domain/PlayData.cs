using System;

namespace MyCat.Domain
{
    // 전반적인 게임 진행도 등 플레이어에 종속된 정보를 저장함
    public class PlayData
    {
        public Action<ulong> OnCoinAmountChanged;
        
        // 현재 유저가 보유한 코인 수량. 클리커 게임이 아니므로 ulong 으로 커버하고 한참 남는다
        private ulong _coins;
        public ulong CurrentCoins => _coins;
        
        public bool HasSufficientCoins(ulong amount)
        {
            return _coins >= amount;
        }

        public void AddCoins(ulong amount)
        {
            _coins += amount;
            OnCoinAmountChanged?.Invoke(_coins);
        }

        public void RemoveCoins(ulong amount)
        {
            if (HasSufficientCoins(amount))
            {
                _coins -= amount;
                OnCoinAmountChanged?.Invoke(_coins);
            }
        }
    }
}