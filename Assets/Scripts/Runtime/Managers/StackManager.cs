using Assets.Scripts.Runtime.Commands.Collectable;
using Assets.Scripts.Runtime.Controllers.Collectable;
using Assets.Scripts.Runtime.Data.UnityObject;
using Assets.Scripts.Runtime.Data.ValueObject;
using Assets.Scripts.Runtime.Enums;
using Assets.Scripts.Runtime.Signals;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.Managers
{
    public class StackManager : MonoBehaviour
    {
        #region variables

        [SerializeField] private GameObject CollectableObje;
        [SerializeField] private StackManager stackManager;
        private Transform _playerManager;
        private StackData _data;
        public List<GameObject> _collectableStack = new List<GameObject>();
        private ItemAdderOnStackCommand _adderOnStackCommand;
        private ItemRemoverOnStackCommand _removerOnStackCommand;
        private StackMoverCommand _stackMoverCommand;
        private TurretRemoveStackCollectable _turretRemoveStack;
        private TransportCollectableToStack _transportCollectableToStack;
        private PlayerScaleIncrease _playerScaleIncrease;

        #endregion

        private void Awake()
        {
            _data = GetStackData();
            Init();
        }
        private void Start()
        {
            InstanceCollectableObje();
        }
        private void Init()
        {
            _adderOnStackCommand = new ItemAdderOnStackCommand(this, _collectableStack, _data);
            _removerOnStackCommand = new ItemRemoverOnStackCommand(_collectableStack);
            _stackMoverCommand = new StackMoverCommand(_collectableStack, _data);
            _transportCollectableToStack = new TransportCollectableToStack(_collectableStack);
            _turretRemoveStack = new TurretRemoveStackCollectable(_collectableStack);
            _playerScaleIncrease = new PlayerScaleIncrease(_collectableStack);
        }
        private StackData GetStackData()
        {
            return Resources.Load<CD_Stack>("Data/CD_Stack").Data;
        }
        private void OnInteractionCollectable(GameObject collectableGameObject)
        {
            _adderOnStackCommand.Execute(collectableGameObject);
        }
        private void InstanceCollectableObje()
        {
            for (byte i = 0; i < 3; i++)
            {
                GameObject clone = Instantiate(CollectableObje, new Vector3(0, 0, 2.2f), Quaternion.identity);
                _adderOnStackCommand.Execute(clone);
            }
        }
        private void FindPlayer()
        {
            if (!_playerManager) _playerManager = FindObjectOfType<PlayerManager>().transform;
        }
        private void Update()
        {
            if (!_playerManager)
                return;
            _stackMoverCommand.Execute(ref _playerManager);
        }

        public void AddInStack(GameObject obj)
        {
            _adderOnStackCommand.Execute(obj);
            CollectableAnimController _collectableAnimController = obj.transform.GetComponent<CollectableAnimController>();
            _collectableAnimController.OnChangeAnimationState(AnimEnum.Run);
        }
        private void OnPlay()
        {
            FindPlayer();
            StackSignals.Instance.onSetPlayerScore?.Invoke(_collectableStack.Count);
        }
        private void OnEnable() => SubscribeEvents();
        private void SubscribeEvents()
        {
            StackSignals.Instance.onInteractionCollectable += OnInteractionCollectable;
            StackSignals.Instance.onInteractionObstacle += _removerOnStackCommand.Execute;
            StackSignals.Instance.onTransportInStack += _transportCollectableToStack.Execute;
            StackSignals.Instance.onGetStackList += AddInStack;
            StackSignals.Instance.onFinish += _playerScaleIncrease.Execute;
            StackSignals.Instance.onTurretRemoveStack += _turretRemoveStack.Execute;
            CoreGameSignals.Instance.onPlay += OnPlay;
        }
        private void UnSubscribeEvents()
        {
            StackSignals.Instance.onInteractionCollectable -= OnInteractionCollectable;
            StackSignals.Instance.onInteractionObstacle -= _removerOnStackCommand.Execute;
            StackSignals.Instance.onTransportInStack -= _transportCollectableToStack.Execute;
            StackSignals.Instance.onGetStackList -= AddInStack;
            StackSignals.Instance.onFinish -= _playerScaleIncrease.Execute;
            StackSignals.Instance.onTurretRemoveStack -= _turretRemoveStack.Execute;
            CoreGameSignals.Instance.onPlay -= OnPlay;
        }
        private void OnDisable() => UnSubscribeEvents();
    }
}