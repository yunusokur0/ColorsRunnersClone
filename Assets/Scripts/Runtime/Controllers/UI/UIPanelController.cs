using Assets.Scripts.Runtime.Signals;
using System.Collections.Generic;
using UnityEngine;

public class UIPanelController : MonoBehaviour
{
    [SerializeField] private List<Transform> layers = new List<Transform>();

    private void OnEnable()
    {
        SubscribeEvents();
    }
    private void SubscribeEvents()
    {
        CoreUISignals.Instance.onClosePanel += OnClosePanel;
        CoreUISignals.Instance.onOpenPanel += OnOpenPanel;
    }
    private void OnOpenPanel(UIPanelTypes panelType, byte value)
    {
        OnClosePanel(value);
        Instantiate(Resources.Load<GameObject>($"Screens/{panelType}Panel"), layers[value]);
    }

    private void OnClosePanel(byte value)
    {
        if (layers[value].childCount <= 0) return;
        DestroyImmediate(layers[value].GetChild(0).gameObject);
    }

    private void UnSubscribeEvents()
    {
        CoreUISignals.Instance.onClosePanel -= OnClosePanel;
        CoreUISignals.Instance.onOpenPanel -= OnOpenPanel;
    }

    private void OnDisable()
    {
        UnSubscribeEvents();
    }
}
