using Assets.Scripts.Runtime.Data.UnityObject;
using Assets.Scripts.Runtime.Data.ValueObject;
using Assets.Scripts.Runtime.Enums;
using Assets.Scripts.Runtime.Keys;
using Assets.Scripts.Runtime.Signals;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Runtime.Managers
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private PlayerMovementController movementController;
        [SerializeField] private TextMeshPro scoreText;
        [SerializeField] private ParticleSystem colorParticle;
        private PlayerData _data;
        private const string PlayerDataPath = "Data/CD_Player";      
        private int _score;
      
        private void Awake()
        {
            _data = GetPlayerData();
            SendPlayerDataToControllers();
        }
        private PlayerData GetPlayerData() => Resources.Load<CD_Player>(PlayerDataPath).Data;
        private void SendPlayerDataToControllers()
        {
            movementController.SetMovementData(_data.MovementData);
        }

        public void PlayerScale(float scale)
        {
            Vector3 Scale = transform.localScale;
            Scale += Vector3.one * scale;
            transform.localScale = Vector3.Min(Vector3.one * (4 / 5), Vector3.one * 5f);
            transform.localScale = Scale;
        }
        private void OnSetPlayerScore(int value)
        {
            _score = value;
            scoreText.text = value.ToString();
        }

        public void Build()
        {
            if (_score > 0)
            {
                PlayerSignals.Instance.onMiniGameBuildColor?.Invoke();
                SaveSignals.Instance.onSaveGameData?.Invoke();
                PlayerScale(-0.08f);
                colorParticle.Play();
                _score--;
                StackSignals.Instance.onSetPlayerScore?.Invoke(_score);
            }
        }

        private void OnPlay()
        {
            PlayerSignals.Instance.onPlayerConditionChanged?.Invoke(true);
            CameraSignals.Instance.onChangeCameraState?.Invoke(CameraStates.run);
        }
        private void OnInputDragged(HorizontalInputParams inputParams)
        {
            movementController.UpdateInputValue(inputParams);
        }
        private void OnReset()
        {
            movementController.OnReset();
        }
        private void OnEnable() => SubscribeEvents();
        private void SubscribeEvents()
        {
            StackSignals.Instance.onSetPlayerScore += OnSetPlayerScore;
            InputSignals.Instance.onInputTaken += () => PlayerSignals.Instance.onMOveConditionChanged?.Invoke(true);
            InputSignals.Instance.onInputReleased += () => PlayerSignals.Instance.onMOveConditionChanged?.Invoke(false);
            InputSignals.Instance.onInputDragged += OnInputDragged;
            CoreGameSignals.Instance.onPlay += OnPlay;
            LevelSignals.Instance.onLevelSuccessful +=
                () => PlayerSignals.Instance.onPlayerConditionChanged?.Invoke(false);
            LevelSignals.Instance.onLevelFailed +=
                () => PlayerSignals.Instance.onPlayerConditionChanged?.Invoke(false);
            CoreGameSignals.Instance.onReset += OnReset;
            StackSignals.Instance.onPlayerScaleIncrease += PlayerScale;
        }
        private void UnSubscribeEvents()
        {
            StackSignals.Instance.onSetPlayerScore -= OnSetPlayerScore;
            InputSignals.Instance.onInputTaken -= () => PlayerSignals.Instance.onMOveConditionChanged?.Invoke(true);
            InputSignals.Instance.onInputReleased -= () => PlayerSignals.Instance.onMOveConditionChanged?.Invoke(false);
            InputSignals.Instance.onInputDragged -= OnInputDragged;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            LevelSignals.Instance.onLevelSuccessful -=
                () => PlayerSignals.Instance.onPlayerConditionChanged?.Invoke(false);
            LevelSignals.Instance.onLevelFailed -=
                () => PlayerSignals.Instance.onPlayerConditionChanged?.Invoke(false);
            CoreGameSignals.Instance.onReset -= OnReset;
            StackSignals.Instance.onPlayerScaleIncrease -= PlayerScale;
        }
        private void OnDisable() => UnSubscribeEvents();
    }
}