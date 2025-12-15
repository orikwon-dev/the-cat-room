using UnityEngine;
using MyCat.Runtime.Data;

namespace MyCat.Runtime
{
    // 게임의 진입로가 되는 클래스로 가장 먼저 실행되도록 설정되어 있다.
    // 여기서 각종 매니저나 시스템을 초기화하면 안정적으로 동작함
    public class GameEntry : MonoBehaviour
    {
        private void Awake()
        {
            GameConfig.Load();
            GameManager.Instance.Initialize();
            CurrencyManager.Instance.Initialize();
        }
    }
}
