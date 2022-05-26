using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [HideInInspector]
    public List<Card> cards;

    public bool useStages;
    public int currentStage;
    public int maxStage;
    public int MaxStage { get { return useStages ? maxStage : 0; } }

    public bool useTimer;
    public int maxTimeSeconds;
    public int MaxTimeSeconds { get { return useTimer ? maxTimeSeconds : 0; } }
    bool timerActive;
    string lastTime;
    float startTime;

    public int minWager;
    public int maxWager;
    int selectedWager;

    public Deck Deck { get { return DeckList.ActiveDeck; } }
    public GameObject CardPrefab;

    public bool running;

    Vector2 defaultCardSize = new Vector2(5, 7);
    public Vector2 CardSize
    {
        get
        {
            return defaultCardSize * transform.lossyScale;
        }
    }

    private void Awake()
    {
        selectedWager = -1;
        running = false;
    }

    public void Restart()
    {
        ShowWagerPopup();
    }

    protected virtual void ClearBoard()
    {
        currentStage = 0;

        cards.Clear();
        Card[] cardObjs = FindObjectsOfType<Card>();
        foreach (Card card in cardObjs)
        {
            card.StopAllCoroutines();
            Destroy(card.gameObject);
        }
    }

    public virtual void StartGame()
    {
        if (running) return;

        if (MaxTimeSeconds > 0) StartTimer();
        running = true;
    }

    void EndGame()
    {
        StopTimer();
        ClearBoard();

        running = false;
    }

    void Update()
    {
        if (timerActive && secondsRemaining() <= 0) Lose();
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
        if (currentStage < MaxStage)
            Invoke("loadNextStage", 1f);
        else
        {
            FindObjectOfType<EndPanel>().Show(EndPanel.EndState.Win);
            GoldManager.AddGold(selectedWager * 2);

            EndGame();
        }
    }
    protected void Lose()
    {
        FindObjectOfType<EndPanel>().Show(EndPanel.EndState.Lose);

        EndGame();
    }

    protected void Draw()
    {
        FindObjectOfType<EndPanel>().Show(EndPanel.EndState.Draw);
        GoldManager.AddGold(selectedWager);

        EndGame();
    }

    public void Surrender()
    {
        if (running)
            Lose();
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
        return Mathf.Max(0, MaxTimeSeconds - (Time.time - startTime));
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


    #region Wagers

    void ShowWagerPopup()
    {
        FindObjectOfType<WagerPopup>().Init(minWager, maxWager, selectedWager == -1 ? minWager : selectedWager, OnWagerConfirmed);
    }

    void OnWagerConfirmed(int selectedWager)
    {
        this.selectedWager = selectedWager;

        GoldManager.RemoveGold(selectedWager);
        LoadStage(0);
    }

    #endregion
}
