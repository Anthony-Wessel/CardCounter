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
        if (selectedCard == card) return;

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
        bool correct = card.value == selectedCard.value;

        if (correct)
        {
            GameObject selectedCardObj = selectedCard.gameObject;
            GameObject cardObj = card.gameObject;
            Destroy(card);
            Destroy(selectedCard);

            paused = false;
            selectedCard = null;
            cardCount -= 2;
            if (cardCount == 0) Win();

            // TODO: destroy colliders?

            yield return new WaitForSeconds(0.5f);

            Destroy(cardObj);
            Destroy(selectedCardObj);
        }
        else
        {
            yield return new WaitForSeconds(0.5f);

            card.Flip();
            selectedCard.Flip();

            selectedCard = null;
            paused = false;
        }
    }
}
