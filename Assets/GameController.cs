using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameController : MonoBehaviour
{
    protected abstract void Win();
    protected abstract void Lose();

}
