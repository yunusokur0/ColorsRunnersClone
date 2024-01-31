using Assets.Scripts.Runtime.Keys;
using Assets.Scripts.Runtime.Signals;
using UnityEngine;

namespace Assets.Scripts.Runtime.Commands.Collectable
{
    public class DuringOnDraggingJoystickCommand
    {
        private Vector3 _joystickPos;
        private Vector3 _moveVector;
        private readonly Joystick _joystick;

        public DuringOnDraggingJoystickCommand(ref Vector3 joystickPos, ref Vector3 moveVector, ref Joystick joystick)
        {
            _joystickPos = joystickPos;
            _moveVector = moveVector;
            _joystick = joystick;
        }

        public void Execute()
        {
            _joystickPos = new Vector3(_joystick.Horizontal, 0, _joystick.Vertical);
            _moveVector = _joystickPos;
            InputSignals.Instance.onInputDragged?.Invoke(new HorizontalInputParams
            {
                jos = _moveVector
            });
        }
    }
}