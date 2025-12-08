using UnityEngine;
using UnityEngine.UI;

namespace MyCat.Runtime.UI
{
    [RequireComponent(typeof(Toggle))]
    public class CustomToggle : MonoBehaviour
    {
        private Toggle _toggle;
        [SerializeField] private GameObject[] _onToggleOn = default;
        [SerializeField] private GameObject[] _onToggleOff = default;

        private void Awake()
        {
            _toggle = GetComponent<Toggle>();
            _toggle.onValueChanged.AddListener(OnToggleChanged);
        }

        private void OnToggleChanged( bool isOn )
        {
            foreach ( var go in _onToggleOn )
            {
                go.SetActive( isOn );
            }

            foreach ( var go in _onToggleOff )
            {
                go.SetActive( !isOn );
            }
        }
    }    
}

