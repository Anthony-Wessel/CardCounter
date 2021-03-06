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
        confirmButton.onClick.RemoveAllListeners();

        minWagerText.text = minWager.ToString();
        maxWagerText.text = maxWager.ToString();
        currentWagerText.text = selectedWager.ToString();

        slider.minValue = minWager;
        slider.maxValue = maxWager;
        slider.value = Mathf.Min(selectedWager, GoldManager.Gold);

        slider.onValueChanged.AddListener((x) => {
            slider.value = Mathf.Min(slider.value, GoldManager.Gold);
            currentWagerText.text = slider.value.ToString();
        });
    

        confirmButton.onClick.AddListener(()=> {
            onWagerConfirmed.Invoke(getSelectedWager());
            SetVisible(false);
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
