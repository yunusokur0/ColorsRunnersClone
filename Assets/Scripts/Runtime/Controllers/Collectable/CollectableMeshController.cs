using Assets.Scripts.Runtime.Data.UnityObject;
using Assets.Scripts.Runtime.Data.ValueObject;
using UnityEngine;
using static Assets.Scripts.Runtime.Data.UnityObject.CD_Color;

namespace Assets.Scripts.Runtime.Controllers.Collectable
{
    public class CollectableMeshController : MonoBehaviour
    {
        [SerializeField] private SkinnedMeshRenderer collectableSkinMeshRenderer;
        public ColorName ColorName;
        private const string PlayerDataPath = "Data/CD_Color";        
        private ColorData _data;

        private void Awake()
        {
            _data = GetColorData();
        }
        private void Start()
        {
            CollectableColor(ColorName);
        }
        public void CollectableColor(ColorName ColorName)
        {
            this.ColorName = ColorName;
            collectableSkinMeshRenderer.material = _data.ColorMaterial[(byte)ColorName];
        }
        private ColorData GetColorData() => Resources.Load<CD_Color>(PlayerDataPath).Data;
    }
}
