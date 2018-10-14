using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constant:Singleton<Constant> {
    protected override void Awake()
    {
        base.Awake();
        baseAbility01 = new List<KeyValuePair<string, int>>();
        baseAbility01.Add(new KeyValuePair<string,int>(BaseAbility.strength.ToString(), 10));
        baseAbility01.Add(new KeyValuePair<string, int>(BaseAbility.intelligence.ToString(), 6));
        baseAbility01.Add(new KeyValuePair<string, int>(BaseAbility.accurate.ToString(), 14));
        baseAbility01.Add(new KeyValuePair<string, int>(BaseAbility.stamina.ToString(), 9));
        baseAbility01.Add(new KeyValuePair<string, int>(BaseAbility.resistance.ToString(), 9));
        baseAbility01.Add(new KeyValuePair<string, int>(BaseAbility.physique.ToString(), 8));
        baseAbility01.Add(new KeyValuePair<string, int>(BaseAbility.spirit.ToString(), 7));
        baseAbility01.Add(new KeyValuePair<string, int>(BaseAbility.agile.ToString(), 15));
        baseAbility01.Add(new KeyValuePair<string, int>(BaseAbility.luck.ToString(), 12));

        baseAbility02 = new List<KeyValuePair<string, int>>();
        baseAbility02.Add(new KeyValuePair<string, int>(BaseAbility.strength.ToString(), 15));
        baseAbility02.Add(new KeyValuePair<string, int>(BaseAbility.intelligence.ToString(), 5));
        baseAbility02.Add(new KeyValuePair<string, int>(BaseAbility.accurate.ToString(), 9));
        baseAbility02.Add(new KeyValuePair<string, int>(BaseAbility.stamina.ToString(), 13));
        baseAbility02.Add(new KeyValuePair<string, int>(BaseAbility.resistance.ToString(), 13));
        baseAbility02.Add(new KeyValuePair<string, int>(BaseAbility.physique.ToString(), 13));
        baseAbility02.Add(new KeyValuePair<string, int>(BaseAbility.spirit.ToString(), 7));
        baseAbility02.Add(new KeyValuePair<string, int>(BaseAbility.agile.ToString(), 7));
        baseAbility02.Add(new KeyValuePair<string, int>(BaseAbility.luck.ToString(), 8));

        baseAbility03 = new List<KeyValuePair<string, int>>();
        baseAbility03.Add(new KeyValuePair<string, int>(BaseAbility.strength.ToString(), 10));
        baseAbility03.Add(new KeyValuePair<string, int>(BaseAbility.intelligence.ToString(), 6));
        baseAbility03.Add(new KeyValuePair<string, int>(BaseAbility.accurate.ToString(), 14));
        baseAbility03.Add(new KeyValuePair<string, int>(BaseAbility.stamina.ToString(), 9));
        baseAbility03.Add(new KeyValuePair<string, int>(BaseAbility.resistance.ToString(), 9));
        baseAbility03.Add(new KeyValuePair<string, int>(BaseAbility.physique.ToString(), 8));
        baseAbility03.Add(new KeyValuePair<string, int>(BaseAbility.spirit.ToString(), 7));
        baseAbility03.Add(new KeyValuePair<string, int>(BaseAbility.agile.ToString(), 15));
        baseAbility03.Add(new KeyValuePair<string, int>(BaseAbility.luck.ToString(), 12));

        baseAbility04 = new List<KeyValuePair<string, int>>();
        baseAbility04.Add(new KeyValuePair<string, int>(BaseAbility.strength.ToString(), 8));
        baseAbility04.Add(new KeyValuePair<string, int>(BaseAbility.intelligence.ToString(), 16));
        baseAbility04.Add(new KeyValuePair<string, int>(BaseAbility.accurate.ToString(), 10));
        baseAbility04.Add(new KeyValuePair<string, int>(BaseAbility.stamina.ToString(), 9));
        baseAbility04.Add(new KeyValuePair<string, int>(BaseAbility.resistance.ToString(), 8));
        baseAbility04.Add(new KeyValuePair<string, int>(BaseAbility.physique.ToString(), 8));
        baseAbility04.Add(new KeyValuePair<string, int>(BaseAbility.spirit.ToString(), 14));
        baseAbility04.Add(new KeyValuePair<string, int>(BaseAbility.agile.ToString(), 10));
        baseAbility04.Add(new KeyValuePair<string, int>(BaseAbility.luck.ToString(), 7));


    }
    public static List<KeyValuePair<string, string>> baseAbilityDes=new List<KeyValuePair<string, string>>();
    public static void InitDescriptions()
    {
        baseAbilityDes.Clear();
        baseAbilityDes.Add(new KeyValuePair<string, string>("111111", "1111"));
        baseAbilityDes.Add(new KeyValuePair<string, string>("111111", "1111"));
        baseAbilityDes.Add(new KeyValuePair<string, string>("111111", "1111"));
        baseAbilityDes.Add(new KeyValuePair<string, string>("111111", "1111"));
        baseAbilityDes.Add(new KeyValuePair<string, string>("111111", "1111"));
        baseAbilityDes.Add(new KeyValuePair<string, string>("111111", "1111"));
        baseAbilityDes.Add(new KeyValuePair<string, string>("111111", "1111"));
        baseAbilityDes.Add(new KeyValuePair<string, string>("111111", "1111"));
        baseAbilityDes.Add(new KeyValuePair<string, string>("111111", "1111"));
    }
    public List<KeyValuePair<string, int>> baseAbility01;
    public List<KeyValuePair<string, int>> baseAbility02;
    public List<KeyValuePair<string, int>> baseAbility03;
    public List<KeyValuePair<string, int>> baseAbility04;
}
