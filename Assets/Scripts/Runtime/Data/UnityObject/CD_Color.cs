using Assets.Scripts.Runtime.Data.ValueObject;
using UnityEngine;

namespace Assets.Scripts.Runtime.Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_Color", menuName = "ColorsRunners/CD_Color", order = 0)]
    public class CD_Color : ScriptableObject
    {
        public enum ColorName
        {
            Red,
            Green,
            Blue,
            Rain
        }
        public ColorData Data;
    }
}