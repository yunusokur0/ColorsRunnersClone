using UnityEngine.Events;

namespace Assets.Scripts.Runtime.Signals
{
    public class CoreGameSignals : MonoSignleton<CoreGameSignals>
    {
        public UnityAction onPlay = delegate { };
        public UnityAction onReset = delegate { };
    }
}