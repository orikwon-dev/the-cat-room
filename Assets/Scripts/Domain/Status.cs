using UnityEngine;

namespace MyCat.Domain
{
    public enum StatusType
    {
        Hunger = 0,
        Energy,
        Cleanliness,
        Affection,
        Count
    }

    public class Status
    {
        private float[] StatValues;

        public Status()
        {
            StatValues = new float[(int)StatusType.Count];
            Init();
        }

        public void Init()
        {
            SetStat(StatusType.Hunger, 100);
            SetStat(StatusType.Energy, 100);
            SetStat(StatusType.Cleanliness, 100);
            SetStat(StatusType.Affection, 50);
        }

        private void SetStat(StatusType statusType, float newValue)
        {
            if (HasValue(statusType) == false)
            {
                return;
            }
            
            StatValues[(int)statusType] = newValue;
        }

        private bool HasValue(StatusType statusType)
        {
            if (StatValues == null || StatValues.Length <= (int)statusType)
            {
                return false;
            }

            return true;
        }

        public float GetStat(StatusType statusType)
        {
            if (HasValue(statusType) == false)
            {
                return -1f;
            }
            
            return StatValues[(int)statusType];
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

            StatValues[(int)statusType] += deltaValue;
            StatValues[(int)statusType] = Mathf.Clamp(StatValues[(int)StatusType.Energy], 0, 100);
        }

        public void ReduceStat(StatusType statusType, float deltaValue)
        {
            if (deltaValue >= 0)
            {
                return;
            }

            if (HasValue(statusType) == false)
            {
                return;
            }

            StatValues[(int)statusType] -= deltaValue;
            StatValues[(int)statusType] = Mathf.Clamp(StatValues[(int)StatusType.Energy], 0, 100);
        }
    }
}