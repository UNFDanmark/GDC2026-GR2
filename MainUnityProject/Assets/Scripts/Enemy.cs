using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = System.Random;

public class Enemy : MonoBehaviour
{
    [Header("Stats")] [SerializeField] 
    float maxHealth = 1000f;
    public float health;
    float defense = 1;

    public float defenseDecrease;
    public int defenseDecreaseDuration;
    

    public float attackDecrease;
    public int attackDecreaseDuration;

    
    
    [Header("Enemy Card Library")] [SerializeField]
    List<GameObject> cardDeck;
    [SerializeField]List<GameObject> bigCardDeck;

    int bigAttackCountdown;
    [SerializeField] int bigAttackCountdownAmount = 3;
    
    //[Header("Sounds")] [SerializeField]
    
    
    [Header("References")] [SerializeField]
    TextMeshProUGUI textComponent;

    CombatManager combatManager;
    RhythmManager rhythmManager;

    public GameObject nextAttack;

    void Awake()
    {
        combatManager = GameObject.FindGameObjectWithTag("CombatManager").GetComponent<CombatManager>();
        rhythmManager = GameObject.FindGameObjectWithTag("RhythmManager").GetComponent<RhythmManager>();
    }

    void Start()
    {
        bigAttackCountdown = bigAttackCountdownAmount;
        health = maxHealth;
        combatManager.currentEnemy = this.gameObject;
        GetNextAttack();
    }

    public void DecreaseDurations()
    {
        defenseDecreaseDuration -= 1;
        if (defenseDecreaseDuration <= 0)
        {
            defenseDecrease = 0;
        }
    }

    void Update()
    {
        textComponent.text = $"Hp: {health}";
    }

    public void TakeDamage(float damage)
    {
        health -= Mathf.RoundToInt((Mathf.Clamp(damage, 0, Single.PositiveInfinity)) * (defense + defenseDecrease));
        health = Mathf.Clamp(health, 0, Single.PositiveInfinity);;
        if (health <= 0)
        {
            print("Enemy Dead");
        }
    }

    public float imaginaryDamageOnenemy(float damage) //iahwldkjbjvbkjakrhkjf
    {
        float damageTaken;

        damageTaken = damage;

        return damageTaken;
    }

    
    public void GetNextAttack()
    {
        bigAttackCountdown -= 1;
        if (bigAttackCountdown <= 0)
        {
            nextAttack = bigCardDeck[UnityEngine.Random.Range(0, bigCardDeck.Count)];
            bigAttackCountdown = bigAttackCountdownAmount;
        }
        else
        {
            nextAttack = cardDeck[UnityEngine.Random.Range(0, cardDeck.Count)];
        }
    }

    public void GiveDefenseDecrease(float decreaseAmount, int duration)
    {
        defenseDecrease = decreaseAmount;
        defenseDecreaseDuration = duration + 1;
    }
}
