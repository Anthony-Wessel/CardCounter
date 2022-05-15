using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSorterController : GameController
{
    public Deck deck;
    public GameObject CardPrefab;
    public int cardCount;

    void Awake()
    {
        cards = new List<Card>();
    }

    void Start()
    {
        List<int> indices = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
        indices.Shuffle();

        for (int i = 0; i < cardCount; i++)
        {
            Card newCard = Instantiate(CardPrefab, transform).GetComponent<Card>();
            int suitIndex = Random.Range(0, 4);
            newCard.Init(deck.cards[suitIndex * 13 + indices[i]], deck.cardBack, indices[i]);

            newCard.Flip();

            AddCard(newCard);
        }

        FindObjectOfType<CardPlacer>().PlaceCards(cards);

        SortCards(cards);
    }

    public override void AddCard(Card card)
    {
        card.OnClick = Play;
        cards.Add(card);
    }

    public void Play(Card card)
    {
        if (card == cards[0])
        {
            cards.RemoveAt(0);
            card.FadeOut();
        }
        else
        {
            card.Shake();
        }

        if (cards.Count == 0) Win();
    }

    void SortCards(List<Card> cards)
    {
        cards.Sort((x,y) => { return x.value.CompareTo(y.value); });
    }
}
