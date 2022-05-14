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

        Shuffle(0, 52);
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
            else if (Input.GetKeyDown(KeyCode.End))
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
            print(hand.name + " busted");
            state = BlackjackState.Over;
            FindObjectOfType<EndPanel>().Show(hand == dealerHand);
        }
        cards.RemoveAt(0);
    }

    void CalculateEnd()
    {
        state = BlackjackState.Over;
        FindObjectOfType<EndPanel>().Show(playerHand.Score > dealerHand.Score);
        // TODO: Account for draws
    }

    void swap(int x, int y)
    {
        Card c = cards[x];
        cards[x] = cards[y];
        cards[y] = c;
    }

    void Shuffle(int start, int endExclusive)
    {
        for (int i = start; i < endExclusive; i++)
        {
            int random = Random.Range(i, endExclusive);
            swap(i, random);
        }
    }
}
