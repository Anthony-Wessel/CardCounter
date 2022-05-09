using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    public int value;

    public int min, max;
    int decksRemaining;

    public EndPanel endPanel;

    private void Start()
    {
        value = 10;
    }

    public void PlayCard(Card c)
    {
        value += c.value;
        updateScoreText();
        if (value < min || value > max)
        {
            endPanel.Show(false);
        }
    }

    void updateScoreText()
    {
        GetComponent<TextMeshProUGUI>().text = value.ToString();
    }

    public void AddDeck()
    {
        decksRemaining++;
    }
    public void RemoveDeck()
    {
        if (--decksRemaining == 0)
            endPanel.Show(true);
    }
}
