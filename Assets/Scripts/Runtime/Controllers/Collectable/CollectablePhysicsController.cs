using Assets.Scripts.Runtime.Enums;
using Assets.Scripts.Runtime.Managers;
using Assets.Scripts.Runtime.Signals;
using UnityEngine;
namespace Assets.Scripts.Runtime.Controllers.Collectable
{
    public class CollectablePhysicsController : MonoBehaviour
    {
        [SerializeField] private CollectableManager colManager;
        [SerializeField] private CollectableMeshController colMeshController;
        [SerializeField] private CollectableAnimController ColAnimController;
        [SerializeField] private bool isTaken;

        #region Tag

        private readonly string _collectable = "Collectable";
        private readonly string _obstacle = "Obstacle";
        private readonly string _wall = "Wall";
        private readonly string _turretArea = "TurretArea";
        private readonly string _crouchWalk = "CrouchWalk";
        private readonly string _turretGroundExit = "TurretGroundExit";
        private readonly string _finish = "Finish";

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(_collectable)&& isTaken)
            {
                var otherPhysic = other.gameObject.GetComponent<CollectablePhysicsController>();
                if (!otherPhysic.isTaken)
                {
                    otherPhysic.isTaken = true;
                    StackSignals.Instance.onInteractionCollectable?.Invoke(other.transform.parent.gameObject);
                }
            }

            if (other.CompareTag(_obstacle))
            {
                colManager.InteractionWithObstacle(transform.parent.gameObject);
                other.gameObject.SetActive(false);
            }

            if (other.CompareTag(_wall))
            {
                var otherColorName = other.GetComponent<WallManager>().ColorName;
                colMeshController.CollectableColor(otherColorName);
                colManager.ColorName = other.GetComponent<WallManager>().ColorName;
            }

            if (other.CompareTag(_finish))
            {
                var otherColorName = other.GetComponent<WallManager>().ColorName;
                colMeshController.CollectableColor(otherColorName);
            }

            if (other.CompareTag(_turretArea))
            {
                ColAnimController.OnChangeAnimationState(AnimEnum.CrouchWalk);
            }

            if (other.CompareTag(_crouchWalk))
            {
                ColAnimController.OnChangeAnimationState(AnimEnum.CrouchWalk);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(_turretGroundExit))
            {
                ColAnimController.OnChangeAnimationState(AnimEnum.Run);
            }
        }
    }
}