using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Header("Stats")]
    public NoteType[] noteChart;
    public EnemyAttackType attackType;

    
    public float damage;
    public int amountOfNotesInChart; //Tror lowkey vi skal sæt den manuelt ;-;

    public AudioSource sound;
}
