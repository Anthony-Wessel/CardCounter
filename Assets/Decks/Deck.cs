using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="PlayingCards/Deck")]
public class Deck : ScriptableObject
{
    public Sprite[] cards;
    public Sprite cardBack;
}
