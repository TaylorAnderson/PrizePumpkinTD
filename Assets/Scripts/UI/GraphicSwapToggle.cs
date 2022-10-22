using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Toggle))]
public class GraphicSwapToggle : MonoBehaviour
{
    private Toggle toggle;
    public Image graphic;
    public Sprite onSprite;
    public Sprite offSprite;
    // Start is called before the first frame update
    void Awake()
    {
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(OnValueChange);
    }
    void OnValueChange(bool on) {
        print("getting value " + on);
        graphic.sprite = on ? onSprite : offSprite;
    }
}
