using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CannonSystem
{
    public class PowerView : MonoBehaviour
    {
        [Inject] private CannonService _cannon;

        [SerializeField] private Scrollbar _scroll;
        [SerializeField] private TMP_Text _textPower;
        
        private void Start()
        {
            _scroll.onValueChanged.AddListener(ChangePower);
        }

        private void ChangePower(float value = 0)
        {
            var power = value + 0.2f;
            _cannon.OnChangePower?.Invoke(power);
            _textPower.text = ((int)(value * 100)).ToString();
        }
    }
}