using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;
public class LevelManager : MonoBehaviour
{
    public List<GameObject> levels;
    public GameObject levelContainer;
    public UnityEvent onPumpkinDead;
    public List<Anthill> anthills;
    public Tilemap flowTilemap;
    public Tilemap pathTilemap;
    public Tilemap grassTilemap;
    public Grid grid;
    public static LevelManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        if (LevelManager.instance != null) {
            print("uh oh");
        }
        LevelManager.instance = this;
        LoadCurrentLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LoadCurrentLevel() {
        var index = LevelSelection.levelSelected;
        var currentLevel = Instantiate(levels[index]);
        currentLevel.transform.parent = levelContainer.transform;
        currentLevel.transform.localPosition = Vector2.zero;
        var levelData = currentLevel.GetComponent<LevelData>();
        levelData.onPumpkinDeath.AddListener(OnPumpkinDead);
        anthills = levelData.anthills;
        flowTilemap = levelData.flowTilemap;
        pathTilemap = levelData.pathTilemap;
        grassTilemap = levelData.grassTilemap;
        grid = levelData.grid;
    }
    
    public void OnPumpkinDead() {
        onPumpkinDead.Invoke();
    }

    
}
