using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private Vector2 detectRange = Vector2.one * 0.2f;
    public List<Waypoint> waypoints;
    public PlantCursor plantCursor;
    public WaterCursor waterCursor;
    public IceCursor iceCursor;
    public Transform midPoint;

    public int goldForKill = 1;

    public List<GameObject> plants;

    public GameObject gameOver;
    public GameObject gameWon;
    
    public int gold = 3;

    public int plantCost = 3;
    public int waterCost = 3;
    public int iceCost = 3;
    public bool mouseLock;

    public List<Anthill> anthills;
    public UnityEvent OnGoldChange;

    void Awake() {
        if (GameManager.instance != null) {
            print("uh oh");
        }
        GameManager.instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        OnGoldChange.Invoke();   
        SfxManager.instance.PlaySound(SoundType.THEME);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public GameObject GetPotUnderMouse() {
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Collider2D detectedCollider = Physics2D.OverlapBox(mouseWorldPosition, detectRange, 0);
        if (detectedCollider && detectedCollider.gameObject.layer == LayerMask.NameToLayer("Pot")) {
            return detectedCollider.gameObject;
        }
        return null;
    }
    public void OnEnemyKilled(Enemy enemy) {
        gold += enemy.gold;
        OnGoldChange.Invoke();
    }

    public void OnBuyPot() {
        mouseLock = true;
        iceCursor.gameObject.SetActive(false);
        waterCursor.gameObject.SetActive(false);
        plantCursor.gameObject.SetActive(true);
    }
    public void OnBuyWater() {
        print("switched to water");
        mouseLock = true;
        iceCursor.gameObject.SetActive(false);
        waterCursor.gameObject.SetActive(true);
        plantCursor.gameObject.SetActive(false);
    }
    public void OnBuyIce() {
        mouseLock = true;
        iceCursor.gameObject.SetActive(true);
        waterCursor.gameObject.SetActive(false);
        plantCursor.gameObject.SetActive(false);
    }

    public void OnPotPlaced() {
        SfxManager.instance.PlaySound(SoundType.POT_PLACED);
        gold -= plantCost;
        plantCost += 3;
        OnGoldChange.Invoke();
    }
    public void OnPlantWatered() {
        SfxManager.instance.PlaySound(SoundType.PLANT_WATERED);
        gold -= waterCost;
        OnGoldChange.Invoke();
    }
    public void OnPlantFrozen() {
        SfxManager.instance.PlaySound(SoundType.PLANT_FROZEN);
        gold -= iceCost;
        OnGoldChange.Invoke();
    }

    public void OnPumpkinDead() {
        foreach (var anthill in anthills) {
            foreach (var e in anthill.enemies) {
                Destroy(e.gameObject);
            }
        }
        SfxManager.instance.StopAllSounds();
        SfxManager.instance.PlaySound(SoundType.GAME_OVER);
        gameOver.SetActive(true);
    }
    public void OnWinGame() {
        SfxManager.instance.StopAllSounds();
        SfxManager.instance.PlaySound(SoundType.GAME_WON, 1, true);
        gameWon.SetActive(true);
    }
}
