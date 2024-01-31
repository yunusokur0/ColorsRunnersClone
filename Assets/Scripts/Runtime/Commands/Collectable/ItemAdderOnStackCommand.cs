using Assets.Scripts.Runtime.Data.ValueObject;
using Assets.Scripts.Runtime.Managers;
using Assets.Scripts.Runtime.Signals;
using System.Collections.Generic;
using UnityEngine;

public class ItemAdderOnStackCommand
{
    private StackManager _stackManager;
    private readonly List<GameObject> _collectableStack;
    private readonly StackData _data;

    public ItemAdderOnStackCommand(StackManager stackManager, List<GameObject> collectableStack,
        StackData stackData)
    {
        _stackManager = stackManager;
        _collectableStack = collectableStack;
        _data = stackData;
    }

    public void Execute(GameObject collectableGameObject)
    {
        if (_collectableStack.Count <= 0)
        {
            collectableGameObject.transform.SetParent(_stackManager.transform);
            _collectableStack.Add(collectableGameObject);
        }
        else
        {
            collectableGameObject.transform.SetParent(_stackManager.transform);
            Vector3 newPos = _collectableStack[^1].transform.localPosition;
            newPos.z -= _data.StackOffset;
            collectableGameObject.transform.localPosition = newPos;
            _collectableStack.Add(collectableGameObject);
        }
        StackSignals.Instance.onSetPlayerScore?.Invoke(_collectableStack.Count);
    }
}
