using Assets.Scripts.Runtime.Enums;
using UnityEngine.Events;

namespace Assets.Scripts.Runtime.Signals
{
    public class CameraSignals : MonoSignleton<CameraSignals>
    {
        public UnityAction<CameraStates> onChangeCameraState = delegate { };
        public UnityAction onSetCinemachineTarget = delegate { };
    }
}