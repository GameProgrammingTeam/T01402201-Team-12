using System;

[Serializable]
public struct SlimeProbability
{
    public SlimeProbability(SlimeSet slime, float probability)
    {
        this.slime = slime;
        this.probability = probability;
    }

    public SlimeSet slime;
    public float probability;
}