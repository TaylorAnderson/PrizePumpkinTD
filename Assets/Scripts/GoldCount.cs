using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCount : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnGoldChanged() {
        var txt = GetComponent<SuperTextMesh>();
        txt.text = $"${GameManager.instance.gold.ToString()}";
    }
}
