using System.Collections.Generic;
using Core;
using UnityEngine;
using Zenject;

namespace CannonSystem
{
    public class TrajectoryRenderer : MonoBehaviour
    {
        [Inject] private CannonService _cannon;

        [SerializeField] private LineRenderer _line;

        private void Start()
        {
            _cannon.OnMovePlayer += ShowTrajectory;
        }

        private void OnDestroy()
        {
            _cannon.OnMovePlayer -= ShowTrajectory;
        }

        private void ShowTrajectory(Vector3 origin, Vector3 speed)
        {
            List<Vector3> points = new List<Vector3>();

            for (int i = 0; i < Constants.MaxCountPointsTrajectory; i++)
            {
                float time = i * 0.04f;
                points.Add(origin + speed * time + Physics.gravity * time * time / 2f);

                if (points[i].y < 0)
                {
                    break;
                }
            }

            _line.positionCount = points.Count;
            _line.SetPositions(points.ToArray());

            _cannon.OnUpdateTrajectory?.Invoke(points);
        }
    }
}
