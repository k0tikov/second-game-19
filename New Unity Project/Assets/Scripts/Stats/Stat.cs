using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Stat {

    public int baseValue;
    public int test;


    //public float Value { get { return CalculateFinalValue(); } }


    private List<int> modifiers = new List<int>();

    public void AddModifier(int modifier)
    {
        if (modifier > 0)
            modifiers.Add(modifier);
        test = GetValue();
    }

    public void RemoveModifier(int modifier)
    {
        if (modifier > 0)
            modifiers.Remove(modifier);
        test = GetValue();
    }

    public int GetValue()
    {
        int finalValue = baseValue;

        modifiers.ForEach(x => finalValue += x);

        return finalValue;

    }
}
