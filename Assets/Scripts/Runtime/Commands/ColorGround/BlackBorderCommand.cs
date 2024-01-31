using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.Commands.Color
{
    public class BlackBorderCommand
    {
        private List<GameObject> _stackList;

        public BlackBorderCommand(ref List<GameObject> stack)
        {
            _stackList = stack;
        }
        public void Execute(float endValue)
        {
            for (var i = 0; i < _stackList.Count; i++)
            {
                var materialColor = _stackList[i].GetComponentInChildren<SkinnedMeshRenderer>().material;
                materialColor.DOFloat(endValue, "_OutlineSize", 1f);
            }
        }
    }
}