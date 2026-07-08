using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DefaultNamespace
{
    public class InputCheckThingy : MonoBehaviour
    {
        public InputAction w;
        public InputAction a;
        public InputAction s;
        public InputAction d;
        public bool wIsPressed;
        public bool aIsPressed;
        public bool sIsPressed;
        public bool dIsPressed;

        void Start()
        {
            w.Enable();
            a.Enable();
            s.Enable();
            d.Enable();
        }

        void Update()
        {
            wIsPressed = w.IsPressed();
            aIsPressed = a.IsPressed();
            sIsPressed = s.IsPressed();
            dIsPressed = d.IsPressed();
        }
    }
}