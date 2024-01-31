using Assets.Scripts.Runtime.Controllers.Collectable;
using Assets.Scripts.Runtime.Enums;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.Runtime.Commands.Collectable
{
    public class CollectablePosCommand
    {
        public void Execute(GameObject other, Transform _colHolder)
        {
            var collectableManager = other.GetComponent<CollectableAnimController>();
            var randomValue = Random.Range(-1.5F, 1.5F);
            other.transform.DOMove(new Vector3(_colHolder.transform.position.x,
                other.transform.position.y,
                _colHolder.transform.position.z + randomValue), 1.1f).OnComplete(() =>
                {
                    collectableManager.OnChangeAnimationState(AnimEnum.Crouch);
                });
            other.transform.DORotate(Vector3.zero, .05f);
        }
    }
}