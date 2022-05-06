using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="PlayingCards/Numbers")]
public class NumberTextures : ScriptableObject
{
    public Texture2D[] textures;

    public Texture2D GetTexture(int value, bool courtCards)
    {
        if (courtCards)
        {
            switch(value)
            {
                case 1:
                    return textures[0];
                case 11:
                    return textures[14];
                case 12:
                    return textures[15];
                case 13:
                    return textures[16];
                default:
                    return textures[value];
            }
        }

        return textures[value];
    }
}
