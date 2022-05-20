using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class EndPanel : MonoBehaviour
{
    public enum EndState
    {
        Win,
        Lose,
        Draw
    }
    public TextMeshProUGUI title;
    public GameObject panel;

    public void Restart()
    {
        StartCoroutine(Fade(false));
        FindObjectOfType<GameController>().Restart();
    }

    public void LoadGameSelect()
    {
        StartCoroutine(Fade(false));
        FindObjectOfType<CameraController>().LoadMainMenu();
    }

    public void Show(EndState state)
    {
        switch (state)
        {
            case EndState.Win:
                title.text = "You win!";
                break;
            case EndState.Lose:
                title.text = "You lose!";
                break;
            case EndState.Draw:
                title.text = "Draw";
                break;
            default:
                title.text = "EndState error";
                break;
        }

        gameObject.SetActive(true);
        StartCoroutine(Fade(true));
    }

    IEnumerator Fade(bool fadeIn) // false means fade out
    {
        if (!fadeIn) panel.SetActive(false);

        Image background = GetComponent<Image>();

        float startTime = Time.realtimeSinceStartup;

        void setAlpha(float alpha)
        {
            Color c = background.color;
            c.a = alpha;
            background.color = c;
        }

        while (Time.realtimeSinceStartup - startTime < 0.5f)
        {
            float alpha = (Time.realtimeSinceStartup - startTime) * 1.5f;
            if (fadeIn)
                setAlpha(alpha);
            else
                setAlpha(0.75f - alpha);
            yield return null;
        }

        if (fadeIn)
            setAlpha(0.75f);
        else
            setAlpha(0);

        if (fadeIn) panel.SetActive(true);
    }
}
