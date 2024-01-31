using Assets.Scripts.Runtime.Data.UnityObject;
using Assets.Scripts.Runtime.Data.ValueObject;
using UnityEngine;
using static Assets.Scripts.Runtime.Data.UnityObject.CD_Color;

namespace Assets.Scripts.Runtime.Controllers.Color
{
    public class TurretColorGroundMeshController : MonoBehaviour
    {
        private const string PlayerDataPath = "Data/CD_Color";
        [SerializeField] private MeshRenderer ColorGroundMesh;
        private ColorData _data;
        public ColorName ColorName;

        private void Awake()
        {
            _data = GetColorData();
        }
        private void Start()
        {
            CollectableColor();
        }
        public void CollectableColor()
        {
            ColorGroundMesh.material = _data.ColorMaterial[(byte)ColorName];
        }
        private ColorData GetColorData() => Resources.Load<CD_Color>(PlayerDataPath).Data;
    }
}