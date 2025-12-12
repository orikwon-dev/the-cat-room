using UnityEngine;
using System;
using System.Collections;
using MyCat.Domain;

namespace MyCat.Runtime
{
    // (기술부채) 정보를 제어하는 기능들을 게임 매니저에 때려넣었는데, 이후 차근차근 분리해나가도록 한다.
    public class GameManager : MonoSingletonBase<GameManager>
    {
        private Status _status = new Status();

        private float _lastPauseTime;
        private bool _paused = false; // 게임 시작 시점에 pause 가 풀렸다고 인지되는 것을 방지하기 위함
        private bool _isInitialized = false;

        public Status CurrentStatus => _status;

        public void Initialize()
        {
            if (_isInitialized == false)
            {
                // (기술부채) 여기서 등록하지 않게 방법 찾기
                _status.OnStatusIncresed += OnStatusIncresed;

                StartCoroutine(RealTimeUpdate());
                _isInitialized = true;
            }
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
            {
                Debug.Log("Paused");
                _paused = true;
                _lastPauseTime = Time.unscaledTime;
            }
            else if (_paused)
            {
                Debug.Log("Resumed");
                float pausedTime = Time.unscaledTime - _lastPauseTime;
                DecayStats(pausedTime);
                _paused = false;
            }
        }

        private IEnumerator RealTimeUpdate()
        {
            var waitForSecondsRealtime = new WaitForSecondsRealtime(1.0f);
            while (true)
            {
                yield return waitForSecondsRealtime;
                DecayStats(1.0f);
            }
        }

        private void DecayStats(float elapsedSeconds)
        {
            float testReduceAmount = 10.0f;

            _status?.ReduceStat(StatusType.Hunger, testReduceAmount * elapsedSeconds);
            _status?.ReduceStat(StatusType.Fun, testReduceAmount * elapsedSeconds);
            _status?.ReduceStat(StatusType.Cleanliness, testReduceAmount * elapsedSeconds);

            if (_status.GetStat(StatusType.Hunger) <= 50.0f)
            {
                _status?.ReduceStat(StatusType.Happiness, testReduceAmount * elapsedSeconds);
            }
        }

        private void OnStatusIncresed(StatusChangeParam param)
        { 
            // (기술부채) 다른 매니저가 여기서 실핸된다는게 문제로 보임
            PlayDataManager.Instance.CurrentPlayData.AddCoins(50);
        }
    }
}