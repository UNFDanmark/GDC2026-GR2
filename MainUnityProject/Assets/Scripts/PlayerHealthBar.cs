using System;
using UnityEngine;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] UnityEngine.UI.Image _imageToBeUnfilled;
    Player _player;

    void Awake()
    {
        _imageToBeUnfilled = GetComponent<UnityEngine.UI.Image>();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        _imageToBeUnfilled.fillAmount = _player.health / 1000;
    }
}
