using Assets.Scripts.Runtime.Commands.Collectable;
using Assets.Scripts.Runtime.Commands.Color;
using Assets.Scripts.Runtime.Controllers;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.Managers
{
    public class ColorManager : MonoBehaviour
    {
        [SerializeField] private ColorGroundMeshController colorGroundMeshController;
        public List<GameObject> ColorManagerStackList;
        private BlackBorderCommand _blackBorderCommand;
        private CollectablePosCommand _collectablePositionSetCommand;
           
        private void Awake()
        {
            Init();
        }
        public void Start()
        {
            _collectablePositionSetCommand = new CollectablePosCommand();
        }
        private void Init()
        {
            _blackBorderCommand = new BlackBorderCommand(ref ColorManagerStackList);
        }
        public void SetBlackBorder(byte value)
        {
            _blackBorderCommand.Execute(value);
        }
        public void ColorType()
        {
            colorGroundMeshController.Color();
        }
        public void MoveCollectablesToArea(GameObject other, Transform _colHolder)
        {
            _collectablePositionSetCommand.Execute(other, _colHolder);
        }
    }
}