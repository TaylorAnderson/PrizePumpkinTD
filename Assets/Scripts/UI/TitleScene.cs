using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TitleScene : MonoBehaviour
{
    // Start is called before the first frame update

    public void Start() {
    }

    public void GoToLevel(int index) {
        LevelSelection.levelSelected = index;
        SceneManager.LoadScene("GameScene");
    }
    public void GoToLevelSelectScene() {
        SceneManager.LoadScene("LevelSelectScene");
    }
    public void ExitToDesktop() {
        Application.Quit();
    }
}
