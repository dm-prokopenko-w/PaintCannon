using UnityEngine;
using Zenject;

namespace CannonSystem
{
    public class CannonAnimation : MonoBehaviour
    {
        [Inject] private CannonService _cannon;

        private bool _isActive;
        private float _endPoint = -1f;
        private float _startPoint;
        private float _speed = 0.05f;
        private int _coef;
        private bool _isCheckEndPoint;

        private void Start()
        {
            _startPoint = transform.localPosition.z;
            _cannon.OnClickActive += Active;
            Restart();
        }

        private void OnDestroy()
        {
            _cannon.OnClickActive -= Active;
        }

        private void Active()
        {
            _isActive = true;
        }

        private void Restart()
        {
            _isActive = false;
            _coef = -1;

            _isCheckEndPoint = false;
        }

        private void Update()
        {
            if (_isActive)
            {
                transform.localPosition = new Vector3(
                    transform.localPosition.x,
                    transform.localPosition.y,
                    transform.localPosition.z + _coef * _speed);

                if (!_isCheckEndPoint)
                {
                    if (transform.localPosition.z < _endPoint)
                    {
                        _isCheckEndPoint = true;
                        _coef = 1;
                    }
                }
                else
                {
                    if (transform.localPosition.z > _startPoint)
                    {
                        Restart();
                    }
                }
            }
        }
    }
}