using Assets.Scripts.Runtime.Enums;
using Assets.Scripts.Runtime.Signals;
using Cinemachine;
using UnityEngine;

namespace Assets.Scripts.Runtime.Managers
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private CinemachineStateDrivenCamera stateDrivenCamera;
        [SerializeField] private Animator animator;

        private void OnSetCinemachineTarget()
        {
            var playerManager = FindObjectOfType<PlayerManager>().transform;
            stateDrivenCamera.Follow = playerManager;
        }

        private void OnChangeCameraState(CameraStates state)
        {
            animator.SetTrigger(state.ToString());
        }

        private void OnEnable() => SubscribeEvents();
        private void SubscribeEvents()
        {
            CameraSignals.Instance.onSetCinemachineTarget += OnSetCinemachineTarget;
            CameraSignals.Instance.onChangeCameraState += OnChangeCameraState;
            CoreGameSignals.Instance.onReset += OnReset;
        }

        private void UnsubscribeEvents()
        {
            CameraSignals.Instance.onSetCinemachineTarget -= OnSetCinemachineTarget;
            CameraSignals.Instance.onChangeCameraState -= OnChangeCameraState;
            CoreGameSignals.Instance.onReset -= OnReset;
        }
        private void OnDisable() => UnsubscribeEvents();

        private void OnReset()
        {
            stateDrivenCamera.Follow = null;
            stateDrivenCamera.LookAt = null;
        }
    }
}