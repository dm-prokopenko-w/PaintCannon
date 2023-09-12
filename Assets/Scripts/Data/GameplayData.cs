using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Data", menuName = "Data/GameplayData")]
    public class GameplayData : ScriptableObject
    {
        [Header("Bullet")] [Range(1, 100)] 
        public int SpeedBullet;

        [Tooltip("Count edges in bullet .")] 
        [Range(2, 5)]public int DetelingBulletX;
        [Range(2, 5)] public int DetelingBulletY;
        [Range(2, 5)] public int DetelingBulletZ;

        [Tooltip("На 6 сторон.")]
        public Material[] ButlletMaterials = new Material[6];
        [Range(0.3f, 0.7f)] public float MinSizeBullet;
        [Range(0.7f, 1f)] public float MaxSizeBullet;
        
        [Range(0.1f, 0.5f)] public float RandomMeshGenerator;
        
        public GameObject BulletEffect;
        
        [Space(5)] [Header("Camera")] public bool IsUseShakeCamera = true;
    }
}