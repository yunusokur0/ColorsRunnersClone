using Assets.Scripts.Runtime.Signals;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Runtime.Commands.Collectable
{
    public class PlayerScaleIncrease
    {
        private readonly List<GameObject> _collectableStack;
        private Transform _poolManager;
        public PlayerScaleIncrease(List<GameObject> collectableStack)
        {
            _collectableStack = collectableStack;
            _poolManager = GameObject.Find("PoolManager").transform;
        }

        public async void Execute()
        {
            for (byte i = 0; _collectableStack.Count > 0; i++)
            {
                var firstCollectable = _collectableStack.First();
                firstCollectable.SetActive(false);
                firstCollectable.transform.SetParent(_poolManager);
                await Task.Delay(200);
                StackSignals.Instance.onPlayerScaleIncrease?.Invoke(.08f);
                _collectableStack.Remove(firstCollectable);
            }

            if (_collectableStack.Count == 0)
            {
                LevelSignals.Instance.onLevelFinish?.Invoke();
            }
        }
    }
}