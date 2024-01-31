using UnityEngine.Events;

namespace Assets.Scripts.Runtime.Signals
{
    public class CoreUISignals : MonoSignleton<CoreUISignals>
    {
        public UnityAction<byte> onClosePanel = delegate { };
        public UnityAction<UIPanelTypes, byte> onOpenPanel = delegate { };
    }
}