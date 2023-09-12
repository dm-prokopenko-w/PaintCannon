using System;
using System.Collections.Generic;
using UnityEngine;

namespace CannonSystem
{
    public class Bullet : MonoBehaviour
    {
        private List<Vector3> _path = new List<Vector3>();
        private float _secBeforePoints = 0.01f;
        private float _currentSec;
        private int _currentNum = 0;
        private GameObject _effect;
        private Action _onArrivalBullet;
        
        public void Init(List<Vector3> path, GameObject effect, Action onArrivalBullet)
        {
            _path = path;
            _effect = effect;
            _onArrivalBullet = onArrivalBullet;
        }

        private void Update()
        {
            if (_path == null || _path.Count == 0) return;

            _currentSec += Time.deltaTime;

            if (_currentSec > _secBeforePoints)
            {
                _currentSec = 0;
                transform.position = _path[_currentNum];
                _currentNum++;

                if (_path.Count == _currentNum)
                {
                    _onArrivalBullet?.Invoke();
                    Instantiate(_effect, transform.position, Quaternion.identity);
                    Destroy(gameObject);
                }
            }
        }
    }
}