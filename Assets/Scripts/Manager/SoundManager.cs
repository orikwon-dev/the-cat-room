using System;
using UnityEngine;

namespace MyCat.Runtime
{
    // TODO : 매니저에서는 사운드 설정값, 재생할 음악 리스트 정보 같은 것을 관리한다.
    // TODO : 이후에 리소스 관리 시스템 같은걸로 불러오는걸 고려하면 좋겠지만, 지금은 그 단계는 아님
    public class SoundManager : MonoSingletonBase<SoundManager>
    {
        public event Action OnBGMSettingChanged;

        // BGM 제어 초기 구현
        [SerializeField] private AudioClip[] _bgmClips;
        private int _bgmClipIndex = 0;
        public bool BGMEnabled { get; private set; } = true;

        public void EnableBGM( bool bgmEnabled )
        {
            BGMEnabled = bgmEnabled;
            OnBGMSettingChanged?.Invoke();
        }

        public AudioClip GetCurrentBGM()
        {
            if ( _bgmClipIndex >= _bgmClips.Length )
            {
                return null;
            }

            return _bgmClips[_bgmClipIndex];
        }
    }
}