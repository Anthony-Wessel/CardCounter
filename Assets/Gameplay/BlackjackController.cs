using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackjackController : GameController
{
    public Hand playerHand, dealerHand;
    bool playerTurn;

    BlackjackState state;
    enum BlackjackState
    {
        Normal,
        PlayerStand,
        DealerStand,
        Over
    }

    protected override void ClearBoard()
    {
        playerHand.Clear();
        dealerHand.Clear();

        base.ClearBoard();
    }

    protected override void LoadStage(int stage)
    {
        for (int i = 0; i < 52; i++)
        {
            Card newCard = Instantiate(CardPrefab, transform).GetComponent<Card>();
            int cardValue = Mathf.Min((i % 13) + 1, 10);
            if (cardValue == 1) cardValue = 11;

            newCard.Init(Deck.cards[i], Deck.cardBack, cardValue);
            cards.Add(newCard);
        }
        state = BlackjackState.Normal;

        cards.Shuffle();

        bool dealerStart = Random.Range(0, 2) == 1;
        if (dealerStart)
        {
            dealCard(dealerHand);
            dealCard(playerHand);

            Invoke("dealerTurn", 0.5f);
        }
        else
        {
            dealCard(playerHand);
            dealCard(dealerHand);

            playerTurn = true;
        }
    }

    public void Hit()
    {
        if (!playerTurn) return;

        dealCard(playerHand);
        playerTurn = false;
        if (state == BlackjackState.DealerStand)
        {
            if (playerHand.Score > dealerHand.Score) End();
            playerTurn = true;
        }
        else Invoke("dealerTurn", 0.5f);
    }
    public void Stand()
    {
        if (!playerTurn) return;

        if (state == BlackjackState.DealerStand)
        {
            End();
            return;
        }
        else
            state = BlackjackState.PlayerStand;

        Invoke("dealerTurn", 0.5f);
    }

    void dealerTurn()
    {
        playerTurn = true;

        if (state == BlackjackState.DealerStand || state == BlackjackState.Over) return;

        if (dealerHand.Score < playerHand.Score || dealerHand.Score < 15)
            dealCard(dealerHand);
        else
        {
            if (state == BlackjackState.PlayerStand)
                End();
            else
                state = BlackjackState.DealerStand;
        }

        if (state == BlackjackState.PlayerStand) dealerTurn();
    }

    void dealCard(Hand hand)
    {
        if (state == BlackjackState.Over) return;

        if (hand.AddCard(cards[0]))
        {
            End();
        }
        cards.RemoveAt(0);
    }

    void End()
    {
        state = BlackjackState.Over;

        Invoke("CalculateEnd", 0.5f);
    }

    void CalculateEnd()
    {
        if (playerHand.Score > 21) Lose();
        else if (dealerHand.Score > 21) Win();
        else if (playerHand.Score > dealerHand.Score) Win();
        else if (playerHand.Score < dealerHand.Score) Lose();
        else Draw();
    }
}
