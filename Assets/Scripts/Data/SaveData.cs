using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public List<int> levelHighScores = new List<int>() {0,0,0};
    public Dictionary<string, float> busVolume = new Dictionary<string, float>() {{Bus.SOUND, 1}, {Bus.MUSIC, 1}};
}
