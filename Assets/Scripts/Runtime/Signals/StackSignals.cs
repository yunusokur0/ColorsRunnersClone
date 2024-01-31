using UnityEngine;
using UnityEngine.Events;
namespace Assets.Scripts.Runtime.Signals
{
    public class StackSignals : MonoSignleton<StackSignals>
    {
        public UnityAction<GameObject> onInteractionCollectable = delegate { };
        public UnityAction<GameObject> onInteractionObstacle = delegate { };
        public UnityAction onTurretRemoveStack = delegate { };
        public UnityAction onDroneGround = delegate { };
        public UnityAction<GameObject> onGetStackList = delegate { };
        public UnityAction onDroneAnim = delegate { };
        public UnityAction onFinish = delegate { };
        public UnityAction<float> onPlayerScaleIncrease = delegate { };
        public UnityAction<int> onSetPlayerScore = delegate { };
        public UnityAction<GameObject, Transform> onTransportInStack = delegate { };     
    }
}