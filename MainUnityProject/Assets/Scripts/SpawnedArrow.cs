using System;
using UnityEngine;

public class SpawnedArrow : MonoBehaviour
{
    [SerializeField] float speed = 3; //must be a multiple of the fixed update time
    public Transform target;
    
    void Update()
    {
        Vector3.Lerp(transform.position, target.position, speed);
    }
}
