using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Cursor : MonoBehaviour
{
    public float gridSize = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 GetMousePosInGrid() {
        var mousePos = Mouse.current.position.ReadValue();
        mousePos =Camera.main.ScreenToWorldPoint(mousePos);
        return SnapPosToGrid(mousePos, gridSize, gridSize/2);
    }

    public Vector3 SnapPosToGrid(Vector3 pos, float gridSize, float offset) {
        return new Vector3((Mathf.Round(pos.x / gridSize + offset) * gridSize) - offset, (Mathf.Round(pos.y / gridSize + offset) * gridSize)-offset, 0);
    }

    public void ChangeSpriteAlpha(GameObject go, float alpha) {
        var sp = go.GetComponent<SpriteRenderer>();
        var color = sp.color;
        color.a = alpha;
        sp.color = color;
    }

    public GameObject GetPlantOnMouse() {
        foreach (var p in GameManager.instance.plants) {
            if (p.transform.position == GetMousePosInGrid()) {
                return p;
            }
        }
        return null;
    }
}
