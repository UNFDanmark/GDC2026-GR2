using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

//Go to SpawnedArrow line 7
public class Player : MonoBehaviour
{
    CombatManager combatManager;
    CardManager cardManager;
    
    [SerializeField] TextMeshProUGUI textComponent;
    
    [Header("Stats")] [SerializeField]
    public float health;

    [SerializeField] float maxHealth;

    public float bonusAttackIncrease = 1;
    public int bonusAttackIncreaseDuration;

    public float blockIncrease = 1;
    public int blockIncreaseDuration;

    public int extraCardDraw;
    
    
    void Awake()
    {
        combatManager = GameObject.FindGameObjectWithTag("CombatManager").GetComponent<CombatManager>();
        cardManager = GameObject.FindGameObjectWithTag("CardManager").GetComponent<CardManager>();
    }

    void Start()
    {
        combatManager.player = this;
        health = maxHealth;
    }

    void Update()
    {
        textComponent.text = $"P HP{health}";
    }

    public void TurnStarted()
    {
        GameObject.FindGameObjectWithTag("Larve").GetComponent<Animator>().SetBool("IsAttackingLarve", false);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("IsAttacking", false);
        DecreaseDurations();
        cardManager.DrawCard(1 + extraCardDraw);
        extraCardDraw = 0;
    }

    void DecreaseDurations()
    {
        bonusAttackIncreaseDuration -= 1;
        blockIncreaseDuration -= 1;

        if (bonusAttackIncreaseDuration <= 0)
        {
            bonusAttackIncrease = 1;
        }

        if (blockIncreaseDuration <= 0)
        {
            blockIncrease = 1;
        }
    }

    public void TakeDamage(float damage)
    {
        health -= Mathf.RoundToInt(Mathf.Clamp(damage / blockIncrease, 0, Single.PositiveInfinity));
        health = Mathf.Clamp(health, 0, Single.PositiveInfinity);;
        if (health <= 0)
        {
            print("Player Dead");
        }
        print($"Enemy did {damage} damage");
    }

    public void HealPlayer(float healing)
    {
        healing = Mathf.Clamp(healing, 0, Single.PositiveInfinity);
        
        health += healing;
        health = Mathf.RoundToInt(health);
        health = Mathf.Clamp(health, 0, maxHealth);
        print($"{healing} hp healed");
    }

    public void GiveBlock(float increase,int duration)
    {
        blockIncrease = 1 + increase;
        blockIncreaseDuration = duration;
    }
}
