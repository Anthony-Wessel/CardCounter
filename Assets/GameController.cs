using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public List<Card> cards;
    protected virtual void Win()
    {

    }
    protected virtual void Lose()
    {

    }

    public virtual void AddCard(Card card)
    {
        cards.Add(card);
    }
}
