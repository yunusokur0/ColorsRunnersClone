using Assets.Scripts.Runtime.Commands.Collectable;
using Assets.Scripts.Runtime.Keys;
using Assets.Scripts.Runtime.Signals;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Runtime.Managers
{
    public class InputManager : MonoBehaviour
    {
        private InputData _data;
        private bool isReadyForTouch, isFirstTimeTouchTaken;
        private bool _isTouching;
        private float _currentVelocity;
        private Vector2? _mousePositon;
        private Vector3 _moveVector;
        private Vector3 jos;
        private DuringOnDraggingJoystickCommand _duringOnDraggingJoystickCommand;
        private Vector3 _joystickPos;
        [SerializeField] private Joystick joystick;
        public bool _isJoystick;
        private void Awake()
        {
            _data = GetInputData();
            Init();
        }
        private InputData GetInputData() => Resources.Load<CD_Input>("Data/CD_Input").Data;
        private void Init()
        {
            _duringOnDraggingJoystickCommand =
                new DuringOnDraggingJoystickCommand(ref _joystickPos, ref jos, ref joystick);
        }

        private void OnPlay()
        {
            isReadyForTouch = true;
        }
        private void OnReset()
        {
            _isTouching = false;
            isReadyForTouch = false;
            isFirstTimeTouchTaken = false;
        }
        private void Update()
        {
            if (!isReadyForTouch) return;
            if (Input.GetMouseButtonDown(0))
            {
                _isTouching = true;
                InputSignals.Instance.onInputTaken?.Invoke();
                if (!isFirstTimeTouchTaken)
                {
                    isFirstTimeTouchTaken = true;
                }
                _mousePositon = Input.mousePosition;
            }
            if (Input.GetMouseButton(0))
            {
                if (_isTouching)
                {
                    if (_mousePositon != null)
                    {
                        if (_isJoystick)
                        {
                            _duringOnDraggingJoystickCommand.Execute();
                        }
                    }
                    if (_mousePositon != null)
                    {
                        if (!_isJoystick)
                        {


                            Vector2 mouseDeltaPos = (Vector2)Input.mousePosition - _mousePositon.Value;


                            if (mouseDeltaPos.x > _data.HorizontalInputSpeed)
                                _moveVector.x = _data.HorizontalInputSpeed / 10f * mouseDeltaPos.x;
                            else if (mouseDeltaPos.x < -_data.HorizontalInputSpeed)
                                _moveVector.x = -_data.HorizontalInputSpeed / 10f * -mouseDeltaPos.x;
                            else

                                _moveVector.x = Mathf.SmoothDamp(_moveVector.x, 0f, ref _currentVelocity,
                                    _data.ClampSpeed);

                            _mousePositon = Input.mousePosition;

                            InputSignals.Instance.onInputDragged?.Invoke(new HorizontalInputParams()
                            {
                                HorizontalValue = _moveVector.x,
                                ClampValues = _data.ClampSides
                            });
                        }
                    }
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                _isTouching = false;
                InputSignals.Instance.onInputReleased?.Invoke();
            }
        }
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void isJoystick(bool arg0) => _isJoystick = arg0;
        private void SubscribeEvents()
        {
            InputSignals.Instance.isJoystick += isJoystick;
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onReset += OnReset;
        }

        private void UnSubscribeEvents()
        {
            InputSignals.Instance.isJoystick -= isJoystick;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onReset -= OnReset;
        }
        private void OnDisable()
        {
            UnSubscribeEvents();
        }
        private bool IsPointerOverUIElement()
        {
            var eventData = new PointerEventData(EventSystem.current);
            eventData.position = Input.mousePosition;
            var results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);
            return results.Count > 0;
        }
    }
}