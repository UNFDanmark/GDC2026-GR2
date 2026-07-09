using System;

[System.Serializable]
[Flags]
public enum CardType
{
    powerRiff = 1, 
    healingTune = 2,
    blockingBallad = 4, 
    leechingHook = 8,
    drumDraw = 16,
    ostinatoBeam = 32,
    mendingMelody = 64,
    agonizingAnthem = 128
}
