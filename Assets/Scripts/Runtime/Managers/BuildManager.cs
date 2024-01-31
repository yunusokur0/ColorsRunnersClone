using UnityEngine;
using TMPro;
using Assets.Scripts.Runtime.Data.ValueObject;
using Assets.Scripts.Runtime.Data.UnityObject;
using DG.Tweening;
using Assets.Scripts.Runtime.Signals;

namespace Assets.Scripts.Runtime.Managers
{
    public class BuildManager : MonoBehaviour
    {
        [SerializeField] private TextMeshPro buildText;
        [SerializeField] private Material BuildMaterial;
        private BuildData _buildData;
        private const string PlayerDataPath = "Data/CD_Build";
        private SaturationValueData _saturationValueData;

        private void Awake()
        {
            _buildData = GetBuildData();
            ES3Load();
            buildText.text = (_buildData.BuildCost - _saturationValueData.ShopBuildValueText).ToString();
            BuildMaterial.DOFloat(3f / (_buildData.BuildCost / _saturationValueData.ShopBuildSaturationValue), "_Saturation", 1);
        }
        private BuildData GetBuildData() => Resources.Load<CD_Build>(PlayerDataPath).Data;
        private void ES3Load()
        {
            _saturationValueData = ES3.Load<SaturationValueData>("_SaturationValueData", _saturationValueData);
        }
        private void OnMiniGameBuildColor()
        {
            TextValueOp();
            ColorSaturation();
        }

        private SaturationValueData OnBuildSaturationValue()
        {
            return _saturationValueData;
        }
        public void TextValueOp()
        {
            _saturationValueData.ShopBuildValueText++;
            buildText.text = (_buildData.BuildCost - _saturationValueData.ShopBuildValueText).ToString();
        }

        private void ColorSaturation()
        {
            BuildMaterial.DOFloat(3f / (_buildData.BuildCost / _saturationValueData.ShopBuildSaturationValue), "_Saturation", 1);
            _saturationValueData.ShopBuildSaturationValue++;
            SaveSignals.Instance.onSaveGameData?.Invoke();
        }

        private void OnEnable() => SubscribeEvents();
        private void SubscribeEvents()
        {
            PlayerSignals.Instance.onMiniGameBuildColor += OnMiniGameBuildColor;
            SaveSignals.Instance.onBuildSaturationValue += OnBuildSaturationValue;
        }
        private void UnSubscribeEvents()
        {
            PlayerSignals.Instance.onMiniGameBuildColor -= OnMiniGameBuildColor;
            SaveSignals.Instance.onBuildSaturationValue -= OnBuildSaturationValue;
        }
        private void OnDisable() => UnSubscribeEvents();
    }
}