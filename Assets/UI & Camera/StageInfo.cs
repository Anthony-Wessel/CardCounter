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

        if (controller.MaxStage == 0) stageText.gameObject.SetActive(false);
        if (controller.MaxTimeSeconds == 0) timerText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (controller.MaxStage != 0)
        {
            stageText.text = "Stage: " + (controller.currentStage+1) + "/" + (controller.MaxStage+1);
        }
        if (controller.MaxTimeSeconds > 0)
        {
            timerText.text = controller.GetFormattedTime();
        }
    }
}
