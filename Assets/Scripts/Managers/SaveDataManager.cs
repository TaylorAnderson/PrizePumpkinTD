using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
public sealed class SaveDataManager : MonoBehaviour
{
    public static SaveDataManager instance;
    private SaveData data = new SaveData();
    private string savePath;
    private bool loadedData = false; //we might not have the right data, because we get remade each scene
    public void Awake() {
        instance = this;
        savePath = Application.persistentDataPath + "/save.data";
    }

    public void SaveGame() {
        string json = "";
        json = JsonConvert.SerializeObject(data);
        File.WriteAllText(savePath, json);
    }

    public SaveData LoadGame() {
        if (File.Exists(savePath)) {
            string fileContents = File.ReadAllText(savePath);
            data = JsonConvert.DeserializeObject<SaveData>(fileContents);
            return data;
        }
        else {
            Debug.Log("no save file exists!");
            return data;
        }
    }

    public void SetLevelData(int level, int wave) {
        LoadGame();
        if (data.levelHighScores[level] < wave)
            data.levelHighScores[level] = wave;
        SaveGame();
    }

    public void SetVolumeMultiplier(string type, float volMultiplier) {
        LoadGame();
        print("setting vol multiplier of " + volMultiplier + " on the bus " + (type == Bus.MUSIC ? "music" : "sound"));
        data.busVolume[type] = volMultiplier;
        SaveGame();
    }

    //still figuring this out, this might do something later
    public SaveData GetData() {
        if (!loadedData) {
            LoadGame();
            loadedData = true;
        }
        return data;
    }
}
