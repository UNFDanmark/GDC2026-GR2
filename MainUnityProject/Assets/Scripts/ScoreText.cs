using System;
using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    TextMeshProUGUI textComponent;
    [SerializeField] RhythmManager rhythmManager;

    void Awake()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        textComponent.text = rhythmManager.totalScore.ToString();
    }
}
