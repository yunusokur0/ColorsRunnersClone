using Assets.Scripts.Runtime.Data.ValueObject;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.Commands.Collectable
{
    public class StackMoverCommand
    {
        private readonly List<GameObject> _stackList;
        private readonly StackData _stackData;
        public StackMoverCommand(List<GameObject> stackList, StackData stackData)
        {
            _stackList = stackList;
            _stackData = stackData;
        }

        public void Execute(ref Transform _playerTransform)
        {
            if (_stackList.Count > 0)
            {
                MoveTowards(_stackList[0], _playerTransform.position, _stackData.StackLerpXDelay, 1f, _stackData.StackLerpZDelay, _stackData.StackOffset);
                for (var i = 1; i < _stackList.Count; i++)
                {
                    MoveTowards(_stackList[i], _stackList[i - 1].transform.localPosition, _stackData.StackLerpXDelay, _stackData.StackLerpYDelay, _stackData.StackLerpZDelay, _stackData.StackOffset);
                }
            }
        }

        private void MoveTowards(GameObject stackObj, Vector3 targetPosition, float lerpXDelay, float lerpYDelay, float lerpZDelay, float stackOffset)
        {
            var directX = Mathf.Lerp(stackObj.transform.localPosition.x, targetPosition.x, lerpXDelay);
            var directY = Mathf.Lerp(stackObj.transform.localPosition.y, targetPosition.y, lerpYDelay);
            var directZ = Mathf.Lerp(stackObj.transform.localPosition.z, targetPosition.z - stackOffset, lerpZDelay);
            stackObj.transform.localPosition = new Vector3(directX, directY, directZ);
        }
    }
}