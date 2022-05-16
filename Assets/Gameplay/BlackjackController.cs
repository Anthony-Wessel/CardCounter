using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackjackController : GameController
{
    public GameObject cardPrefab;
    public Deck deck;

    public Hand playerHand, dealerHand;
    bool dealerTurn;

    BlackjackState state;
    enum BlackjackState
    {
        Normal,
        PlayerStand,
        DealerStand,
        Over
    }

    void Start()
    {
        for (int i = 0; i < 52; i++)
        {
            Card newCard = Instantiate(cardPrefab, transform).GetComponent<Card>();
            int cardValue = Mathf.Min((i % 13) + 1, 10);
            if (cardValue == 1) cardValue = 11;

            newCard.Init(deck.cards[i], deck.cardBack, cardValue);
            cards.Add(newCard);
        }
        state = BlackjackState.Normal;

        cards.Shuffle();

        dealCard(playerHand);
        dealCard(dealerHand);
    }

    private void Update()
    {
        if (state == BlackjackState.Over) return;

        if (state != BlackjackState.PlayerStand)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                dealCard(playerHand);
                dealerTurn = true;
            }
            else if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                if (state == BlackjackState.DealerStand)
                    CalculateEnd();
                else
                    state = BlackjackState.PlayerStand;
            }
        }

        if (state == BlackjackState.Over) return;

        if (state != BlackjackState.DealerStand)
        {
            if (dealerTurn || state == BlackjackState.PlayerStand)
            {
                if (dealerHand.Score < playerHand.Score || dealerHand.Score < 15)
                    dealCard(dealerHand);
                else
                {
                    if (state == BlackjackState.PlayerStand)
                        CalculateEnd();
                    else
                        state = BlackjackState.DealerStand;
                }

                dealerTurn = false;
            }
        }
    }

    void dealCard(Hand hand)
    {
        if (hand.AddCard(cards[0]))
        {
            state = BlackjackState.Over;
            if (hand == dealerHand) Win();
            else Lose();
        }
        cards.RemoveAt(0);
    }

    void CalculateEnd()
    {
        state = BlackjackState.Over;
        if (playerHand.Score > dealerHand.Score) Win();
        else if (playerHand.Score < dealerHand.Score) Lose();
        else Draw();
    }
}
