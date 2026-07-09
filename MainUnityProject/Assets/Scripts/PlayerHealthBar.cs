using System;
using UnityEngine;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] UnityEngine.UI.Image imageToBeUnfilled;
    Player _player;

    void Awake()
    {
        imageToBeUnfilled = GetComponent<UnityEngine.UI.Image>();
        _player = GameObject.FindGameObjectWithTag("DaPlayer").GetComponent<Player>();
    }

    void Update()
    {
        imageToBeUnfilled.fillAmount = _player.health * 0.001f;
    }
}
