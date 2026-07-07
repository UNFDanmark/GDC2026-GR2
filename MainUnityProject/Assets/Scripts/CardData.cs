using System;
using System.Linq;
using UnityEngine;

public class CardData : MonoBehaviour
{
    RhythmManager rhythmManager;
    
    [SerializeField] NoteType[] noteChart;

    [SerializeField] Sprite sprite;

    [SerializeField] float damage;
    [SerializeField] float healing;
    [SerializeField] bool block;
    [SerializeField] float leechProcent;
    [SerializeField] bool hitAllNotesRequirement;

    void Awake()
    {
        rhythmManager = GameObject.FindGameObjectWithTag("RhythmManager").GetComponent<RhythmManager>();
    }

    void Start()
    {
        for (int i = 0; i < noteChart.Length; i++)
        {
            
        }
    }

    void Update()
    {
        
    }

    public void PlayCard()
    {
        rhythmManager.notesQueue.AddRange(noteChart);
    }
}
