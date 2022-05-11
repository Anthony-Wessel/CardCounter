using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpawner : MonoBehaviour
{
    public int rows, columns;
    public bool flip;

    public GameObject CardPrefab;
    public Deck deck;

    List<Card> cards;


    void Start()
    {
        CardSorterController controller = FindObjectOfType<CardSorterController>();
        cards = new List<Card>();
        for (int i = 0; i < rows*columns; i++)
        {
            Card newCard = Instantiate(CardPrefab, transform).GetComponent<Card>();
            int suitIndex = Random.Range(0, 4);
            newCard.Init(deck.cards[suitIndex * 13 + i], deck.cardBack, i+1);

            if (flip) newCard.Flip();
            
            controller.AddCard(newCard);
            cards.Add(newCard);
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
