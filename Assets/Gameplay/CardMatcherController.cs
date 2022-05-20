using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMatcherController : GameController
{
    public Deck deck;
    public GameObject CardPrefab;
    public int initialNumPairs;

    Card selectedCard;
    bool paused;
    int remainingCardCount;

    void Awake()
    {
        paused = false;
    }

    protected override void ClearBoard()
    {
        selectedCard = null;
        paused = false;
        remainingCardCount = 0;

        base.ClearBoard();
    }

    public override void AddCard(Card card)
    {
        cards.Add(card);
        card.OnClick = Play;
        remainingCardCount++;
    }

    protected override void LoadStage(int stage)
    {
        List<int> indices = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
        indices.Shuffle();

        for (int i = 0; i < initialNumPairs + stage; i++)
        {
            int index = (Random.Range(0, 4) * 13) + indices[i];

            Card cardA = Instantiate(CardPrefab, transform).GetComponent<Card>();
            Card cardB = Instantiate(CardPrefab, transform).GetComponent<Card>();
            cardA.Init(deck.cards[index], deck.cardBack, i);
            cardB.Init(deck.cards[index], deck.cardBack, i);

            AddCard(cardA);
            AddCard(cardB);
        }

        cards.Shuffle();
        FindObjectOfType<CardPlacer>().PlaceCards(cards, this);
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
            Card selectedCardObj = selectedCard;
            Card cardObj = card;
            Destroy(card.GetComponent<BoxCollider2D>());
            Destroy(selectedCard.GetComponent<BoxCollider2D>());

            cards.Remove(card);
            cards.Remove(selectedCard);

            paused = false;
            selectedCard = null;
            remainingCardCount -= 2;
            if (remainingCardCount == 0) Win();

            yield return new WaitForSeconds(0.5f);

            cardObj.FadeOut();
            selectedCardObj.FadeOut();
        }
        else
        {
            card.Shake();
            selectedCard.Shake();
            yield return new WaitForSeconds(1f);

            card.Flip();
            selectedCard.Flip();

            selectedCard = null;
            paused = false;
        }
    }
}
