using Assets.Scripts.Runtime.Managers;
using Assets.Scripts.Runtime.Signals;
using UnityEngine;

namespace Assets.Scripts.Runtime.Controllers.Collectable
{
    public class ColorPhsyicsController : MonoBehaviour
    {
        [SerializeField] private ColorManager colorManager;
        [SerializeField] private Transform colHolder;
        private void DroneGround(Collider other)
        {
            StackSignals.Instance.onTransportInStack?.Invoke(other.transform.parent.gameObject, colHolder);
            colorManager.ColorManagerStackList.Add(other.transform.parent.gameObject);
            other.gameObject.GetComponent<Collider>().enabled = false;
            colorManager.MoveCollectablesToArea(other.transform.parent.gameObject, colHolder);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Collectable"))
                DroneGround(other);
        }
    }
}