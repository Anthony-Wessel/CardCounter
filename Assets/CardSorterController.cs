using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSorterController : GameController
{
    void Awake()
    {
        cards = new List<Card>();
    }

    public override void AddCard(Card card)
    {
        card.OnClick = Play;
        for (int i = 0; i < cards.Count; i++)
        {
            if (card.value < cards[i].value)
            {
                cards.Insert(i, card);
                return;
            }
        }
        cards.Add(card);
    }

    public void Play(Card card)
    {
        if (card == cards[0])
        {
            cards.RemoveAt(0);
            Destroy(card.gameObject);
        }

        if (cards.Count == 0) Win();
    }
}
