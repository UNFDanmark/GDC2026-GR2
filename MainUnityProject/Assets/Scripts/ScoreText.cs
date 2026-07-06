using System;
using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    TextMeshProUGUI textComponent;

    void Awake()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        textComponent.text = RythmManager.instance.totalScore.ToString();
    }
}
