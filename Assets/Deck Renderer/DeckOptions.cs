using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="PlayingCards/DeckOptions")]
public class DeckOptions : ScriptableObject
{
    public Suit[] suits;
    public GameObject[] cardTemplates;
    public Texture2D cardTexture;
    public Texture2D cardBackTexture;
    public NumberTextures numbers;

    public bool useCourt;

}
