using System;
using UnityEngine;

namespace MyCat.Runtime.Data
{
    [Serializable]
    public class GameConfigParameters
    {
        public float StatusDecayIntervalSeconds;
        public float HungerDecayAmount;
        public float FunDecayAmount;
        public float CleanlinessDecayAmount;
        public float HappinessDecayAmount;
        public float HappinessDecayStartThreshold;
        public float HappinessIncreaseStartThreshold;

        public float HungerCareIncreaseAmount;
        public float FunCareIncreaseAmount;
        public float CleanlinessCareIncreaseAmount;
        public long CareRewardAmount;
    }
    
    [CreateAssetMenu(fileName = "GameConfigData", menuName = "Scriptable Objects/GameConfigData")]
    public class GameConfigData : ScriptableObject
    {
        public GameConfigParameters GameConfigParameters;
    }

    public static class GameConfig
    {
        private const string CONFIG_FILENAME = "GameConfig";
        private static GameConfigData _configData = null;
        public static GameConfigParameters Parameters => _configData.GameConfigParameters;
        
        public static bool Load()
        {
            _configData = Resources.Load<GameConfigData>(CONFIG_FILENAME);
            if (_configData == null)
            {
                Debug.LogError($"Config file {CONFIG_FILENAME} couldn't be loaded.");
                return false;
            }
            
            return true;
        }
    }
}

