using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPlacer : MonoBehaviour
{
    public Transform startPosition;

    public void PlaceCards(List<Card> cards, GameController controller)
    {
        if (startPosition != null)
        {
            foreach (Card card in cards)
            {
                card.transform.position = startPosition.position;
            }
        }

        StartCoroutine(PlaceCardsAnimated(cards, controller));
    }

    IEnumerator PlaceCardsAnimated(List<Card> cards, GameController controller)
    {
        int rows = Mathf.CeilToInt(Mathf.Sqrt(cards.Count));
        int columns = Mathf.CeilToInt((float)cards.Count / rows);

        Vector2 offset = new Vector3(columns / 2f - 0.5f, rows / 2f - 0.5f);

        int cardIndex = 0;

        for (int x = 0; x < columns; x++)
        {
            for (int y = rows - 1; y >= 0; y--)
            {
                if (cardIndex >= cards.Count) break;

                Vector2 pos = new Vector2(x, y) - offset;

                Vector3 localPosition = new Vector3(controller.CardSize.x * 1.1f * pos.x, controller.CardSize.y * 1.1f * pos.y);

                Vector3 targetPosition = transform.position + (transform.rotation * localPosition);
                cards[cardIndex++].Move(targetPosition);

                yield return new WaitForSeconds(0.05f);
            }
        }

        controller.StartGame();
    }
}
