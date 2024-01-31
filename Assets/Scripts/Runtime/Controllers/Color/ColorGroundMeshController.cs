using Assets.Scripts.Runtime.Controllers.Collectable;
using Assets.Scripts.Runtime.Data.UnityObject;
using Assets.Scripts.Runtime.Data.ValueObject;
using Assets.Scripts.Runtime.Enums;
using Assets.Scripts.Runtime.Managers;
using Assets.Scripts.Runtime.Signals;
using DG.Tweening;
using UnityEngine;
using static Assets.Scripts.Runtime.Data.UnityObject.CD_Color;

namespace Assets.Scripts.Runtime.Controllers
{
    public class ColorGroundMeshController : MonoBehaviour
    {
        public ColorName ColorName;
        private const string PlayerDataPath = "Data/CD_Color";
        private ColorData _data;
        private int stackListCount;

        [SerializeField] private MeshRenderer ColorGroundMesh;
        [SerializeField] private Transform colHolder;
        [SerializeField] private ColorManager colorCheckAreaManager;
        [SerializeField] private ColorGroundMeshController colorCheckAreaManager2;
     
        private void Awake()
        {
            _data = GetColorData();
        }
        private void Start()
        {
            CollectableColor();
        }
        public void Color()
        {
            stackListCount = colorCheckAreaManager.ColorManagerStackList.Count;
            colorCheckAreaManager.transform.GetChild(1).gameObject.SetActive(false);
            for (int i = 0; i < stackListCount; i++)
            {
                var colManager = colHolder.GetChild(0).GetComponent<CollectableManager>();
                var colAnim = colHolder.GetChild(0).GetComponent<CollectableAnimController>();
                if (ColorName == colManager.ColorName)
                {
                    colorCheckAreaManager.ColorManagerStackList.Remove(colManager.gameObject);
                    StackSignals.Instance.onGetStackList?.Invoke(colManager.gameObject);
                    colManager.gameObject.GetComponentInChildren<Collider>().enabled = true;
                }

                else
                {
                    colAnim.OnChangeAnimationState(AnimEnum.Dead);
                    colorCheckAreaManager.ColorManagerStackList.Remove(colManager.gameObject);
                    colorCheckAreaManager.ColorManagerStackList.TrimExcess();
                    colHolder.GetChild(0).gameObject.transform.parent = transform.parent;
                    transform.DOScaleY(0f, .8f).OnComplete(() => gameObject.SetActive(false));
                }
            }
        }
        public void CollectableColor()
        {
            ColorGroundMesh.material = _data.ColorMaterial[(byte)ColorName];
        }
        private ColorData GetColorData() => Resources.Load<CD_Color>(PlayerDataPath).Data;
    }
}