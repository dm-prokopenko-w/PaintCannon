using ControlSystem;
using Core;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Gameplay
{
    public class Player : MonoBehaviour
    {
        [Inject] private ControlService _control;

        [SerializeField] private Transform _rotHor;
        [SerializeField] private Transform _rotVer;

        private float _sensitivity = 2f;

        private void Start()
        {
            _control.TouchMoved += OnDrag;
        }

        private void OnDestroy()
        {
            _control.TouchMoved -= OnDrag;
        }

        private void OnDrag(PointerEventData eventData)
        {
            if (eventData.delta.y >= 0)
            {
                if (_rotVer.transform.rotation.x > -0.3f)
                    _rotVer.Rotate(new Vector3(-eventData.delta.y * Time.deltaTime * _sensitivity, 0, 0));
            }
            else if (eventData.delta.y < 0)
            {
                if (_rotVer.transform.rotation.x < 0f)
                    _rotVer.Rotate(new Vector3(-eventData.delta.y * Time.deltaTime * _sensitivity, 0, 0));
            }

            if (eventData.delta.x >= 0)
            {
                if (_rotHor.transform.rotation.y < 0.2f)
                    _rotHor.Rotate(new Vector3(0, eventData.delta.x * Time.deltaTime * _sensitivity, 0));
            }
            else if (eventData.delta.x < 0)
            {
                if (_rotHor.transform.rotation.y > -0.2f)
                    _rotHor.Rotate(new Vector3(0, eventData.delta.x * Time.deltaTime * _sensitivity, 0));
            }
        }
    }
}
