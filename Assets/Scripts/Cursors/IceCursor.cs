using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using DG.Tweening;
public class IceCursor : Cursor
{
    public GameObject sprite;

    public UnityEvent OnPlantFrozen;
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
            if (GameManager.instance.gold < GameManager.instance.iceCost) {
                ShowBrokeNotification(Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()));
                print("not enough money!");
                return;
            }
            var plant = GameManager.instance.GetPotUnderMouse();
            if (plant != null) {
                OnPlantFrozen.Invoke();
                sprite.transform.DORotate(Vector3.forward * 45, 0.2f).OnComplete(() => {sprite.transform.DORotate(Vector3.zero, 0.2f);});
                var pot = plant.GetComponent<PlantPot>();
                if (pot) {
                    pot.FreezeGrowth();
                }
                else {
                    plant.transform.parent.GetComponent<PlantPot>().FreezeGrowth();
                }
            }
        }
    }
}
