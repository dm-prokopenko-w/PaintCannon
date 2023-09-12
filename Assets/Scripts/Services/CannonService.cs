using System;
using System.Collections.Generic;
using ControlSystem;
using Core;
using Data;
using Gameplay;
using UnityEngine;
using UnityEngine.EventSystems;
using WallsSystem;
using Zenject;
using Random = UnityEngine.Random;

namespace CannonSystem
{
    public class CannonService : Service
    {
        [Inject] private ControlService _control;
        [Inject] private Game _game;
        [Inject] private WallsService _walls;

        public Action<float> OnChangePower;
        public Action<Vector3, Vector3> OnMovePlayer;
        public Action<List<Vector3>> OnUpdateTrajectory;
        public Action OnClickActive;
        public Action OnBulletEffect;
        public Action<RaycastHit> OnDrawing;

        private MeshGenerator _generator;
        private bool _isMoved;
        private GameplayData _data;
        private Vector3 _startPoint;
        private Vector3 _endPoint;
        private List<Vector3> _currentTr = new List<Vector3>();

        public override void InstallBindings() =>
            Container.Bind<CannonService>().FromInstance(this).AsSingle().NonLazy();

        public override void Start()
        {
            base.Start();
            _control.TouchMoved += OnDrag;
            _control.TouchStart += TouchStart;
            _control.OnClick += OnClick;

            if (_data == null) _data = _game.GetData();

            _generator = new MeshGenerator(
                new Vector3Int(_data.DetelingBulletX, _data.DetelingBulletY, _data.DetelingBulletZ),
                _data.ButlletMaterials, _data.RandomMeshGenerator);

            OnUpdateTrajectory += UpdateTrajectory;
            OnMovePlayer += MovePlayer;

            OnChangePower?.Invoke(Constants.StartCannonPower);
        }

        private void MovePlayer(Vector3 startPoint, Vector3 speed) => _startPoint = startPoint;

        private void UpdateTrajectory(List<Vector3> currentTr) => _currentTr = currentTr;

        private void OnDrag(PointerEventData eventData) => _isMoved = true;

        private void TouchStart(PointerEventData eventData) => _isMoved = false;

        private void OnClick(PointerEventData eventData)
        {
            if (_isMoved) return;
            OnClickActive?.Invoke();

            var obj = _generator.Generate();
            obj.name = Constants.Bullet;
            var scale = (Random.Range(_data.MinSizeBullet, _data.MaxSizeBullet)) / 10f;
            obj.transform.localScale = new Vector3(scale, scale, scale);
            obj.transform.position = new Vector3(_startPoint.x - scale, _startPoint.y - scale,
                _startPoint.z - scale);
            obj.AddComponent<BoxCollider>();

            var data = CalculateBulletPath();

            var fixPath = new List<Vector3>();
            foreach (var p in data.Path)
            {
                fixPath.Add(new Vector3(p.x - scale, p.y - scale, p.z - scale));
            }

            obj.AddComponent<Bullet>().Init(fixPath, _data.BulletEffect,
                () => ArrivalBullet(data));
        }

        private void ArrivalBullet(DataArrivalBullet data)
        {
            OnBulletEffect?.Invoke();

            if (data.IsUseDrawing)
            {
                if (_walls.IsHitWall(data.Hit.collider))
                {
                    OnDrawing?.Invoke(data.Hit);
                }
            }
        }

        private DataArrivalBullet CalculateBulletPath()
        {
            var path = new List<Vector3>();
            RaycastHit hit = new RaycastHit();

            for (int i = 0; i < _currentTr.Count - 1; i++)
            {
                var heading = _currentTr[i + 1] - _currentTr[i];

                if (Physics.Raycast(_currentTr[i], heading, out hit, heading.magnitude))
                {
                    if (hit.collider != null && _walls.IsHitWall(hit.collider))
                    {
                        /*
                        int textureSize = 512;
                        int brushSize = 64;
                        Texture2D texture = new Texture2D(textureSize, textureSize);
                        Texture2D textureBrush = new Texture2D(textureSize, textureSize);
                        int rayX = (int)(hit.textureCoord.x * textureSize);
                        int rayY = (int)(hit.textureCoord.y * textureSize);
                        for (int y = 0; y < brushSize; y++)
                        {
                            for (int x = 0; x < brushSize; x++)
                            {
                                texture.SetPixel(
                                    rayX + x - brushSize / 2,
                                    rayY + y - brushSize / 2,
                                    textureBrush.GetPixel(x, y));
                            }
                        }

                        texture.Apply();
                        */
                        return new DataArrivalBullet(path, true, hit);
                    }
                }

                path.Add(_currentTr[i]);
            }

            return new DataArrivalBullet(path, false, hit);
        }

        public class DataArrivalBullet
        {
            public List<Vector3> Path;
            public bool IsUseDrawing;
            public RaycastHit Hit;

            public DataArrivalBullet(List<Vector3> path, bool isUseDrawing, RaycastHit hit)
            {
                Path = path;
                IsUseDrawing = isUseDrawing;
                Hit = hit;
            }
        }
    }
}