using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public SpriteRenderer sprite;
    Sprite frontSprite, backSprite;
    public int value;
    public Action<Card> OnClick;
    bool faceUp;

    public void Init(Sprite frontTex, Sprite backTex, int value)
    {
        frontSprite = frontTex;
        backSprite = backTex;
        sprite.sprite = backSprite;
        this.value = value;
        faceUp = false;
    }

    public void Flip(bool faceUp)
    {
        if (faceUp != this.faceUp) Flip();
    }

    public void Flip()
    {
        StartCoroutine(FlipAnim(0.2f));
    }

    public void OnMouseDown()
    {
        if (OnClick != null) OnClick.Invoke(this);
    }

    IEnumerator FlipAnim(float duration)
    {
        Quaternion original = transform.rotation;
        Quaternion target = Quaternion.Euler(original.eulerAngles + (Vector3.up * 90));

        // Rotate to invisible
        float startTime = Time.time;
        while (Time.time - startTime < duration/2)
        {
            transform.rotation = Quaternion.Lerp(original, target, (Time.time - startTime) / (duration/2));
            yield return null;
        }

        // Update sprite
        faceUp = !faceUp;
        sprite.sprite = faceUp ? frontSprite : backSprite;

        // Rotate back to flat
        startTime = Time.time;
        while (Time.time - startTime < duration / 2)
        {
            transform.rotation = Quaternion.Lerp(target, original, (Time.time - startTime) / (duration / 2));
            yield return null;
        }

        transform.rotation = original;
    }
}
