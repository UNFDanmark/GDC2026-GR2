using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

//Go to SpawnedArrow line 7
public class Player : MonoBehaviour
{
    CombatManager combatManager;
    [SerializeField] TextMeshProUGUI textComponent;
    
    [Header("Stats")] [SerializeField]
    public float health;

    [SerializeField] float maxHealth;

    public float bonusAttackIncrease;
    public int bonusAttackIncreaseDuration;

    public float blockIncrease;
    public int blockIncreaseDuration;
    
    
    void Awake()
    {
        combatManager = GameObject.FindGameObjectWithTag("CombatManager").GetComponent<CombatManager>();
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

    public void TakeDamage(float damage)
    {
        health -= Mathf.RoundToInt(Mathf.Clamp(damage, 0, Single.PositiveInfinity));
        health = Mathf.Clamp(health, 0, Single.PositiveInfinity);;
        if (health <= 0)
        {
            print("Player Dead");
        }
    }
}
