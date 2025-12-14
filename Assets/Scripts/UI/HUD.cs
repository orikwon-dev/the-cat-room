using MyCat.Domain;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace MyCat.Runtime.UI
{
    public class HUD : MonoBehaviour
    {
        [Header("Status")]
        [SerializeField] private Text _stateText;
        [SerializeField] private Text[] _statValueTexts;
        
        [Header("Coin")]
        [SerializeField] private Text _coinText;
        [SerializeField] private Text _coinDeltaText;
        [SerializeField] private float _coinDeltaTextLifeTime = 1.0f;

        private bool _isCoinDeltaTextShowing = false;
        private long _accumulatedDelta = 0;
        private float _accumulatedTime = 0;
        
        private void Start()
        {
            // 여기서 호출하는 것은 테스트를 위해서임
            GameManager.Instance.Initialize();
            Status curStatus = GameManager.Instance.CurrentStatus;
            curStatus.OnChange += UpdateStatusTexts;

            PlayData curPlayData = PlayDataManager.Instance.CurrentPlayData;
            curPlayData.OnCoinAmountChanged += UpdateCoinText;
            curPlayData.OnCoinAdded += OnCoinAdded;
            curPlayData.OnCoinRemoved += OnCoinRemoved;
            
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

        private void UpdateCoinText(long coin)
        {
            _coinText.text = string.Format("{0:N0}", coin);
        }

        private void OnCoinAdded(long delta)
        {
            if (delta == 0)
            {
                return;
            }

            _accumulatedDelta += delta;
            _accumulatedTime += _coinDeltaTextLifeTime;
            if (_isCoinDeltaTextShowing)
            {
                UpdateDeltaTextFromAccumulatedDelta();
            }
            else
            {
                StartCoroutine(ShowDeltaCoinString());
            }
        }

        private void OnCoinRemoved(long delta)
        {
            if (delta == 0)
            {
                return;
            }
            
            _accumulatedDelta -= delta;
            _accumulatedTime += _coinDeltaTextLifeTime;
            if (_isCoinDeltaTextShowing)
            {
                UpdateDeltaTextFromAccumulatedDelta();
            }
            else
            {
                StartCoroutine(ShowDeltaCoinString());
            }
        }

        private IEnumerator ShowDeltaCoinString()
        {
            _isCoinDeltaTextShowing = true;
            _coinDeltaText.gameObject.SetActive(true);
            UpdateDeltaTextFromAccumulatedDelta();
            
            float elapsedTime = 0;
            while (elapsedTime < _accumulatedTime)
            {
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            
            _isCoinDeltaTextShowing = false;
            _coinDeltaText.gameObject.SetActive(false);
            _accumulatedDelta = 0;
        }

        private void UpdateDeltaTextFromAccumulatedDelta()
        {
            if (_coinDeltaText == null)
            {
                return;
            }

            string formatString = (_accumulatedDelta >= 0) ? "+{0:N0}" : "-{0:N0}";
            _coinDeltaText.text = string.Format(formatString, _accumulatedDelta);
        }
    }
}