using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPlacer : MonoBehaviour
{
    public void PlaceCards(List<Card> cards)
    {
        int rows = Mathf.CeilToInt(Mathf.Sqrt(cards.Count));
        int columns = Mathf.CeilToInt((float)cards.Count / rows);

        Vector3 offset = new Vector3(5.5f * (columns - 1), 7.5f * (rows - 1));
        offset = -offset / 2;
        
        int cardIndex = 0;

        for (int x = 0; x < columns; x++)
        {
            for (int y = rows-1; y >= 0; y--)
            {
                if (cardIndex >= cards.Count) break;
                cards[cardIndex++].transform.position = new Vector3(5.5f * x, 7.5f * y) + offset;
            }
        }
    }
}
