using Assets.Scripts.Runtime.Controllers.Player;
using Assets.Scripts.Runtime.Data.ValueObject;
using Assets.Scripts.Runtime.Keys;
using Assets.Scripts.Runtime.Managers;
using Assets.Scripts.Runtime.Signals;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private PlayerManager manager;
    [SerializeField] private new Rigidbody rigidbody;
    private bool _isReadyToMove, _isReadyToPlay;
    private PlayerMovementData _data;
    private float _inputValue;
    private Vector2 _clampValues;
    private Vector3 jos;
    public PlayerAnimController anim;
    public bool Joystick = false;
    public Animator animm;
    internal void SetMovementData(PlayerMovementData movementData)
    {
        _data = movementData;
    }
    private void OnEnable()
    {
        SubscribeEvents();
    }
    private void SubscribeEvents()
    {
        PlayerSignals.Instance.onPlayerConditionChanged += OnPlayerConditionChange;
        PlayerSignals.Instance.onMOveConditionChanged += OnMoveConditionChanged;
    }
    private void OnMoveConditionChanged(bool arg0) => _isReadyToMove = arg0;
    private void OnPlayerConditionChange(bool arg0) => _isReadyToPlay = arg0;
    private void UnSubscribeEvents()
    {
        PlayerSignals.Instance.onPlayerConditionChanged -= OnPlayerConditionChange;
        PlayerSignals.Instance.onMOveConditionChanged -= OnMoveConditionChanged;
    }
    private void OnDisable()
    {
        UnSubscribeEvents();
    }
    public void UpdateInputValue(HorizontalInputParams inputParams)
    {
        _inputValue = inputParams.HorizontalValue;
        _clampValues = inputParams.ClampValues;
        jos = inputParams.jos;
    }
    private void FixedUpdate()
    {
        if (_isReadyToPlay)
        {
            if (_isReadyToMove) Move();
            else StopSideways();
        }
        if(!_isReadyToPlay)
        {
            if (_isReadyToMove)
            {
                MoveJoystick();
                animm.SetBool("run", true);
            }
            else
            {
                Stop();
                animm.SetBool("run", false);
            }
        }
    }
    public void ChangeForwardSpeed(float newSpeed)
    {
        _data.ForwardSpeed = newSpeed;
    }

    private void MoveJoystick()
    {
         Vector3 direction = new Vector3(jos.x*2, 0, jos.z*2);
        rigidbody.velocity = direction;

        if (direction != Vector3.zero)
        {
            Transform firstChild = transform.GetChild(0);
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            firstChild.rotation = targetRotation;
        }
    }   
    private void Move()
    {
        var velocity = rigidbody.velocity;
        velocity = new Vector3(_inputValue * _data.SidewaysSpeed, velocity.y,
            _data.ForwardSpeed);
        rigidbody.velocity = velocity;

        Vector3 position;
        position = new Vector3(
            Mathf.Clamp(rigidbody.position.x, _clampValues.x,
                _clampValues.y),
            (position = rigidbody.position).y,
            position.z);
        rigidbody.position = position;
    }
    private void StopSideways()
    {
        rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, _data.ForwardSpeed);
        rigidbody.angularVelocity = Vector3.zero;
    }
    public void Stop()
    {
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
    }
    public void OnReset()
    {
        Stop();
        _isReadyToPlay = false;
        _isReadyToMove = false;
    }
}
