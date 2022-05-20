using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class WagerPopup : MonoBehaviour
{
    public Slider slider;

    public TextMeshProUGUI minWagerText;
    public TextMeshProUGUI maxWagerText;
    public TextMeshProUGUI currentWagerText;

    public Button confirmButton;

    public void Init(int minWager, int maxWager, int selectedWager, Action<int> onWagerConfirmed)
    {
        minWagerText.text = minWager.ToString();
        maxWagerText.text = maxWager.ToString();
        currentWagerText.text = selectedWager.ToString();

        slider.minValue = minWager;
        slider.maxValue = maxWager;
        slider.value = selectedWager;

        slider.onValueChanged.AddListener((x) => currentWagerText.text = slider.value.ToString());

        confirmButton.onClick.AddListener(()=> {
            onWagerConfirmed.Invoke(getSelectedWager());
            SetVisible(false);
            confirmButton.onClick.RemoveAllListeners();
        });

        SetVisible(true);
    }

    int getSelectedWager()
    {
        return (int)slider.value;
    }

    public void SetVisible(bool visible)
    {
        transform.GetChild(0).gameObject.SetActive(visible);
    }
}
