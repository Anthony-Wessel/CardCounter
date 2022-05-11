using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMatcherSpawner : MonoBehaviour
{
    public int rows, columns;

    public GameObject CardPrefab;
    public Deck deck;

    void Start()
    {
        CardMatcherController controller = FindObjectOfType<CardMatcherController>();
        List<Card> cards = new List<Card>();
        int numPairs = (rows * columns) / 2;
        List<int> usedIndices = new List<int>();
        for (int i = 0; i < numPairs; i++)
        {
            int index = Random.Range(0, deck.cards.Length);
            while (usedIndices.Contains(index))
            {
                index = Random.Range(0, deck.cards.Length);
            }
            usedIndices.Add(index);

            Card cardA = Instantiate(CardPrefab, transform).GetComponent<Card>();
            Card cardB = Instantiate(CardPrefab, transform).GetComponent<Card>();
            cardA.Init(deck.cards[index], deck.cardBack, i);
            cardB.Init(deck.cards[index], deck.cardBack, i);
            cards.Add(cardA);
            cards.Add(cardB);

            controller.AddCard(cardA);
            controller.AddCard(cardB);
        }

        Vector3 offset = new Vector3(5.5f * (columns - 1), 7.5f * (rows - 1));
        offset = -offset / 2;

        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                int cardIndex = Random.Range(0, cards.Count);
                cards[cardIndex].transform.position = new Vector3(5.5f * x, 7.5f * y) + offset;
                cards.RemoveAt(cardIndex);
            }
        }

    }
}
