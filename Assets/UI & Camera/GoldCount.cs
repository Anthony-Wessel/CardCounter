using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoldCount : MonoBehaviour
{
    public TextMeshProUGUI text;

    void Update()
    {
        text.text = GoldManager.Gold.ToString();
    }
}
