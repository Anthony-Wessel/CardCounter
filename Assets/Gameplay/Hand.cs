using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    List<Card> heldCards;
    public int Score { get { return score; } }
    int score;
    public bool dealer;

    private void Awake()
    {
        heldCards = new List<Card>();
        score = 0;
    }

    public bool AddCard(Card c)
    {
        heldCards.Add(c);
        
        if (dealer) c.transform.rotation = Quaternion.Euler(0, 0, 180);
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

            Vector3 localPosition = new Vector3(pos * 5.5f, 0);
            heldCards[i].Move(transform.position + localPosition);
        }
    }
}
