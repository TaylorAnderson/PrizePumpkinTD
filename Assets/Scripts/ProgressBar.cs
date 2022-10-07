using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PowerTools;
public class ProgressBar : MonoBehaviour
{
    public List<Sprite> sprites = new List<Sprite>();
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void SetProgress(float progress) {
        var index = Mathf.FloorToInt(Map(progress, 0, 1, 0, sprites.Count -1)) - 1;
        spriteRenderer.sprite = sprites[index];
    }

    public float Map (float value, float from, float to, float from2, float to2) {
        return (value - from) * (to2 - from2) / (to - from) + to;
    }
}
