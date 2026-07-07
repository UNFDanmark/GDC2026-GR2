using System;

[System.Serializable]
[Flags]
public enum NoteType
{
    left = 1, 
    down = 2,
    up = 4, 
    right = 8 
}