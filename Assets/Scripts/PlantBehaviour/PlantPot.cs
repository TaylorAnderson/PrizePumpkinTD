using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantPot : MonoBehaviour
{
    public List<GameObject> plantList;
    public int growthIndex = 0;
    public float progress = 0;
    public float progressModifier = 1;
    public float progressModEffectTime = 3;
    private float progressModEffectTimer = 0;
    private bool placed = false;
    public ProgressBar progressBar;
    public SpriteRenderer shadowSprite;
    public float rateOfGrowth = 0.05f;
    public GameObject waterEffect;
    public GameObject iceEffect;
    // Start is called before the first frame update
    void Start() {
        SetPlant();
    }

    // Update is called once per frame
    void Update() {
        if (placed) {
            progress += rateOfGrowth * progressModifier * Time.deltaTime;
            if (progress >= 1) {
                growthIndex += 1;
                if (growthIndex > 3) {
                    growthIndex = 0;
                }
                SfxManager.instance.PlaySound(SoundType.PLANT_GROW);
                SetPlant();
                progress = 0;
            }
        }

        progressBar.SetProgress(progress);
        progressModEffectTimer -= Time.deltaTime;
        if (progressModEffectTimer < 0) {
            waterEffect.SetActive(false);
            iceEffect.SetActive(false);
            progressModifier = 1;
        }
    }

    void SetPlant() {
        
        foreach (GameObject p in plantList) {
            p.SetActive(false);
        }
        if (growthIndex > 0) {
            plantList[growthIndex-1].SetActive(true);
            plantList[growthIndex-1].GetComponent<PlantBehaviour>().OnActivated();
        }
    }

    public void OnPlaced() {
        placed = true;
        foreach (GameObject p in plantList) {
            p.GetComponent<PlantBehaviour>().OnPlaced();
        }
    }

    public void AccelerateGrowth() {
        waterEffect.SetActive(true);
        progressModifier = 3f;
        progressModEffectTimer = progressModEffectTime;
    }

    public void FreezeGrowth() {
        iceEffect.SetActive(true);
        progressModifier = 0.1f;
        progressModEffectTimer = progressModEffectTime;
    }
}
