using System;
using UnityEngine;

namespace MyCat.Runtime
{
    [RequireComponent(typeof(AudioSource))]
    public class BGMPlayer : MonoBehaviour
    {
        private AudioSource _audio;

        private void OnEnable()
        {
            SoundManager.Instance.OnBGMSettingChanged += OnBGMSettingChanged;
        }

        private void OnDisable()
        {
            if ( SoundManager.Instance != null )
            {
                SoundManager.Instance.OnBGMSettingChanged -= OnBGMSettingChanged;
            }
        }

        private void Awake()
        {
            _audio = GetComponent<AudioSource>();
        }

        private void Start()
        {
            // BGM 은 반드시 루핑되어야 해서 코드를 통해 강제로 설정함
            _audio.loop = true;
            _audio.clip = SoundManager.Instance.GetCurrentBGM();
            _audio.Play();
        }

        private void OnBGMSettingChanged()
        {
            _audio.mute = !SoundManager.Instance.BGMEnabled;
        }
    }
}