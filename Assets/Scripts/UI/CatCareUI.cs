using System;
using MyCat.Domain;
using UnityEngine;
using UnityEngine.UI;

namespace MyCat.Runtime.UI
{
    public class CatCareUI : MonoBehaviour
    {
        [SerializeField] private Button _feedButton;
        [SerializeField] private Button _playWithCatButton;
        [SerializeField] private Button _bathButton;

        private void Awake()
        {
            // TODO : (기술부채) 고양이를 돌보면서 얻는 포인트에 대한 처리는 여기서 하지 않는게 원칙이다. 나중에 위치를 옮기자.
            _feedButton.onClick.AddListener(() =>
            {
                GameManager.Instance.CurrentStatus.AddStat(StatusType.Hunger, 50);
            });
            _playWithCatButton.onClick.AddListener(() =>
            {
                GameManager.Instance.CurrentStatus.AddStat(StatusType.Fun, 30);
            });
            _bathButton.onClick.AddListener(() =>
            {
                GameManager.Instance.CurrentStatus.AddStat(StatusType.Cleanliness, 100);
            });
        }
    }
}

