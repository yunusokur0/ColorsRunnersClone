using Assets.Scripts.Runtime.Signals;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Runtime.Commands.Collectable
{
    public class TurretRemoveStackCollectable
    {
        private readonly List<GameObject> _collectableStack;
        private Transform _poolManager;

        public TurretRemoveStackCollectable(List<GameObject> collectableStack)
        {
            _collectableStack = collectableStack;
            _poolManager = GameObject.Find("PoolManager").transform;
        }

        public void Execute()
        {
            if (_collectableStack.Count > 0)
            {
                var firstCollectable = _collectableStack.First();
                _collectableStack.Remove(firstCollectable);
                firstCollectable.SetActive(false);
                firstCollectable.transform.SetParent(_poolManager);

                StackSignals.Instance.onSetPlayerScore?.Invoke(_collectableStack.Count);
            }
        }
    }
}