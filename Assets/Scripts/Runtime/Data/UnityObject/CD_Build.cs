using Assets.Scripts.Runtime.Data.ValueObject;
using UnityEngine;

namespace Assets.Scripts.Runtime.Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_Build", menuName = "ColorsRunners/CD_Build", order = 0)]
    public class CD_Build : ScriptableObject
    {
        public BuildData Data;
        public SaturationValueData SaturationData;
    }
}