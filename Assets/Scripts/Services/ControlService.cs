using System;
using Core;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ControlSystem
{
    public class ControlService : Service
    {
        [SerializeField] private TouchZone _touchZone;
        
        public event Action<PointerEventData> TouchStart;
        public event Action<PointerEventData> TouchEnd;
        public event Action<PointerEventData> TouchMoved;
        public event Action<PointerEventData> OnClick;

        public override void InstallBindings()
        {
            Container.Bind<ControlService>().FromInstance(this).AsSingle().NonLazy();
        }

        private void Awake()
        {
            _touchZone.TouchStart += OnPointerDown;
            _touchZone.TouchEnd += OnPointerUp;
            _touchZone.TouchMoved += OnDrag;
            _touchZone.OnClick += Click;
        }

        private void OnDestroy()
        {
            _touchZone.TouchStart -= OnPointerDown;
            _touchZone.TouchEnd -= OnPointerUp;
            _touchZone.TouchMoved -= OnDrag;
            _touchZone.OnClick -= Click;
        }

        private void OnPointerDown(PointerEventData eventData)
        {
            TouchStart?.Invoke(eventData);
        }

        private void OnPointerUp(PointerEventData eventData)
        {
            TouchEnd?.Invoke(eventData);
        }

        private void OnDrag(PointerEventData eventData)
        {
            TouchMoved?.Invoke(eventData);
        }
        
        private void Click(PointerEventData eventData)
        {
            OnClick?.Invoke(eventData);
        }
    }
}