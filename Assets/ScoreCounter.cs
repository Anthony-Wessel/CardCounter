using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    public int value;

    private void Start()
    {
        value = 10;
    }

    public void PlayCard(Card c)
    {
        value += c.value;
        updateScore();
    }

    void updateScore()
    {
        GetComponent<TextMeshProUGUI>().text = value.ToString();
    }
}
