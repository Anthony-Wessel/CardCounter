using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public SpriteRenderer sprite, backSprite;
    public int value;

    public void Init(Sprite texture, Sprite backTexture, int value)
    {
        sprite.sprite = texture;
        backSprite.sprite = backTexture;
        this.value = value;
    }

    public void Flip()
    {
        sprite.enabled = !sprite.enabled;
        backSprite.enabled = !backSprite.enabled;
    }

    public void Play()
    {
        GameObject.FindGameObjectWithTag("ScoreCounter").GetComponent<ScoreCounter>().PlayCard(this);
    }
}
