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

    public void OnMouseDown()
    {
        if (OnClick != null) OnClick.Invoke(this);
    }

    #region Flip

    public void Flip(bool faceUp)
    {
        if (faceUp != this.faceUp) Flip();
    }

    public void Flip()
    {
        StartCoroutine(FlipAnim(0.2f));
    }

    IEnumerator FlipAnim(float duration)
    {
        Quaternion original = transform.rotation;
        Quaternion target = Quaternion.Euler(original.eulerAngles + (Vector3.up * 90));

        // Rotate to invisible
        float startTime = Time.time;
        while (Time.time - startTime < duration / 2)
        {
            transform.rotation = Quaternion.Lerp(original, target, (Time.time - startTime) / (duration / 2));
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

    #endregion

    #region Fade

    public void FadeOut()
    {
        StartCoroutine(FadeAnim(0.25f));
    }

    IEnumerator FadeAnim(float duration)
    {
        SpriteRenderer sr = GetComponentInChildren<SpriteRenderer>();

        float startTime = Time.time;
        while(Time.time - startTime < duration)
        {
            Color c = sr.color;
            c.a = 1 - (Time.time - startTime) / duration;
            sr.color = c;

            yield return null;
        }

        Destroy(gameObject);
    }

    #endregion

    #region Move

    public void Move(Vector3 position)
    {
        StartCoroutine(MoveAnim(position, 0.25f));
    }

    IEnumerator MoveAnim(Vector3 targetPosition, float duration)
    {
        Vector3 originalPosition = transform.position;
        float startTime = Time.time;

        while (Time.time - startTime < duration)
        {
            float progress = (Time.time - startTime) / duration;
            transform.position = Vector3.Lerp(originalPosition, targetPosition, progress);

            yield return null;
        }

        transform.position = targetPosition;
    }

    #endregion

    #region Shake

    public void Shake()
    {
        StartCoroutine(ShakeAnim(0.25f));
    }

    IEnumerator ShakeAnim(float duration)
    {
        Transform spriteTransform = transform.GetChild(0);
        float startTime = Time.time;

        float period = 3*Mathf.PI;
        float amplitude = 10;

        while (Time.time - startTime < duration)
        {
            float progress = (Time.time - startTime)/duration;
            float value = amplitude * Mathf.Sin((period * progress));

            spriteTransform.localRotation = Quaternion.Euler(0, 0, value);

            yield return null;
        }

        spriteTransform.localRotation = Quaternion.identity;
    }

    #endregion
}
