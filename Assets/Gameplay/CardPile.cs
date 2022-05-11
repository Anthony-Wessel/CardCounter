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

        BalancedShuffle();

        cards[0].Flip();
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

    void BalancedShuffle()
    {
        Shuffle(0, cards.Count / 2);
        Shuffle(cards.Count / 2, cards.Count);

        for (int i = 0; i < cards.Count/2; i++)
        {
            if (i % 2 == 0) swap(i, cards.Count / 2 + i);
        }

        for (int i = 1; i < cards.Count; i++)
        {
            if (Random.Range(0f, 1f) < 0.25f) swap(i - 1, i);
            cards[i - 1].GetComponent<SortingGroup>().sortingOrder = -(i - 1);
        }
        cards[cards.Count - 1].GetComponent<SortingGroup>().sortingOrder = -(cards.Count - 1);
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
