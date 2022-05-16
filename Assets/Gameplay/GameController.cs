using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [HideInInspector]
    public List<Card> cards;

    [Header("Stage variables")]
    public int currentStage;
    public int maxStage;

    [Header("Timer variables")]
    public int maxTimeSeconds;
    bool timerActive;
    string lastTime;
    float startTime;

    void Start()
    {
        LoadStage(0);
        if (maxTimeSeconds > 0) StartTimer();
    }

    void Update()
    {
        if (secondsRemaining() <= 0) Lose();
    }

    public virtual void AddCard(Card card)
    {
        cards.Add(card);
    }

    #region Stages

    protected virtual void LoadStage(int stage)
    {

    }

    void loadNextStage()
    {
        LoadStage(++currentStage);
    }

    #endregion


    #region Endings

    protected void Win()
    {
        if (currentStage < maxStage)
            Invoke("loadNextStage", 1f);
        else
        {
            FindObjectOfType<EndPanel>().Show(EndPanel.EndState.Win);
            StopTimer();
        }  
    }
    protected void Lose()
    {
        FindObjectOfType<EndPanel>().Show(EndPanel.EndState.Lose);

        StopTimer();
    }

    protected void Draw()
    {
        FindObjectOfType<EndPanel>().Show(EndPanel.EndState.Draw);

        StopTimer();
    }

    #endregion


    #region Timer

    private void StartTimer()
    {
        startTime = Time.time;
        timerActive = true;
    }

    private void StopTimer()
    {
        timerActive = false;
    }

    float secondsRemaining()
    {
        return Mathf.Max(0, maxTimeSeconds - (Time.time - startTime));
    }

    public string GetFormattedTime()
    {
        if (timerActive)
        {
            float remainingTime = secondsRemaining();
            int minutes = Mathf.FloorToInt(remainingTime / 60f);

            remainingTime -= (60 * minutes);
            int seconds = Mathf.FloorToInt(remainingTime);

            remainingTime -= seconds;
            int remainder = Mathf.FloorToInt(remainingTime * 100);

            lastTime = minutes + ":" + seconds + "." + remainder;
        }
        
        return lastTime;
    }

    #endregion
}
