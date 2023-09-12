using CannonSystem;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Core
{
    public class CameraShake : MonoBehaviour
    {
        [Inject] private CannonService _cannon;
        
        private bool _isShake = false;
        private float _time = 0f;
        private float _duration = 0.2f;
        private float _magnitude = 0.1f;
        private Vector3 _original;

        private void Start()
        {
            _original = transform.localPosition;
            _cannon.OnBulletEffect += Active;
            Restart();
        }

        private void OnDestroy()
        {
            _cannon.OnBulletEffect -= Active;
        }

        private void Active()
        {
            _isShake = true;
        }
        
        private void Restart()
        {
            _time = 0;
            transform.localPosition = _original;
            _isShake = false;
        }

        private void Update()
        {
            if (_isShake)
            {
                float x = Random.Range(-1, 1f) * _magnitude;
                float y = Random.Range(-1, 1f) * _magnitude;

                transform.localPosition = new Vector3(x, y, _original.z);
                _time += Time.deltaTime;

                if (_time > _duration)
                {
                    Restart();
                }
            }
        }
    }
}
