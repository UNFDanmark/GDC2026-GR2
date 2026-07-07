using System;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class CardData : MonoBehaviour
{
    public RhythmManager rhythmManager;
    CardManager cardManager;
    
    [SerializeField] public NoteType[] noteChart;

    [SerializeField] Sprite sprite;

    [SerializeField] float damage;
    [SerializeField] float healing;
    [SerializeField] bool block;
    [SerializeField] float leechProcent;
    [SerializeField] bool hitAllNotesRequirement;

    [SerializeField] Button button;

    

    void Awake()
    {
        rhythmManager = GameObject.FindGameObjectWithTag("RhythmManager").GetComponent<RhythmManager>();
        cardManager = GameObject.FindGameObjectWithTag("CardManager").GetComponent<CardManager>();
    }

    void Start()
    {
        for (int i = 0; i < noteChart.Length; i++)
        {
            
        }
    }
}
