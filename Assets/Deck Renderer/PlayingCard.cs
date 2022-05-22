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

    public void Init(Suit suit, Texture2D number, Texture2D background, bool useCourt, int value)
    {
        this.background.texture = background;
        foreach (RawImage img in pips)
        {
            img.texture = suit.pip;
            img.color = suit.color;
        }

        if (useCourt)
        {
            if (value == 1 || value > 10)
            {
                pipHolder.SetActive(false);
                customTexture.gameObject.SetActive(true);

                switch(value)
                {
                    case 1:
                        customTexture.texture = suit.AceTexture;
                        break;
                    case 11:
                        customTexture.texture = suit.JackTexture;
                        break;
                    case 12:
                        customTexture.texture = suit.QueenTexture;
                        break;
                    case 13:
                        customTexture.texture = suit.KingTexture;
                        break;
                }
            }
        }
            

        foreach (RawImage img in numbers)
        {
            img.texture = number;
            img.color = suit.color;
        }
    }
}
