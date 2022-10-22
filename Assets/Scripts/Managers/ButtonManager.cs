using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
public class ButtonManager : MonoBehaviour
{
    public List<Button> buttons;

    private List<Vector3> basePositions = new List<Vector3>();
    // Start is called before the first frame update
    void Start()
    {
        foreach (Button b in buttons) {

            basePositions.Add(b.transform.position);

            if (b.name.Contains("Plant")) {
                b.GetComponentInChildren<SuperTextMesh>().text = "$" + GameManager.instance.plantCost;
            }
            if (b.name.Contains("Water")) {
                b.GetComponentInChildren<SuperTextMesh>().text = "$" + GameManager.instance.waterCost;
            }
            if (b.name.Contains("Ice")) {
                b.GetComponentInChildren<SuperTextMesh>().text = "$" + GameManager.instance.iceCost;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Button b in buttons) {
            if (b.name.Contains("Plant")) {
                b.GetComponentInChildren<SuperTextMesh>().text = "$" + GameManager.instance.plantCost;
            }
            if (b.name.Contains("Water")) {
                b.GetComponentInChildren<SuperTextMesh>().text = "$" + GameManager.instance.waterCost;
            }
            if (b.name.Contains("Ice")) {
                b.GetComponentInChildren<SuperTextMesh>().text = "$" + GameManager.instance.iceCost;
            }
        }
    }

    public void OnButtonClick(Button btn) {
        SfxManager.instance.PlaySound(SoundType.BUTTON_PRESS, 0.5f);
        for (int i = 0; i < buttons.Count; i++) {
            if (buttons[i] != btn) {
                buttons[i].transform.DOMoveX(basePositions[i].x, 0.3f);
            }
            else {
                buttons[i].transform.DOMoveX(basePositions[i].x + 25, 0.3f);
            }
        }
    }
}
