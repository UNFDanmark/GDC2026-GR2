using System.Net.Mime;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TurnIndicator : MonoBehaviour
{
    public CombatManager combatManager;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        combatManager = GameObject.FindGameObjectWithTag("CombatManager").GetComponent<CombatManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (combatManager.isPlayersTurn)
        {
            GetComponent<TextMeshProUGUI>().text = "YOUR TURN!";
        }

        if (combatManager.isPlayingPlayerRhythmGame)
        {
            GetComponent<TextMeshProUGUI>().text = "SING ALL YOU GOT!";
        }
        if (combatManager.isEnemyAttacking)
        {
            GetComponent<TextMeshProUGUI>().text = "ENEMY IS ATTACKING, DEFEND!";
        }
    }
}
