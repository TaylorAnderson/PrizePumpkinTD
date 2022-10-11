using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SpriteMask))]
public class RenderTextureMask : MonoBehaviour {

    private SpriteMask spriteMask;
    public RenderTexture renderTex;
    public Camera renderCamera;
    // Start is called before the first frame update
    void Start() {
      print("hello");
      spriteMask = GetComponent<SpriteMask>();

      Texture2D texture = new Texture2D(renderTex.width, renderTex.height, TextureFormat.RGBA32, false);
      texture = ToTexture2D(renderTex);
      Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 16);
      spriteMask.sprite = sprite; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Texture2D ToTexture2D(RenderTexture rTex) {
        // Remember currently active render texture
        RenderTexture currentActiveRT = RenderTexture.active;
        // Set the supplied RenderTexture as the active one
        RenderTexture.active = rTex;

        renderCamera.Render();
        // Create a new Texture2D and read the RenderTexture image into it
        Texture2D tex = new Texture2D(rTex.width, rTex.height);
        tex.ReadPixels(new Rect(0, 0, tex.width, tex.height), 0, 0);
        tex.Apply();
        // Restorie previously active render texture
        RenderTexture.active = currentActiveRT;
        
        return tex;
    }
}
