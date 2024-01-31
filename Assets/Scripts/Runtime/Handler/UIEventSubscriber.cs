using Assets.Scripts.Runtime.Enums;
using Assets.Scripts.Runtime.Managers;
using System;
using UnityEngine;
using UnityEngine.UI;

public class UIEventSubscriber : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private UIEventSubscriptionTypes type;
    private UIManager _uiManager;

    private void Awake()
    {
        FindRefernces();
    }
    private void FindRefernces()
    {
        _uiManager = FindObjectOfType<UIManager>();
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        switch (type)
        {
            case UIEventSubscriptionTypes.OnPlay:
                button.onClick.AddListener(_uiManager.OnPlay);
                break;
            case UIEventSubscriptionTypes.OnNextLevel:
                button.onClick.AddListener(_uiManager.OnNextLevel);
                break;
            case UIEventSubscriptionTypes.OnRestartLevel:
                button.onClick.AddListener(_uiManager.OnRestartLevel);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void UnSubscribeEvents()
    {
        switch (type)
        {
            case UIEventSubscriptionTypes.OnPlay:
                button.onClick.AddListener(_uiManager.OnPlay);
                break;
            case UIEventSubscriptionTypes.OnNextLevel:
                button.onClick.AddListener(_uiManager.OnNextLevel);
                break;
            case UIEventSubscriptionTypes.OnRestartLevel:
                button.onClick.AddListener(_uiManager.OnRestartLevel);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    private void OnDisable()
    {
        UnSubscribeEvents();
    }
}
