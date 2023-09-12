using UnityEngine;

namespace Core
{
    public class DestroyByTime : MonoBehaviour
    {
        [SerializeField] private float _lifetime = 1f;

        private float _timer = 0f;

        private void Start()
        {
            _timer = _lifetime;
        }

        private void Update()
        {
            if (_timer > 0.0F)
            {
                _timer -= Time.deltaTime;
                if (_timer <= 0.0f)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}