using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using DG.Tweening;
using UnityEngine.EventSystems;
public class WaterCursor : Cursor
{
    public GameObject sprite;
    public UnityEvent OnPlantWatered;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var mp = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        sprite.transform.position = new Vector3(mp.x, mp.y, 0);
    }

    public void OnClick(InputAction.CallbackContext ctx) {
        if (!gameObject.activeSelf) return;
        if (ctx.performed) {
            if (GameManager.instance.gold < GameManager.instance.waterCost) {
                print("not enough money!");
                return;
            }
            if (EventSystem.current.IsPointerOverGameObject()) {
                return;
            }
            var pot = GameManager.instance.GetPotUnderMouse();
            if (pot != null) {
                OnPlantWatered.Invoke();
                sprite.transform.DORotate(Vector3.forward * 45, 0.2f).OnComplete(() => {sprite.transform.DORotate(Vector3.zero, 0.2f);});
                pot.GetComponent<PlantPot>().AccelerateGrowth();
            }
        }

    }
}
