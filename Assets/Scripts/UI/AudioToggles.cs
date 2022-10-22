using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AudioToggles : MonoBehaviour
{
    public Toggle musicToggle;
    public Toggle soundToggle;
    public void Start() {
        var saveData = SaveDataManager.instance.GetData();
        print(saveData.busVolume[Bus.MUSIC]);
        musicToggle.isOn = saveData.busVolume[Bus.MUSIC] == 1;
        soundToggle.isOn = saveData.busVolume[Bus.SOUND] == 1;

    }
    public void OnMusicToggleSwitched(bool on) {
        SaveDataManager.instance.SetVolumeMultiplier(Bus.MUSIC, on ? 1 : 0);
    }
    
    public void OnSoundToggleSwitched(bool on) {
        SaveDataManager.instance.SetVolumeMultiplier(Bus.SOUND, on ? 1 : 0);
    }
}
