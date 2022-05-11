using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMatcherController : GameController
{
    Card selectedCard;
    bool paused;
    int cardCount;

    void Awake()
    {
        paused = false;
    }

    public override void AddCard(Card card)
    {
        cards.Add(card);
        card.OnClick = Play;
        cardCount++;
    }

    public void Play(Card card)
    {
        if (paused) return;

        if (selectedCard == null)
        {
            selectedCard = card;
            card.Flip();
        }
        else
        {
            card.Flip();
            StartCoroutine(checkCards(card));
        }
        
    }

    IEnumerator checkCards(Card card)
    {
        paused = true;

        yield return new WaitForSeconds(1f);
        
        if (card.value == selectedCard.value)
        {
            Destroy(card.gameObject);
            Destroy(selectedCard.gameObject);
            cardCount -= 2;
            if (cardCount == 0) Win();
        }
        else
        {
            card.Flip();
            selectedCard.Flip();
        }

        selectedCard = null;
        paused = false;
    }
}
