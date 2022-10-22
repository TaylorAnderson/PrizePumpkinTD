using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelSelectManager : MonoBehaviour
{
    public List<GameObject> levels;
    public LevelWindow levelWindow;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void OnPumpkinClicked() {
        LevelSelection.levelSelected = 0;
        levelWindow.Initialize(levels[0], 0);
    }
    public void OnCarrotClicked() {
        LevelSelection.levelSelected = 1;
        levelWindow.Initialize(levels[1], 1);
    }
    public void OnEggplantClicked() {
        LevelSelection.levelSelected = 2;
    }
}
