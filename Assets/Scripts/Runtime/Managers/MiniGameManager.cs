using Assets.Scripts.Runtime.Commands.Collectable;
using Assets.Scripts.Runtime.Controllers.Drone;
using Assets.Scripts.Runtime.Signals;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.Managers
{
    public class MiniGameManager : MonoBehaviour
    {
        [SerializeField] private MiniGameManager miniGameManager;
        [SerializeField] private DroneAnimController droneAnimController;
        [SerializeField] private List<ColorManager> colorManager;
        [SerializeField] private PlayerMovementController playerMovementController;
        private DroneGround _droneGround;

        private void Awake()
        {
            Init();
        }
        private void Init()
        {
            _droneGround = new DroneGround(ref miniGameManager);
        }

        public void SetBlackBorder(byte value)
        {
            for (byte i = 0; i < colorManager.Count; i++)
                colorManager[i].SetBlackBorder(value);
        }
        public void ListReturn()
        {
            for (byte i = 0; i < colorManager.Count; i++)
                colorManager[i].ColorType();
        }
        public void ChangeSpeed()
        {
            playerMovementController.ChangeForwardSpeed(3);
        }
        private void OnDroneGround()
        {
            StartCoroutine(_droneGround.Execute());
        }
        private void OnEnable() => SubscribeEvents();
        private void SubscribeEvents()
        {
            StackSignals.Instance.onDroneGround += OnDroneGround;
        }
        private void UnSubscribeEvents()
        {
            StackSignals.Instance.onDroneGround -= OnDroneGround;
        }
        private void OnDisable() => UnSubscribeEvents();
    
    }
}