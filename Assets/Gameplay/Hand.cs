using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    List<Card> heldCards;
    public int Score { get { return score; } }
    int score;
    public bool dealer;
    GameController controller;

    public void Clear()
    {
        score = 0;
        heldCards.Clear();
    }

    private void Awake()
    {
        heldCards = new List<Card>();
        score = 0;
        controller = GetComponentInParent<GameController>();
    }

    public bool AddCard(Card c)
    {
        heldCards.Add(c);
        
        if (dealer) c.transform.localRotation = Quaternion.Euler(0, 0, 180);
        c.transform.parent = transform;
        c.Flip();

        UpdateCardPositions();

        score += c.value;
        return score > 21;
    }

    void UpdateCardPositions()
    {
        float offset = (heldCards.Count / 2f) - 0.5f;
        for (int i = 0; i < heldCards.Count; i++)
        {
            float pos = i - offset;
            if (dealer) pos *= -1;

            Vector3 localPosition = new Vector3(pos * controller.CardSize.x * 1.1f, 0);
            heldCards[i].Move(transform.position + localPosition);
        }
    }
}
