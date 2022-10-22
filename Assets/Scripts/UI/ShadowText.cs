using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowText : MonoBehaviour
{
    public SuperTextMesh front;
    public SuperTextMesh back;

    public void SetText(string text) {
        front.text = text;
        back.text = text;
    }
}
