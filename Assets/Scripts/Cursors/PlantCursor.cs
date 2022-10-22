using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.AI;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class PlantCursor : Cursor
{
    public GameObject potPrefab;
    
    private GameObject currentPot;

    public Grid grid;

    public UnityEvent onPotPlaced;

    public Tilemap grassTilemap;
    public Tilemap dirtTilemap;

    [HideInInspector]
    public bool tileUnderMouseValid;

    // Start is called before the first frame update
    void Start() {
        LoadNewPot();
    }

    // Update is called once per frame
    void Update()
    {
        var potPos = GetMousePosInGrid();
        tileUnderMouseValid = LevelManager.instance.grassTilemap.GetTile(LevelManager.instance.grid.WorldToCell(potPos)) != null && LevelManager.instance.pathTilemap.GetTile(LevelManager.instance.grid.WorldToCell(potPos)) == null;
        if (currentPot) {
            currentPot.transform.position = potPos;
        }
    }

    void LoadNewPot() {
        currentPot = Instantiate(potPrefab);
        currentPot.GetComponent<BoxCollider2D>().enabled = false;
        ChangeSpriteAlpha(currentPot, 0.5f);
        currentPot.GetComponent<PlantPot>().shadowSprite.enabled = false;
    }



    public void OnMouseClick(InputAction.CallbackContext ctx) {
        if (!gameObject.activeSelf) return;
        if (ctx.performed) {
            print("performed");
            if (GameManager.instance.gold < GameManager.instance.plantCost) {
                print("not enough money!");
                ShowBrokeNotification(GetMousePosInGrid());
                return;
            }
            if (currentPot != null && !GameManager.instance.GetPotUnderMouse() && tileUnderMouseValid) {
                onPotPlaced.Invoke();
                ChangeSpriteAlpha(currentPot, 1.0f);
                currentPot.GetComponent<PlantPot>().shadowSprite.enabled = true;
                currentPot.GetComponent<PlantPot>().OnPlaced();
                GameManager.instance.plants.Add(currentPot);
                currentPot.GetComponent<BoxCollider2D>().enabled = true;
                currentPot = null;
                LoadNewPot();
            }
        }
    }
    public void OnEnable() {
        if (currentPot) {
            currentPot.SetActive(true);
        }
    }
    public void OnDisable() {
        if (currentPot) {
            currentPot.SetActive(false);
        }
    }
}
