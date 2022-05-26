using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardCounterController : GameController
{
    public int value;

    public int min, max;
    int decksRemaining;

    public TextMeshProUGUI scoreText;

    protected override void ClearBoard()
    {
        CardPile[] piles = FindObjectsOfType<CardPile>();
        foreach (CardPile pile in piles)
        {
            pile.Clear();
        }

        value = 10;
        updateScoreText();

        base.ClearBoard();
    }

    protected override void LoadStage(int stage)
    {
        value = 10;

        
        for (int i = 0; i < 52; i++)
        {
            Card newCard = Instantiate(CardPrefab, transform).GetComponent<Card>();
            newCard.Init(Deck.cards[i], Deck.cardBack, (i < Deck.cards.Length / 2 ? -1 : 1) * ((i % 13) + 1));
            cards.Add(newCard);
        }

        for (int i = 0; i < 13; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                int swapIndex = Random.Range(j, 4);
                cards.swap(j * i, swapIndex * i);
            }
        }

        StartCoroutine(PlaceCards());

        decksRemaining = 4;
    }

    IEnumerator PlaceCards()
    {
        CardPile[] piles = FindObjectsOfType<CardPile>();
        for (int i = 0; i < piles.Length; i++)
        {
            piles[i].AddCards(cards.GetRange(i * 13, 13));
            yield return new WaitForSeconds(0.15f);
        }

        yield return new WaitForSeconds(1f);

        StartGame();
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
        scoreText.text = value.ToString();
    }

    public void AddDeck()
    {
        decksRemaining++;
    }
    public void RemoveDeck()
    {
        if (--decksRemaining == 0)
        {
            running = false;
            Win();
        }
    }
}
