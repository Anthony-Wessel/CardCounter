using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldAdder : MonoBehaviour
{
    public void AddGold(int amount)
    {
        GoldManager.AddGold(amount);
    }
}
