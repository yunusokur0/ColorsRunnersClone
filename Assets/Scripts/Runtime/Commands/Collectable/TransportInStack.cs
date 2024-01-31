using Assets.Scripts.Runtime.Signals;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.Commands.Collectable
{
    public class TransportCollectableToStack
    {
        private readonly List<GameObject> _stackList;

        public TransportCollectableToStack(List<GameObject> stackList)
        {
            _stackList = stackList;
        }

        public void Execute(GameObject collectable, Transform targetStackTransform)
        {
            _stackList.Remove(collectable);
            collectable.transform.parent = targetStackTransform;

            if (_stackList.Count == 0)
            {
                StackSignals.Instance.onDroneGround?.Invoke();
            }
        }
    }
}
