using Assets.Scripts.Runtime.Managers;
using Assets.Scripts.Runtime.Signals;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Runtime.Commands.Collectable
{
    public class DroneGround 
    {
        private readonly MiniGameManager _manager;

        public DroneGround(ref MiniGameManager manager)
        {
            _manager = manager;
        }
        public IEnumerator Execute()
        {
            yield return new WaitForSeconds(1);
            _manager.SetBlackBorder(0);
            StackSignals.Instance.onDroneAnim?.Invoke();

            yield return new WaitForSeconds(3.5f);
            _manager.SetBlackBorder(50);
            _manager.ListReturn();
            _manager.ChangeSpeed();
        }
    }
}