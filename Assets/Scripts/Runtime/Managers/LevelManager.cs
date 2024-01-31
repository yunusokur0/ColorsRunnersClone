using Assets.Scripts.Runtime.Commands.Level;
using Assets.Scripts.Runtime.Signals;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] internal GameObject levelHolder;
    [SerializeField] private byte totalLevelCount;
    private byte _currentLevel;
    private LevelDestroyerCommand _levelDestroyer;
    private LevelLoaderCommand _leveLoader;

    private void Awake()
    {
        Init();
        _currentLevel = GetActiveLevel();
    }

    private void Start()
    {
        LevelSignals.Instance.onLevelInitialize?.Invoke(0);
    }

    private void Init()
    {
        _leveLoader = new LevelLoaderCommand(this);
        _levelDestroyer = new LevelDestroyerCommand(this);

    }

    private byte GetActiveLevel()
    {
        if (!ES3.FileExists()) return 0;
        return (byte)(ES3.KeyExists("Level") ? ES3.Load<byte>("Level") % totalLevelCount : 0);
    }
    private void OnNextLevel()
    {
        _currentLevel++;
        SaveSignals.Instance.onSaveGameData?.Invoke();
        LevelSignals.Instance.onClearActiveLevel?.Invoke();
        LevelSignals.Instance.onLevelInitialize?.Invoke(_currentLevel);
        CoreGameSignals.Instance.onReset?.Invoke();
    }

    private void OnRestartLevel()
    {
        SaveSignals.Instance.onSaveGameData?.Invoke();
        LevelSignals.Instance.onClearActiveLevel?.Invoke();
        LevelSignals.Instance.onLevelInitialize?.Invoke(_currentLevel);
        CoreGameSignals.Instance.onReset?.Invoke();
    }
    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        LevelSignals.Instance.onClearActiveLevel += _levelDestroyer.Execute;
        LevelSignals.Instance.onLevelInitialize += _leveLoader.Execute;
        LevelSignals.Instance.onNextLevel += OnNextLevel;
        LevelSignals.Instance.onRestartLevel += OnRestartLevel;
    }

    private void UnSubscribeEvents()
    {
        LevelSignals.Instance.onClearActiveLevel -= _levelDestroyer.Execute;
        LevelSignals.Instance.onLevelInitialize -= _leveLoader.Execute;
        LevelSignals.Instance.onNextLevel -= OnNextLevel;
        LevelSignals.Instance.onRestartLevel -= OnRestartLevel;
    }
    private void OnDisable()
    {
        UnSubscribeEvents();
    }
}
