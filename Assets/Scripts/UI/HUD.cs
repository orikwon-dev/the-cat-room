using MyCat.Domain;
using UnityEngine;
using UnityEngine.UI;

namespace MyCat.Runtime.UI
{
    public class HUD : MonoBehaviour
    {
        [Header("Status")]
        [SerializeField] private Text _stateText;
        [SerializeField] private Text[] _statValueTexts;
        
        [Header("Coin")]
        [SerializeField] private Text _coinText;
        
        private void Start()
        {
            // 여기서 호출하는 것은 테스트를 위해서임
            GameManager.Instance.Initialize();
            Status curStatus = GameManager.Instance.CurrentStatus;
            curStatus.OnChange += UpdateStatusTexts;

            PlayData curPlayData = PlayDataManager.Instance.CurrentPlayData;
            curPlayData.OnCoinAmountChanged += UpdateCoinText;
            
            UpdateStatusTexts();
            UpdateCoinText(curPlayData.CurrentCoins);
        }
        
        private void UpdateStatusTexts()
        {
            Status curStatus = GameManager.Instance.CurrentStatus;
            if (curStatus == null)
            {
                Debug.LogError($"{nameof(HUD)} couldn't get current status");
                return;
            }
            
            _statValueTexts[(int)StatusType.Hunger].text = $"Hunger : {curStatus.GetStat(StatusType.Hunger)}";
            _statValueTexts[(int)StatusType.Fun].text = $"Fun : {curStatus.GetStat(StatusType.Fun)}";
            _statValueTexts[(int)StatusType.Cleanliness].text = $"Cleanliness : {curStatus.GetStat(StatusType.Cleanliness)}";
            _statValueTexts[(int)StatusType.Happiness].text = $"Happiness : {curStatus.GetStat(StatusType.Happiness)}";
            _stateText.text = $"State : {curStatus.GetState().ToString()}";
        }

        private void UpdateCoinText(ulong coin)
        {
            _coinText.text = string.Format("{0:N0}", coin);
        }
    }
}