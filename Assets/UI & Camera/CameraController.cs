using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public CameraTarget defaultTarget;

    [System.Serializable]
    public struct CameraTarget
    {
        public GameObject game;
        public Transform transform;
    }
    public CameraTarget[] targets;

    CameraTarget currentTarget;
    CameraTarget lastTarget;

    public GameObject GameUI;

    private void Awake()
    {
        currentTarget = defaultTarget;
        lastTarget = defaultTarget;
    }

    public void SelectGame(GameObject game)
    {
        foreach (CameraTarget ct in targets)
        {
            if (ct.game == game)
            {
                StartCoroutine(MoveToTarget(ct));
            }
        }
    }

    public void LoadMainMenu()
    {
        StartCoroutine(MoveToTarget(defaultTarget));
    }

    IEnumerator MoveToTarget(CameraTarget ct)
    {
        lastTarget = currentTarget;
        currentTarget = ct;

        float startTime = Time.time;
        float duration = 0.5f;

        while (Time.time - startTime < duration)
        {
            float progress = (Time.time - startTime) / duration;
            transform.Lerp(lastTarget.transform, currentTarget.transform, progress);
            yield return null;
        }

        transform.Lerp(lastTarget.transform, currentTarget.transform, 1);

        lastTarget.game.SetActive(false);
        currentTarget.game.SetActive(true);

        GameController gc = currentTarget.game.GetComponentInChildren<GameController>();
        if (gc != null)
        {
            GameUI.SetActive(true);
            gc.Restart();
        }
        else
        {
            GameUI.SetActive(false);
        }
    }


}
