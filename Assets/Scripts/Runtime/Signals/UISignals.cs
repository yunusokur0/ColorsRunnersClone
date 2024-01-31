using UnityEngine.Events;

namespace Assets.Scripts.Runtime.Signals
{
    public class UISignals : MonoSignleton<UISignals>
    {
        public UnityAction<byte> onSetScoreText;
    }
}