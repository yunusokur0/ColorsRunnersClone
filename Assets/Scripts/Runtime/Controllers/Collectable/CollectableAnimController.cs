using Assets.Scripts.Runtime.Enums;
using UnityEngine;

namespace Assets.Scripts.Runtime.Controllers.Collectable
{
    public class CollectableAnimController : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        public void OnChangeAnimationState(AnimEnum animationState)
        {
            animator.SetTrigger(animationState.ToString());
        }
    }
}