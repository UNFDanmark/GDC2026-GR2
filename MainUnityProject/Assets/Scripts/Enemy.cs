using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    public float health;

    [Header("Enemy Card Library")] [SerializeField]
    List<GameObject> cardDeck;

    [Header("References")] [SerializeField]
    TextMeshProUGUI textComponent;

    CombatManager combatManager;

    void Awake()
    {
        combatManager = GameObject.FindGameObjectWithTag("CombatManager").GetComponent<CombatManager>();
    }

    void Start()
    {
        combatManager.currentEnemy = this.gameObject;
    }

    void Update()
    {
        textComponent.text = $"Hp: {health}";
    }
}
