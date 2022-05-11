using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class EndPanel : MonoBehaviour
{

    public TextMeshProUGUI title;
    public GameObject panel;

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadGameSelect()
    {
        Debug.LogWarning("LoadGameSelect not implemented yet");
    }

    public void Show(bool win)
    {
        title.text = win ? "You win!" : "You lose!";
        gameObject.SetActive(true);
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
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
            setAlpha((Time.realtimeSinceStartup - startTime) * 1.5f);
            yield return null;
        }

        setAlpha(0.75f);

        panel.SetActive(true);
    }
}