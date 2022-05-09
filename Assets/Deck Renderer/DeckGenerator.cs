using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.SceneManagement;

public class DeckGenerator : MonoBehaviour
{
    public GameObject cardBackPrefab;
    public void GenerateDeck(DeckOptions options, DeckRenderer deckRenderer)
    {
        // Clear children
        for (int i = transform.childCount-1; i >= 0; i--)
            DestroyImmediate(transform.GetChild(i).gameObject);

        int numCards = options.suits.Length * options.cardTemplates.Length;
        Vector2Int size = getBestRect(numCards);

        deckRenderer.ResizeToFit(size);

        void placeCard(RectTransform rt, int xIndex, int yIndex)
        {
            rt.anchoredPosition = new Vector2((xIndex - size.x / 2f) * 500, (size.y / 2f - yIndex) * 700);
        }

        for (int suitIndex = 0; suitIndex < options.suits.Length; suitIndex++)
        {
            for (int templateIndex = 0; templateIndex < options.cardTemplates.Length; templateIndex++)
            {
                RectTransform newCard = Instantiate(options.cardTemplates[templateIndex], transform).GetComponent<RectTransform>();
                Texture2D numberTexture = options.numbers.GetTexture(templateIndex+1, options.useCourt);
                newCard.GetComponent<PlayingCard>().Init(options.suits[suitIndex], numberTexture, options.cardTexture);

                int cardNum = suitIndex * options.cardTemplates.Length + templateIndex;
                int x = cardNum % size.x;
                int y = cardNum / size.x;

                placeCard(newCard, x, y);
            }
        }

        RectTransform cardBack = Instantiate(cardBackPrefab, transform).GetComponent<RectTransform>();
        cardBack.GetComponent<RawImage>().texture = options.cardBackTexture;
        int x2 = numCards % size.x;
        int y2 = numCards / size.x;
        placeCard(cardBack, x2, y2);
    }

    Vector2Int getBestRect(int numCards)
    {
        float root = Mathf.Sqrt(numCards);
        int width = Mathf.CeilToInt(root);

        float remainder = (float)numCards / width;
        int height = Mathf.CeilToInt(remainder);

        return new Vector2Int(width, height);
    }
}
