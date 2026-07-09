using System;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class CardData : MonoBehaviour
{
    public AudioClip cardMelody;
    
    public RhythmManager rhythmManager;
    CardManager cardManager;
    
    [SerializeField] public NoteType[] noteChart;

    [SerializeField] Sprite sprite;

    public CardType cardType;

    public float damage;
    public float healing;
    public bool block;
    public float leechProcent;
    public bool hitAllNotesRequirement;
    public bool drawCards;
    public bool increaseDamage;
    public int increaseDamageDuration;
    public bool decreaseEnemyDamage;
    public int decreaseEnemyDamageDuration;
    public int noteAmount;
    [Range(0.5f, 2f)]public float noteSpeed;

    [SerializeField] Button button;

    

    void Awake()
    {
        if (gameObject.tag == "CombatManager")
        {
            return;
        }
        noteAmount = 0;
        rhythmManager = GameObject.FindGameObjectWithTag("RhythmManager").GetComponent<RhythmManager>();
        cardManager = GameObject.FindGameObjectWithTag("CardManager").GetComponent<CardManager>();
        print(noteChart);
        for (int i = 0; i < noteChart.Length; i++)
        {
            if (noteChart[i].HasFlag(NoteType.left))
            {
                noteAmount++;
            }
            if (noteChart[i].HasFlag(NoteType.down))
            {
                noteAmount++;
            }
            if (noteChart[i].HasFlag(NoteType.up))
            {
                noteAmount++;
            }
            if (noteChart[i].HasFlag(NoteType.right))
            {
                noteAmount++;
            }
        }
    }
}
