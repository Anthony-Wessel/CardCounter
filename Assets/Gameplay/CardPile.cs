using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CardPile : MonoBehaviour
{
    List<Card> cards;
    CardCounterController controller;

    void Awake()
    {
        controller = GetComponentInParent<CardCounterController>();
    }

    public void Clear()
    {
        if (cards != null)
            cards.Clear();
    }

    public void AddCards(List<Card> cardsToAdd)
    {
        cards = cardsToAdd;
        cards.Shuffle();

        StartCoroutine(MoveCards());
    }

    IEnumerator MoveCards()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            Card c = cards[i];

            c.OnClick = PlayCard;
            c.Move(transform.position);
            c.transform.parent = transform;
            c.GetComponent<SortingGroup>().sortingOrder = -i;

            yield return new WaitForSeconds(0.05f);
        }

        cards[0].Flip();
    }

    public void PlayCard(Card playedCard)
    {
        if (!controller.running) return;
        if (cards.Count == 0) return;

        Card card = cards[0];
        cards.RemoveAt(0);
        controller.PlayCard(card);

        card.Move(controller.transform.position);
        card.FadeOut();

        if (cards.Count > 0)
            cards[0].Flip();
        else
            controller.RemoveDeck();
    }
}
