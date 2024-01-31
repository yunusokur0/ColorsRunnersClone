using Assets.Scripts.Runtime.Data.ValueObject;
using System;
using UnityEngine.Events;
namespace Assets.Scripts.Runtime.Signals
{
    public class SaveSignals : MonoSignleton<SaveSignals>
    {
        public UnityAction onSaveGameData = delegate { };
        public Func<SaturationValueData> onBuildSaturationValue = delegate { return default; };
    }
}