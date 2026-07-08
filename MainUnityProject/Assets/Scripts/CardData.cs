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

    [SerializeField] Button button;

    

    void Awake()
    {
        rhythmManager = GameObject.FindGameObjectWithTag("RhythmManager").GetComponent<RhythmManager>();
        cardManager = GameObject.FindGameObjectWithTag("CardManager").GetComponent<CardManager>();
    }
}
