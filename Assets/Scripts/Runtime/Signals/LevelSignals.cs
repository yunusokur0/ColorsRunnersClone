using UnityEngine.Events;

public class LevelSignals : MonoSignleton<LevelSignals>
{
    public UnityAction<byte> onLevelInitialize = delegate { };
    public UnityAction onClearActiveLevel = delegate { };
    public UnityAction onNextLevel = delegate { };
    public UnityAction onLevelFailed = delegate { }; 
    public UnityAction onLevelSuccessful = delegate { };
    public UnityAction onLevelFinish = delegate { };
    public UnityAction onRestartLevel = delegate { };
}
