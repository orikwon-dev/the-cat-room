using System;
using Math = System.Math;

namespace MyCat.Domain
{
    public enum StateType
    {
        NotDetermined = -1,
        Normal, // 일반적인 상태
        Happy, // 행복한 상태
        Hungry, // 배고픔
        Bored, // 심심함
        Illness, // 아픔
    }

    public enum StatusType
    {
        Hunger = 0, // 포만감 수치. 밥을 주면 상승, 서서히 감소
        Fun, // 즐거움 수치. 놀아주면 상승, 서서히 감소
        Cleanliness, // 청결 수치. 목욕을 시켜주면 완전 회복, 평시엔 서서히 감소/놀아주거나 밥을 먹이면 일정 수치 감소
        Happiness, // 애정도 수치. 모든 스탯 중 하나라도 50% 미만이면 서서히 감소. 아프거나 전반적으로 수치가 낮으면 빠르게 감소
        Count
    }

    public struct StatusChangeParam
    {
        public StatusType StatusType;
        public float Delta;

        public StatusChangeParam(StatusType statusType, float delta)
        {
            StatusType = statusType;
            Delta = delta;
        }
    }

    public class Status
    {
        public event Action OnChange;
        public event Action<StatusChangeParam> OnStatusIncresed; // float: delta
        public event Action<StatusChangeParam> OnStatusDecresed; // float: delta
        private float[] _statValues;

        // 매번 enum 을 호출하기 싫어서 편의성을 위해 추가함
        public float Hunger => GetStat(StatusType.Hunger);
        public float Fun => GetStat(StatusType.Fun);
        public float Cleanliness => GetStat(StatusType.Cleanliness);
        public float Happiness => GetStat(StatusType.Happiness);

        public Status()
        {
            _statValues = new float[(int)StatusType.Count];
            Init();
        }

        public void Init()
        {
            SetStat(StatusType.Hunger, 50);
            SetStat(StatusType.Fun, 50);
            SetStat(StatusType.Cleanliness, 50);
            SetStat(StatusType.Happiness, 50);
        }


        public float GetStat(StatusType statusType)
        {
            if (HasValue(statusType) == false)
            {
                return -1f;
            }

            return _statValues[(int)statusType];
        }

        public void AddStat(StatusType statusType, float deltaValue)
        {
            if (deltaValue <= 0)
            {
                return;
            }

            if (HasValue(statusType) == false)
            {
                return;
            }

            if (_statValues[(int)statusType] >= 100.0f)
            {
                return;
            }

            float remainToMax = 100.0f - _statValues[(int)statusType];
            float realIncreasedValue = Math.Min(remainToMax, deltaValue);
            _statValues[(int)statusType] += realIncreasedValue;

            OnChange?.Invoke();

            if (realIncreasedValue > 0.0f)
            {
                OnStatusIncresed?.Invoke(new StatusChangeParam(statusType, realIncreasedValue));
            }
        }

        public void ReduceStat(StatusType statusType, float deltaValue)
        {
            if (deltaValue <= 0)
            {
                return;
            }

            if (HasValue(statusType) == false)
            {
                return;
            }

            if (_statValues[(int)statusType] <= 0.0f)
            {
                return;
            }

            // 음수로 내려가지 않게 하는 감소치
            float realDecreasedValue = Math.Min(_statValues[(int)statusType], deltaValue);
            _statValues[(int)statusType] -= realDecreasedValue;
            OnChange?.Invoke();

            if (realDecreasedValue > 0.0f)
            {
                OnStatusDecresed?.Invoke(new StatusChangeParam(statusType, realDecreasedValue));
            }
        }

        public StateType GetState()
        {
            // 현재 스탯에 따른 고양이의 상태(또는 무드)를 반환한다.
            // 상태 조건의 범위가 겹치는 경우 우선순위가 있음
            // 이 조건에 대한 처리를 이후에 데이터 기반으로 변경할 필요가 있지만, 기획 검증 단계이므로 일단 하드코딩으로 해결한다.
            // 조건 : 위생과 허기 수치가 낮으면 아픈 상태가 된다
            if (Cleanliness <= 40.0f && Hunger <= 40.0f)
            {
                return StateType.Illness;
            }

            // 조건 : 포만감이 절반 이하로 내려가면 배고픔
            if (Hunger <= 50.0f)
            {
                return StateType.Hungry;
            }

            // 조건 : 즐거움이 낮으면 지루함
            if (Fun <= 50.0f)
            {
                return StateType.Bored;
            }

            // 세 가지 욕구 수치가 높으면 해피
            if (Happiness >= 80.0f)
            {
                return StateType.Happy;
            }

            return StateType.Normal;
        }

        private void SetStat(StatusType statusType, float newValue)
        {
            if (HasValue(statusType) == false)
            {
                return;
            }

            _statValues[(int)statusType] = newValue;
            OnChange?.Invoke();
        }

        private bool HasValue(StatusType statusType)
        {
            if (_statValues == null || _statValues.Length <= (int)statusType)
            {
                return false;
            }

            return true;
        }
    }
}