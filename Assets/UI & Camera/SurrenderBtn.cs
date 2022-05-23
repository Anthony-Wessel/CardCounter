using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurrenderBtn : MonoBehaviour
{
    public void Surrender()
    {
        FindObjectOfType<GameController>().Surrender();
    }
}
