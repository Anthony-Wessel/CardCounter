using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldManager : MonoBehaviour
{
    public static int Gold
    {
        get
        {
            return PlayerPrefs.GetInt("Gold", 50);
        }
    }

    /// <summary>
    /// Add a specific amount of gold to the total
    /// </summary>
    /// <param name="amount">amount of gold to add</param>
    public static void AddGold(int amount)
    {
        PlayerPrefs.SetInt("Gold", Mathf.Min(Gold + amount, int.MaxValue));
    }

    /// <summary>
    /// Remove a specific amount of gold from the total. Will not remove any gold if amount exceeds total.
    /// </summary>
    /// <param name="amount">amount of gold to remove</param>
    /// <returns>True if amount exceeds current gold total, False otherwise</returns>
    public static bool RemoveGold(int amount)
    {
        if (amount > Gold) return true;

        PlayerPrefs.SetInt("Gold", Gold - amount);
        return false;
    }
}
