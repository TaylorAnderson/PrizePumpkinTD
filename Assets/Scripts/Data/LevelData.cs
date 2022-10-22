using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;
public enum Veggie {
    CARROT,
    PUMPKIN,
    EGGPLANT
}
public class LevelData : MonoBehaviour
{
    public List<Anthill> anthills;
    public Tilemap pathTilemap;
    public Tilemap grassTilemap;
    public Tilemap flowTilemap;
    public Grid grid;
    public UnityEvent onPumpkinDeath;
    public string levelName;
    public Veggie vegetable;
    public Sprite screenshot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPumpkinDeath() {
        onPumpkinDeath.Invoke();
    }
}
