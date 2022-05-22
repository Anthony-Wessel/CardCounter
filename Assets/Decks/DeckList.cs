using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="DeckList")]
public class DeckList : ScriptableObject
{
    public Deck[] decks;

    static DeckList loadedList;
    static DeckList GetDeckList()
    {
        return Resources.Load<DeckList>("DeckList");
    }

    public static DeckList List
    {
        get
        {
            if (loadedList == null) loadedList = GetDeckList();

            return loadedList;
        }
    }

    public static Deck ActiveDeck
    {
        get
        {
            return List.decks[PlayerPrefs.GetInt("activeDeckID", 0)];
        }
    }

    public void SetActiveDeck(int id)
    {
        PlayerPrefs.SetInt("activeDeckID", id);
    }
}
