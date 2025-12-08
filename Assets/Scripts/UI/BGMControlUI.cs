using UnityEngine;
using UnityEngine.UI;

namespace MyCat.Runtime.UI
{
    public class BGMControlUI : MonoBehaviour
    {
        [SerializeField] private Toggle _toggleBGM;

        private void Awake()
        {
            _toggleBGM.onValueChanged.AddListener(OnToggleBGM);
            _toggleBGM.isOn = SoundManager.Instance.BGMEnabled; // 매니저의 현재 상태와 동기화
        }

        private void OnToggleBGM( bool value )
        {
            SoundManager.Instance.EnableBGM(value);
        }
    }
}