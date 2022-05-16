using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StageInfo : MonoBehaviour
{
    public TextMeshProUGUI stageText;
    public TextMeshProUGUI timerText;

    GameController controller;

    private void Start()
    {
        controller = FindObjectOfType<GameController>();

        if (controller.maxStage == 0) stageText.gameObject.SetActive(false);
        if (controller.maxTimeSeconds == 0) timerText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (controller.maxStage != 0)
        {
            stageText.text = "Stage: " + (controller.currentStage+1) + "/" + (controller.maxStage+1);
        }
        if (controller.maxTimeSeconds > 0)
        {
            timerText.text = controller.GetFormattedTime();
        }
    }
}
