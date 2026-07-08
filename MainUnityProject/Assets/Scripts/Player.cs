using System;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    CombatManager combatManager;

    
    [Header("Stats")] [SerializeField]
    public float health;

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
    }
}
