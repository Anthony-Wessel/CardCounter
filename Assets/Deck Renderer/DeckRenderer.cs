using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DeckRenderer : MonoBehaviour
{
    public RenderTexture deckRenderTexture;

    Camera cam { get { return GetComponent<Camera>(); } }

    public void ResizeToFit(Vector2Int deckSize)
    {
        if (deckRenderTexture != null) DestroyImmediate(deckRenderTexture);
        deckRenderTexture = new RenderTexture((deckSize.x) * 500, (deckSize.y) * 700, 8);
        cam.targetTexture = deckRenderTexture;
    }

    public void RenderToTexture()
    {
        cam.Render();
        Texture2D tex = new Texture2D(deckRenderTexture.width, deckRenderTexture.height);

        RenderTexture activeRT = RenderTexture.active;
        RenderTexture.active = deckRenderTexture;
        tex.ReadPixels(new Rect(0, 0, deckRenderTexture.width, deckRenderTexture.height), 0, 0);
        RenderTexture.active = activeRT;

        Directory.CreateDirectory(Application.persistentDataPath + "/DeckRenders");
        File.WriteAllBytes(Application.persistentDataPath + "/DeckRenders/render.png", tex.EncodeToPNG());

    }
}
