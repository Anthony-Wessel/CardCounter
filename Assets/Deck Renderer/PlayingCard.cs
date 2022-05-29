using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayingCard : MonoBehaviour
{
    [Header("References")]
    [SerializeField] RawImage background;
    [SerializeField] RawImage[] pips;
    [SerializeField] RawImage[] numbers;
    [SerializeField] RawImage customTexture;
    [SerializeField] GameObject pipHolder;

    public void Init(Suit suit, Texture2D number, Texture2D background, Texture2D courtTex)
    {
        this.background.texture = background;
        foreach (RawImage img in pips)
        {
            img.texture = suit.pip;
            img.color = suit.color;
        }

        if (courtTex != null)
        {
            pipHolder.SetActive(false);
            customTexture.gameObject.SetActive(true);
            customTexture.texture = courtTex;
        }
            

        foreach (RawImage img in numbers)
        {
            img.texture = number;
            img.color = suit.color;
        }
    }
}
