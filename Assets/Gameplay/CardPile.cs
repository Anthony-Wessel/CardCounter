using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CardPile : MonoBehaviour
{
    public Deck deck;
    public GameObject cardPrefab;

    List<Card> cards;

    ScoreCounter score;

    void Start()
    {
        score = GameObject.FindGameObjectWithTag("ScoreCounter").GetComponent<ScoreCounter>();
        score.AddDeck();
        cards = new List<Card>();

        for (int i = 0; i < deck.cards.Length; i++)
        {
            Card newCard = Instantiate(cardPrefab, transform).GetComponent<Card>();
            newCard.Init(deck.cards[i], deck.cardBack, (i < deck.cards.Length/2 ? -1 : 1) * ((i % 13) + 1));
            cards.Add(newCard);
        }

        Shuffle();

        cards[0].Flip();
    }

    void Shuffle()
    {
        void swap(int x, int y)
        {
            Card c = cards[x];
            cards[x] = cards[y];
            cards[y] = c;
        }

        for (int i = 0; i < cards.Count; i++)
        {
            int random = Random.Range(i, cards.Count);
            swap(i, random);

            cards[i].GetComponent<SortingGroup>().sortingOrder = -i;
        }
    }

    public void OnMouseDown()
    {
        if (cards.Count == 0) return;

        Card card = cards[0];
        cards.RemoveAt(0);
        score.PlayCard(card);
        Destroy(card.gameObject);

        if (cards.Count > 0)
            cards[0].Flip();
        else
            score.RemoveDeck();
    }
}
