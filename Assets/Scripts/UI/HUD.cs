using MyCat.Domain;
using UnityEngine;
using UnityEngine.UI;

namespace MyCat.Runtime.UI
{
    public class HUD : MonoBehaviour
    {
        [Header("Status")] [SerializeField] private Text _stateText;
        [SerializeField] private Text[] _statValueTexts;

        [Header("Balance")] [SerializeField] private Text _balanceText;
        [SerializeField] private Text _productionRateText;

        private void Start()
        {
            Status curStatus = GameManager.Instance.CurrentStatus;
            curStatus.OnChange += UpdateStatusTexts;
            UpdateStatusTexts();

            CurrencyManager.Instance.OnBalanceChanged += UpdateBalanceText;
            UpdateProductionRateText();
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
            _statValueTexts[(int)StatusType.Cleanliness].text =
                $"Cleanliness : {curStatus.GetStat(StatusType.Cleanliness)}";
            _statValueTexts[(int)StatusType.Happiness].text = $"Happiness : {curStatus.GetStat(StatusType.Happiness)}";
            _stateText.text = $"State : {curStatus.GetState().ToString()}";
        }

        private void UpdateBalanceText(BigNumber balance)
        {
            string formattedBalance = FormatNumbers.Format(balance);
            _balanceText.text = formattedBalance;
        }

        private void UpdateProductionRateText()
        {
            string formattedProductionRate = FormatNumbers.Format(CurrencyManager.Instance.ProductionRate);
            _productionRateText.text = string.Format("+{0}/s", formattedProductionRate);
        }
    }
}