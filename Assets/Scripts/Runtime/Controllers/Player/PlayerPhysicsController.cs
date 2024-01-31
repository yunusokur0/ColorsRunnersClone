using Assets.Scripts.Runtime.Enums;
using Assets.Scripts.Runtime.Managers;
using Assets.Scripts.Runtime.Signals;
using UnityEngine;

namespace Assets.Scripts.Runtime.Controllers.Player
{
    public class PlayerPhysicsController : MonoBehaviour
    {      
        [SerializeField] private PlayerMovementController playerMoveController;
        [SerializeField] private PlayerManager playerManager;
        [SerializeField] private GameObject running;
        [SerializeField] private GameObject score;
        private float _timer;

        #region tag

        private readonly string _collectable = "Collectable";
        private readonly string _droneArea = "DroneArea";
        private readonly string _turretArea = "TurretArea";
        private readonly string _crouchWalk = "CrouchWalk";
        private readonly string _build = "Build";
        private readonly string _turretGroundExit = "TurretGroundExit";
        private readonly string _finish = "Finish";

        #endregion
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(_collectable))
            {
                StackSignals.Instance.onInteractionCollectable?.Invoke(other.transform.parent.gameObject);
            }

            if (other.CompareTag(_droneArea))
            {
                playerMoveController.ChangeForwardSpeed(0);
                score.SetActive(false);
            }
            if (other.CompareTag(_turretArea))
            {
                playerMoveController.ChangeForwardSpeed(1.5f);
                PlayerSignals.Instance.onLaserFiring?.Invoke(true);
            }
            if (other.CompareTag(_crouchWalk))
            {
                playerMoveController.ChangeForwardSpeed(1.5f);
                PlayerSignals.Instance.onLaserFiring?.Invoke(false);
            }        
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag(_build))
            {
                if (_timer >= .8f)
                {
                    playerManager.Build();

                     _timer = 0;
                }
                else
                    _timer += Time.deltaTime;
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(_turretGroundExit))
            {
                playerMoveController.ChangeForwardSpeed(3f);
                PlayerSignals.Instance.onLaserFiring?.Invoke(false);
            }

            if(other.CompareTag(_finish))
            {
                StackSignals.Instance.onFinish?.Invoke();
                PlayerSignals.Instance.onPlayerConditionChanged?.Invoke(false);
                InputSignals.Instance.isJoystick?.Invoke(true);
                CameraSignals.Instance.onChangeCameraState?.Invoke(CameraStates.idle);
                running.SetActive(true);
            }

            if(other.CompareTag(_droneArea))
            {
                score.SetActive(true);
            }
        }
    }
}