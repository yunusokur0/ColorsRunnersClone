using UnityEngine.Events;

namespace Assets.Scripts.Runtime.Signals
{
    public class PlayerSignals : MonoSignleton<PlayerSignals>
    {
        public UnityAction<bool> onPlayerConditionChanged = delegate { };
        public UnityAction<bool> onMOveConditionChanged = delegate { };
        public UnityAction<bool> onLaserFiring = delegate { };
        public UnityAction onMiniGameBuildColor = delegate { };
    }
}