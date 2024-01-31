using Assets.Scripts.Runtime.Signals;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.Commands.Collectable
{
    public class ItemRemoverOnStackCommand
    {
        private readonly List<GameObject> _collectableStack;
        private Transform _poolManager;

        public ItemRemoverOnStackCommand(List<GameObject> collectableStack)
        {
            _collectableStack = collectableStack;
            _poolManager = GameObject.Find("PoolManager").transform;
        }

        public void Execute(GameObject collectableGameObject)
        {
            int index = _collectableStack.IndexOf(collectableGameObject);
            collectableGameObject.transform.SetParent(_poolManager);
            collectableGameObject.SetActive(false);
            _collectableStack.RemoveAt(index);
            StackSignals.Instance.onSetPlayerScore?.Invoke(_collectableStack.Count);
        }
    }
}