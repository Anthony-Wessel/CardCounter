using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CardPile : MonoBehaviour
{
    List<Card> cards;
    CardCounterController score;

    void Awake()
    {
        score = GameObject.FindGameObjectWithTag("ScoreCounter").GetComponent<CardCounterController>();
    }

    public void AddCards(List<Card> cardsToAdd)
    {
        cards = cardsToAdd;

        cards.Shuffle();

        for (int i = 0; i < cards.Count; i++)
        {
            Card c = cards[i];

            c.OnClick = PlayCard;
            c.transform.position = transform.position;
            c.transform.parent = transform;
            c.GetComponent<SortingGroup>().sortingOrder = -i;
        }

        cards[0].Flip();
    }

    public void PlayCard(Card playedCard)
    {
        if (cards.Count == 0) return;

        Card card = cards[0];
        cards.RemoveAt(0);
        score.PlayCard(card);

        card.Move(score.transform.position);
        card.FadeOut();

        if (cards.Count > 0)
            cards[0].Flip();
        else
            score.RemoveDeck();
    }
}
