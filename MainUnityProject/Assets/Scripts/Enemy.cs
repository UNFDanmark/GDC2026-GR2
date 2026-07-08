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

    public float attackDecrease;
    public int attackDecreaseDuration;

    [Header("Enemy Card Library")] [SerializeField]
    List<GameObject> cardDeck;

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
        health = maxHealth;
        combatManager.currentEnemy = this.gameObject;
        GetNextAttack();
    }

    void Update()
    {
        textComponent.text = $"Hp: {health}";
    }

    public void TakeDamage(float damage)
    {
        health -= Mathf.RoundToInt(Mathf.Clamp(damage, 0, Single.PositiveInfinity));
        health = Mathf.Clamp(health, 0, Single.PositiveInfinity);;
        if (health <= 0)
        {
            print("Enemy Dead");
        }
    }

    
    public void GetNextAttack()
    {
        nextAttack = cardDeck[UnityEngine.Random.Range(0, cardDeck.Count)];
    }
}
