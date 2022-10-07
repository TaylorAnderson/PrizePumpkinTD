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

    private Tilemap tilemap;

    [HideInInspector]
    public TileBase tileUnderMouse;

    // Start is called before the first frame update
    void Start() {
        tilemap = grid.GetComponentInChildren<Tilemap>();
        LoadNewPot();
    }

    // Update is called once per frame
    void Update()
    {
        var potPos = GetMousePosInGrid();
        tileUnderMouse = tilemap.GetTile(grid.WorldToCell(potPos));
        if (currentPot) {
            currentPot.transform.position = potPos;
        }
    }

    void LoadNewPot() {
        currentPot = Instantiate(potPrefab);
        currentPot.GetComponent<BoxCollider2D>().enabled = false;
        ChangeSpriteAlpha(currentPot, 0.5f);
    }



    public void OnMouseClick(InputAction.CallbackContext ctx) {
        if (!gameObject.activeSelf) return;
        if (ctx.performed) {
            if (GameManager.instance.gold < GameManager.instance.plantCost) {
                print("not enough money!");
                return;
            }
            if (EventSystem.current.IsPointerOverGameObject()) {
                return;
            }
            if (currentPot != null && !GameManager.instance.GetPotUnderMouse() && tileUnderMouse != null && !tileUnderMouse.name.Contains("path")) {
                onPotPlaced.Invoke();
                ChangeSpriteAlpha(currentPot, 1.0f);
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
