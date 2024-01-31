using Assets.Scripts.Runtime.Signals;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.Runtime.Managers
{
    public class LaserGunManager : MonoBehaviour
    {
        [SerializeField] public DOTweenAnimation rightLaserAnim, leftLaserAnim;
        [SerializeField] public Transform rightTurret, leftTurret;
        [SerializeField] public Transform player;
        [SerializeField] public bool yes;
        [SerializeField] public ParticleSystem _rightTurretParticle, _leftTurretParticle;

        private void Start()
        {
            InvokeRepeating("DeactivateFirstCollectable", 0.2f, 1f);
        }
        private void GunLook()
        {
            rightLaserAnim.DOPause();
            leftLaserAnim.DOPause();
            rightTurret.DOLookAt(player.position, 0.5f);
            leftTurret.DOLookAt(player.position, 0.5f);
        }

        private void DeactivateFirstCollectable()
        {
            if (yes)
            {
                _rightTurretParticle.Play();
                _leftTurretParticle.Play();
                StackSignals.Instance.onTurretRemoveStack?.Invoke();
            }
        }

        private void Update()
        {
            if (yes)
            {
                GunLook();
            }
            else
            {
                rightLaserAnim.DOPlay();
                leftLaserAnim.DOPlay();
                _rightTurretParticle.Stop();
                _leftTurretParticle.Stop();
            }

        }

        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
            PlayerSignals.Instance.onLaserFiring += OnPlayerConditionChange;
        }
        private void OnDisable()
        {
            UnSubscribeEvents();
        }
        private void OnPlayerConditionChange(bool arg0) => yes = arg0;
        private void UnSubscribeEvents()
        {
            PlayerSignals.Instance.onLaserFiring -= OnPlayerConditionChange;
        }

    }
}



