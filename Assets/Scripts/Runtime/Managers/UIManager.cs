using Assets.Scripts.Runtime.Signals;
using UnityEngine;

namespace Assets.Scripts.Runtime.Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject Joystick;
        private void OnLevelInitialize(byte levelValue)
        {
            CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Level, 0);
            CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Start, 1);
        }
        private void OnLevelFailed()
        {
            CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Fail, 3);
        }
        private void OnLevelSuccessful()
        {
            CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Win, 3);
        }

        private void OnLevelFinish()
        {
            Joystick.SetActive(true);
        }

        public void OnPlay()
        {
            CoreGameSignals.Instance.onPlay?.Invoke();
            CoreUISignals.Instance.onClosePanel?.Invoke(1);
        }
        public void OnNextLevel()
        {
            CoreUISignals.Instance.onClosePanel?.Invoke(3);
            LevelSignals.Instance.onNextLevel?.Invoke();
        }
        public void OnRestartLevel()
        {
            CoreUISignals.Instance.onClosePanel?.Invoke(3);
            LevelSignals.Instance.onRestartLevel?.Invoke();
        }
        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
            LevelSignals.Instance.onLevelInitialize += OnLevelInitialize;
            LevelSignals.Instance.onLevelFinish += OnLevelFinish;
            LevelSignals.Instance.onLevelFailed += OnLevelFailed;
            LevelSignals.Instance.onLevelSuccessful += OnLevelSuccessful;
        }
        private void UnSubscribeEvents()
        {
            LevelSignals.Instance.onLevelInitialize -= OnLevelInitialize;
            LevelSignals.Instance.onLevelFinish -= OnLevelFinish;
            LevelSignals.Instance.onLevelFailed -= OnLevelFailed;
            LevelSignals.Instance.onLevelSuccessful -= OnLevelSuccessful;
        }
        private void OnDisable()
        {
            UnSubscribeEvents();
        }
    }
}