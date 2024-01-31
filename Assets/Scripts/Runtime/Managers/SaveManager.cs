using Assets.Scripts.Runtime.Signals;
using UnityEngine;

namespace Assets.Scripts.Runtime.Managers
{
    public class SaveManager : MonoBehaviour
    {
        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
            SaveSignals.Instance.onSaveGameData += SaveData;
        }
        private void SaveData()
        {
            OnSaveGame(
                 new SaveGameDataParams()
                 {
                     _SaturationValueData = SaveSignals.Instance.onBuildSaturationValue(),
                 }
            );
        }
        private void OnSaveGame(SaveGameDataParams saveDataParams)
        {
            ES3.Save("_SaturationValueData", saveDataParams._SaturationValueData);
        }
        private void UnsubscribeEvents()
        {
            SaveSignals.Instance.onSaveGameData -= SaveData;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
    }
}