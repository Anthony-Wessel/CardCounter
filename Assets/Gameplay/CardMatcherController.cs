using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMatcherController : GameController
{
    public Deck deck;
    public GameObject CardPrefab;
    public int numPairs;

    Card selectedCard;
    bool paused;
    int remainingCardCount;

    void Awake()
    {
        paused = false;
    }

    public override void AddCard(Card card)
    {
        cards.Add(card);
        card.OnClick = Play;
        remainingCardCount++;
    }

    void Start()
    {
        List<int> indices = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
        indices.Shuffle();

        for (int i = 0; i < numPairs; i++)
        {
            int index = Random.Range(0, 4) * indices[i];

            Card cardA = Instantiate(CardPrefab, transform).GetComponent<Card>();
            Card cardB = Instantiate(CardPrefab, transform).GetComponent<Card>();
            cardA.Init(deck.cards[index], deck.cardBack, i);
            cardB.Init(deck.cards[index], deck.cardBack, i);

            AddCard(cardA);
            AddCard(cardB);
        }

        cards.Shuffle();
        FindObjectOfType<CardPlacer>().PlaceCards(cards);
    }

    public void Play(Card card)
    {
        if (paused) return;
        if (selectedCard == card) return;

        if (selectedCard == null)
        {
            selectedCard = card;
            card.Flip();
        }
        else
        {
            card.Flip();
            StartCoroutine(checkCards(card));
        }
        
    }

    IEnumerator checkCards(Card card)
    {
        paused = true;
        bool correct = card.value == selectedCard.value;

        if (correct)
        {
            GameObject selectedCardObj = selectedCard.gameObject;
            GameObject cardObj = card.gameObject;
            Destroy(card.GetComponent<BoxCollider2D>());
            Destroy(selectedCard.GetComponent<BoxCollider2D>());

            paused = false;
            selectedCard = null;
            remainingCardCount -= 2;
            if (remainingCardCount == 0) Win();

            // TODO: destroy colliders?

            yield return new WaitForSeconds(0.5f);

            Destroy(cardObj);
            Destroy(selectedCardObj);
        }
        else
        {
            yield return new WaitForSeconds(0.5f);

            card.Flip();
            selectedCard.Flip();

            selectedCard = null;
            paused = false;
        }
    }
}
