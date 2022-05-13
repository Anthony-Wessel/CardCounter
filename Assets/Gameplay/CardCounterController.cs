using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardCounterController : GameController
{
    public int value;

    public int min, max;
    int decksRemaining;

    public Deck deck;
    public GameObject cardPrefab;

    private void Start()
    {
        value = 10;

        CardPile[] piles = FindObjectsOfType<CardPile>();
        for (int i = 0; i < 52; i++)
        {
            Card newCard = Instantiate(cardPrefab).GetComponent<Card>();
            newCard.Init(deck.cards[i], deck.cardBack, (i < deck.cards.Length / 2 ? -1 : 1) * ((i % 13) + 1));
            cards.Add(newCard);
        }

        for (int i = 0; i < 13; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                int swapIndex = Random.Range(j, 4);
                swap(j * i, swapIndex * i);
            }
        }

        for (int i = 0; i < piles.Length; i++)
        {
            piles[i].AddCards(cards.GetRange(i * 13, 13));
        }

        decksRemaining = piles.Length;
    }

    void swap(int x, int y)
    {
        Card c = cards[x];
        cards[x] = cards[y];
        cards[y] = c;
    }

    public void PlayCard(Card c)
    {
        value += c.value;
        updateScoreText();
        if (value < min || value > max)
        {
            Lose();
        }
    }

    void updateScoreText()
    {
        GetComponent<TextMeshProUGUI>().text = value.ToString();
    }

    public void AddDeck()
    {
        decksRemaining++;
    }
    public void RemoveDeck()
    {
        if (--decksRemaining == 0)
            Lose();
    }
}
