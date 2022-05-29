using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="PlayingCards/Suit")]
public class Suit : ScriptableObject
{
    public Texture2D pip;
    public Color color;

    [Header("Override Textures")]
    public Texture2D AceTexture;
    public Texture2D JackTexture;
    public Texture2D QueenTexture;
    public Texture2D KingTexture;
}
