using Assets.Scripts.Runtime.Signals;
using UnityEngine;

namespace Assets.Scripts.Runtime.Controllers.Drone
{
    public class DroneAnimController : MonoBehaviour
    {
        [SerializeField] private Animator droneAnim;
        public void DroneMove()
        {
            droneAnim.SetTrigger("Drone");
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
            StackSignals.Instance.onDroneAnim += DroneMove;
        }
        private void UnSubscribeEvents()
        {
            StackSignals.Instance.onDroneAnim -= DroneMove;
        }
        private void OnDisable()
        {
            UnSubscribeEvents();
        }
    }
}