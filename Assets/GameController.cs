using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public List<Card> cards;
    protected virtual void Win()
    {
        FindObjectOfType<EndPanel>().Show(true);
    }
    protected virtual void Lose()
    {
        FindObjectOfType<EndPanel>().Show(false);
    }

    public virtual void AddCard(Card card)
    {
        cards.Add(card);
    }
}
