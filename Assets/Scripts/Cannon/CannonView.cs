using ControlSystem;
using Core;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace CannonSystem
{
    public class CannonView : MonoBehaviour
    {
        [Inject] private ControlService _control;
        [Inject] private CannonService _cannon;

        [SerializeField] private Transform _startPoint;
        [SerializeField] private Transform _endPoint;

        private float _powerCoef = 7f;
        private float _currentPower;

        private void Start()
        {
            _control.TouchMoved += OnDrag;
            _cannon.OnChangePower += ChangePower;
        }

        private void OnDestroy()
        {
            _control.TouchMoved -= OnDrag;
            _cannon.OnChangePower -= ChangePower;
        }

        private void ChangePower(float value)
        {
            _currentPower = value;
            OnDrag(new PointerEventData(EventSystem.current));
        }

        private void OnDrag(PointerEventData eventData)
        {
            Vector3 speed = (_startPoint.position - _endPoint.position) * _currentPower * _powerCoef;
            _cannon?.OnMovePlayer(_startPoint.position, speed);
        }
    }
}