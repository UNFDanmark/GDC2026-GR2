using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class HitDetection : MonoBehaviour
{
    public InputAction hit;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hit.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (hit.WasPerformedThisFrame())
        { 
            print("you pressed si button");
        }
    }
}
