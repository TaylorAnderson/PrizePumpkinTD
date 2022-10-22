using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LevelWindow : MonoBehaviour
{
    public ShadowText levelTitle;
    public GameObject pumpkin;
    public GameObject carrot;
    public GameObject eggplant;
    public Image levelScreenshot;
    public SuperTextMesh highScoreText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize(GameObject level, int levelIndex) {

        gameObject.SetActive(true);
        var data = level.GetComponent<LevelData>();
        levelTitle.SetText(data.levelName);
        levelScreenshot.sprite = data.screenshot;
        SaveData saveData = SaveDataManager.instance.GetData();
        highScoreText.text = saveData.levelHighScores[levelIndex] + " waves"; 
    }

    public void OnPlay() {
        SceneManager.LoadScene("GameScene");
    }
    public void OnExit() {
        gameObject.SetActive(false);
    }
}
