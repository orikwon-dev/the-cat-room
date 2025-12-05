using System;
using MyCat.Domain;
using UnityEngine;
using UnityEngine.UI;

namespace MyCat.UI
{
    public class HUD : MonoBehaviour
    {
        [SerializeField] private Text[] _statValueTexts;

        private void Start()
        {
            // 여기서 호출하는 것은 테스트를 위해서임
            GameManager.Instance.Initialize();
            
            Status curStatus = GameManager.Instance.CurrentStatus;
            _statValueTexts[(int)StatusType.Hunger].text = $"Hunger : {curStatus.GetStat(StatusType.Hunger)}";
            _statValueTexts[(int)StatusType.Energy].text = $"Energy : {curStatus.GetStat(StatusType.Energy)}";
            _statValueTexts[(int)StatusType.Cleanliness].text = $"Cleanliness : {curStatus.GetStat(StatusType.Cleanliness)}";
            _statValueTexts[(int)StatusType.Affection].text = $"Affection : {curStatus.GetStat(StatusType.Affection)}";
        }
    }
}