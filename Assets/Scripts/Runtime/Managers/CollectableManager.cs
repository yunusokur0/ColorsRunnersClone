using Assets.Scripts.Runtime.Signals;
using UnityEngine;
using static Assets.Scripts.Runtime.Data.UnityObject.CD_Color;

namespace Assets.Scripts.Runtime.Managers
{
    public class CollectableManager : MonoBehaviour
    {
        public ColorName ColorName;
        public void InteractionWithObstacle(GameObject collectableGameObject)
        {
            StackSignals.Instance.onInteractionObstacle?.Invoke(collectableGameObject);
        }
    }
}