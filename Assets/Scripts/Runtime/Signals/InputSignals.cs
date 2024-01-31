using Assets.Scripts.Runtime.Keys;
using UnityEngine.Events;

namespace Assets.Scripts.Runtime.Signals
{
    public class InputSignals : MonoSignleton<InputSignals>
    {
        public UnityAction onInputTaken = delegate { };
        public UnityAction<bool> isJoystick = delegate { };
        public UnityAction onInputReleased = delegate { };
        public UnityAction<HorizontalInputParams> onInputDragged = delegate { };
    }
}