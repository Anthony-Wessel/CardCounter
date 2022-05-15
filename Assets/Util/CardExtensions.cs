using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CardExtensions
{
    public static void Shuffle<T>(this List<T> cards)
    {
        cards.Shuffle(0, cards.Count);
    }

    public static void Shuffle<T>(this List<T> cards, int startInclusive, int endExclusive)
    {
        for (int i = startInclusive; i < endExclusive; i++)
        {
            int random = Random.Range(i, endExclusive);
            cards.swap(i, random);
        }
    }

    public static void BalancedShuffle<T>(this List<T> cards)
    {
        cards.Shuffle(0, cards.Count / 2);
        cards.Shuffle(cards.Count / 2, cards.Count);

        for (int i = 0; i < cards.Count / 2; i++)
        {
            if (i % 2 == 0) cards.swap(i, cards.Count / 2 + i);
        }

        for (int i = 1; i < cards.Count; i++)
        {
            if (Random.Range(0f, 1f) < 0.25f) cards.swap(i - 1, i);
        }
    }

    public static void swap<T>(this List<T> cards, int x, int y)
    {
        T c = cards[x];
        cards[x] = cards[y];
        cards[y] = c;
    }
}
