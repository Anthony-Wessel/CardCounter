using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [HideInInspector]
    public List<Card> cards;

    public int currentStage;
    public int maxStage;

    void Start()
    {
        LoadStage(0);
    }

    protected virtual void LoadStage(int stage)
    {

    }

    void loadNextStage()
    {
        LoadStage(++currentStage);
    }

    protected virtual void Win()
    {
        if (currentStage < maxStage)
            Invoke("loadNextStage", 1f);
        else
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
