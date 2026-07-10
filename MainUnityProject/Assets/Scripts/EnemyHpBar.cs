using UnityEngine;

public class EnemyHpBar : MonoBehaviour
{
    [SerializeField] UnityEngine.UI.Image imageToBeUnfilled;
    Enemy _enemy;

    void Awake()
    {
        imageToBeUnfilled = GetComponent<UnityEngine.UI.Image>();
        _enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();
    }

    void Update()
    {
        imageToBeUnfilled.fillAmount = _enemy.health * 0.001f;
    }
}
